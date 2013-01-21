using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;
using System.Timers;

namespace Contact.Server
{

    [DataContract]
    public partial class Room
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public int Id { get; private set; }

        public GameState gameState { get; private set; }
        
        public Room(int id, string name)
        {
            Id = id;
            Name = name;
            gameState = new GameState();
            
            //setup timer
            InitTiming();
        }

        public void EnterRoom(User user)
        {
            lock (gameState)
            {
                gameState.Users.Add(user);
            }

            BroadcastMessage(GameMessage.UserJoinedRoomMessage(user));
        }

        public void LeaveRoom(User user)
        {
            if (user.role == User.Role.Host)
            {
                ChangeState(GameState.State.GameOver);
                ResetRoles();
            }

            if (user.role == User.Role.Qwestioner)
            {
                ChangeState(GameState.State.HaveNoCurrentWord);
                BroadcastMessage(GameMessage.UserRoleChangedMessage(user, User.Role.None));
            }
            lock (gameState)
            {
                gameState.Users.Remove(user);
            }

            BroadcastMessage(GameMessage.UserLeftRoomMessage(user));
        }

        public GameState GetState()
        {
            // TODO: make sure that this works properly
            lock (gameState)
            {
                return gameState;
            }
        }

        public void AcceptPrimaryWord(User user, string primaryWord)
        {
            if(user.role != User.Role.Host)
            {
                LogSaver.Log("!!! AcceptPrimaryWord invoked user " + user.Id + " role " + user.role.ToString());
                GameException.Throw("Вы не можете этого делать!");
            }

            lock (gameState)
            {
                gameState.PrimaryWord = primaryWord.ToLower();
                ChangeState(GameState.State.HaveNoCurrentWord);
            }
        }

        public void AcceptQuestion(User user, string question, string word)
        {
            if (user.role != User.Role.None)
            {
                LogSaver.Log("!!! AcceptQuestion invoked user " + user.Id + " role " + user.role.ToString());
                GameException.Throw("Вы не можете этого делать!");
            }

            bool epicWin = false; //угадал primaryWord

            lock (gameState)
            {
                gameState.Question = question;

                if (!word.StartsWith(gameState.PrimaryWordKnownLetters))
                    GameException.Throw("Слово должно начинаться с открытых букв");

                if (word == gameState.PrimaryWord)
                {
                    epicWin = true;
                }
                else
                {
                    gameState.CurrentWord = word;
                    user.role = User.Role.Qwestioner;
                }
            }

            if (epicWin)
            {
                ResetRoles();
                ChangeState(GameState.State.GameOver);
            }
            else
            {
                BroadcastMessage(GameMessage.QuestionAsked(question, word));
                BroadcastMessage(GameMessage.UserRoleChangedMessage(user, User.Role.Qwestioner));
                ChangeState(GameState.State.HaveCurrentWord);
            }

        }

        public void AcceptCurrentWordVariant(User user, string word)
        {
            if (user.role != User.Role.None)
            {
                LogSaver.Log("!!! AcceptCurrentWordVariant invoked user "+user.Id+" role "+user.role.ToString());
                GameException.Throw("Вы не можете так делать");
            }

            bool epicWin = false; // угадал primaryWord

            lock (gameState)
            {
                if(gameState.UsedWords.Contains(word))
                    GameException.Throw("Это слово уже использовалось");

                if (!word.StartsWith(gameState.PrimaryWordKnownLetters))
                    GameException.Throw("Слово должно начинаться с открытых букв");

                if (word == gameState.PrimaryWord)
                {
                    epicWin = true;
                }
                else
                {
                    gameState.VarOfCurWord = word;
                    user.role = User.Role.Contacter;
                }
               
            }

            if (epicWin)
            {
                ResetRoles();
                ChangeState(GameState.State.GameOver);
            }
            else
            {
                BroadcastMessage(GameMessage.UserRoleChangedMessage(user, User.Role.Contacter));
                BroadcastMessage(GameMessage.VarOfCurWordChangedMessage(word));
                ChangeState(GameState.State.HaveCurrentWordVariant);
            }
        }
        public void AcceptChiefWord(User user, string word)
        {
            if (user.role != User.Role.Host)
            {
                LogSaver.Log("!!! AcceptChiefWord invoked user " + user.Id + " role " + user.role.ToString());
                GameException.Throw("Вы не можете так делать");
            }

            lock (gameState)
            {
                if (gameState.UsedWords.Contains(word))
                    GameException.Throw("Это слово уже использовалось");

                if (!word.StartsWith(gameState.PrimaryWordKnownLetters))
                    GameException.Throw("Слово должно начинаться с открытых букв");
                if (word == gameState.PrimaryWord)
                    GameException.Throw("Так ты спалишь загаданное слово!");

                gameState.PrepareForVoting(1);
                gameState.ChiefWord = word;
                BroadcastMessage(GameMessage.WeHaveChiefWord(word));
                ChangeState(GameState.State.VotingForHostWord);
            }
        }

        public void VoteForPlayerWord(User user, int wordId, bool up)
        {
            lock (gameState)
            {
               if(user.role != User.Role.None)
                   GameException.Throw("Вы не можете голосовать");

                gameState.votings[wordId].Vote(user, up);
            }
        }
        public void VoteForChiefWord(User user, bool up)
        {
            lock (gameState)
            {
                if (user.role == User.Role.Host)
                    GameException.Throw("Вы не можете голосовать");

                gameState.votings[0].Vote(user, up);
            }
        }
        public void StartGame()
        {
            //TODO: start with normal state
            ChangeState(GameState.State.HaveNoPrimaryWord);
        }

        public void ChangeState(GameState.State state)
        {
            lock (gameState)
            {
                gameState.state = state;
                SetStateTimer(state);
            }

            BroadcastMessage(GameMessage.StateChangedMessage(state));
        }

        private void BroadcastMessage(GameMessage message)
        {
            var deadUsers = new List<User>();
            lock (gameState)
            {
                foreach (var user in gameState.Users)
                {
                    try
                    {
                        user.Callback.Notify(message);
                    }
                    catch (CommunicationException e)
                    {
                        // assume that client is down. we can't logof it now - deadlock
                        LogSaver.Log(e.Message);
                        deadUsers.Add(user);
                    }
                    catch (Exception e)
                    {
                        LogSaver.Log("FAIL!! mysterious callback exception: "+e.Message);
                    }
                }
            }

            // logoff all deadUsers
            foreach (var deadUser in deadUsers)
            {
                LogSaver.Log("User "+deadUser.Id+" is dead. Logoff");
                RoomControll.DeleteOnlineUser(deadUser);
            }
        }

        private void ResetRoles()
        {
            User host, questioner, contacter;

            lock (gameState)
            {
                host = gameState.Users.SingleOrDefault(user => user.role == User.Role.Host);
                if (host != null) host.role = User.Role.None;
                questioner = gameState.Users.SingleOrDefault(user => user.role == User.Role.Qwestioner);
                if (questioner != null) questioner.role = User.Role.None;
                contacter = gameState.Users.SingleOrDefault(user => user.role == User.Role.Contacter);
                if (contacter != null) contacter.role = User.Role.None;
            }

            if(host!=null)
                BroadcastMessage(GameMessage.UserRoleChangedMessage(host, User.Role.None));
            if(contacter!=null)
                BroadcastMessage(GameMessage.UserRoleChangedMessage(contacter, User.Role.None));
            if(questioner!=null)
                BroadcastMessage(GameMessage.UserRoleChangedMessage(questioner, User.Role.None));
        }
    }
}