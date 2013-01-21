using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Файл содержит все связаное со сменой состояния игры по таймеру

namespace Contact.Server
{
    // Содержит длительности разных фаз игры
    class Timing
    {
        // State => duration (in milliseconds)
        private static readonly Dictionary<GameState.State, int> StateDurationTime = new Dictionary<GameState.State, int>
            {
                {GameState.State.NotStarted, Int32.MaxValue},
                {GameState.State.HaveNoPrimaryWord, Int32.MaxValue},
                {GameState.State.HaveNoCurrentWord, 200*1000}, //TODO: check for right timeout
                {GameState.State.HaveCurrentWord, 200*1000}, //TODO: check for right timeout
                {GameState.State.HaveCurrentWordVariant, 5*1000},
                {GameState.State.VotingForPlayersWords, 10*1000},
                {GameState.State.GameOver, Int32.MaxValue},
                {GameState.State.VotingForHostWord, 10*1000},
                {GameState.State.WeHaveChiefWord, 200*1000}
            };

        public static int Duration(GameState.State state)
        {
            if (!StateDurationTime.ContainsKey(state))
            {
                LogSaver.Log("[!!!] StateDurationTime не установлено для "+state.ToString());
                throw new Exception("StateDurationTime не установлено");
            }
            return StateDurationTime[state];
        }
    }

    partial class Room
    {
        private System.Timers.Timer stateTimer;

        public delegate void StateTimeoutDelegate();

        private Dictionary<GameState.State, StateTimeoutDelegate> timeoutFunctions;

        private void InitTiming()
        {
            timeoutFunctions = new Dictionary
            <GameState.State, StateTimeoutDelegate>
            {
                {GameState.State.HaveCurrentWord, this.HaveCurrentWordTimeout},
                {GameState.State.HaveCurrentWordVariant, this.HaveCurrentWordVariantTimeout},
                {GameState.State.VotingForPlayersWords, this.VotingForPlayersWordsTimeout},
                {GameState.State.HaveNoCurrentWord, this.HaveNoCurrentWordTimeout},
                {GameState.State.VotingForHostWord, this.VotingForHostWordTimeout},

            };

            stateTimer = new System.Timers.Timer();
            stateTimer.Elapsed += Timer_Elapsed;
            stateTimer.AutoReset = false;
        }


        private void SetStateTimer(GameState.State state)
        {
            stateTimer.Stop();
            stateTimer.Interval = Timing.Duration(state);
            stateTimer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // TODO: check that state that ended is the state that was sheduled
            //Invoke appropriate function
            if (!timeoutFunctions.ContainsKey(gameState.state))
            {
                LogSaver.Log("[!!!] timeoutFunctions не задан для " + gameState.state.ToString());
                throw new Exception("timeoutFunctions не задан");
            }
            timeoutFunctions[gameState.state]();
        }

        #region State timeout functions

        private void HaveNoCurrentWordTimeout()
        {
            LogSaver.Log("HaveNoCurrentWord Timeout");
            //TODO: remove stub and do actual logic
            ChangeState(GameState.State.NotStarted);
        }

        private void HaveCurrentWordTimeout()
        {
            LogSaver.Log("HaveCurrentWord Timeout");
            //TODO: remove stub and do actual logic
            ChangeState(GameState.State.HaveNoCurrentWord);
        }

        private void HaveCurrentWordVariantTimeout()
        {
            LogSaver.Log("HaveCurrentWordVariant Timeout");

            lock (gameState)
            {
                gameState.PrepareForVoting(2);
            }

            ChangeState(GameState.State.VotingForPlayersWords);
        }
        private void VotingForHostWordTimeout()
        {
            LogSaver.Log("VotingForChiefWord Timeout");         
            bool openLetter = false;
            bool winGame = false;
            bool ChiefWord = false;

            lock (gameState)
            {
                ChiefWord = gameState.votings[0].Accepted(gameState.Users.Count);

                if (ChiefWord)
                    gameState.UsedWords.Add(gameState.ChiefWord);
                else
                {
                    gameState.NumberOfOpenChars++;

                    if (gameState.NumberOfOpenChars == gameState.PrimaryWord.Length) winGame = true;
                    else openLetter = true;
                }                
            }            

            if (ChiefWord)
                BroadcastMessage(GameMessage.UsedWordAddedMessage(gameState.ChiefWord));

            if (openLetter)
                BroadcastMessage(GameMessage.PrimaryWordCharOpened());

            if (winGame)
                ChangeState(GameState.State.GameOver);
            else
                ChangeState(GameState.State.HaveNoCurrentWord);

        }
        private void VotingForPlayersWordsTimeout()
        {
            LogSaver.Log("VotingForPlayersWords Timeout");
            User contacter, questioner;
            bool openLetter = false;
            bool winGame = false;
            bool CurrentWordAccepted = false;
            bool VarOfCurWordAccepted = false;

            lock (gameState)
            {
                CurrentWordAccepted = gameState.votings[0].Accepted(gameState.Users.Count);
                VarOfCurWordAccepted = gameState.votings[1].Accepted(gameState.Users.Count);


                // добавить в список используемых
                if (CurrentWordAccepted)
                    gameState.UsedWords.Add(gameState.CurrentWord);

                if (VarOfCurWordAccepted)
                    gameState.UsedWords.Add(gameState.VarOfCurWord);


                if (CurrentWordAccepted && VarOfCurWordAccepted) // игроки выиграли
                {
                    gameState.NumberOfOpenChars++;

                    if (gameState.NumberOfOpenChars == gameState.PrimaryWord.Length) winGame=true;
                    else openLetter = true;
                }


                // сбросить роли
                contacter = gameState.Users.Single(user => user.role == User.Role.Contacter);
                contacter.role = User.Role.None;

                questioner = gameState.Users.Single(user => user.role == User.Role.Qwestioner);
                questioner.role = User.Role.None;
            }

            //разослать изменения
            BroadcastMessage(GameMessage.UserRoleChangedMessage(contacter, User.Role.None));
            BroadcastMessage(GameMessage.UserRoleChangedMessage(questioner, User.Role.None));

            //Сообщить о добавленых словах 
            if(CurrentWordAccepted)
                BroadcastMessage(GameMessage.UsedWordAddedMessage(gameState.CurrentWord)); // TODO: а это безопасно?
            if(VarOfCurWordAccepted)
                BroadcastMessage(GameMessage.UsedWordAddedMessage(gameState.VarOfCurWord));


            if(openLetter) 
                BroadcastMessage(GameMessage.PrimaryWordCharOpened());

            if(winGame)
                ChangeState(GameState.State.GameOver);
            else
                ChangeState(GameState.State.HaveNoCurrentWord);

            LogSaver.Log("VotingForPlayersWords Timeout end");
        }
        #endregion
    }

}
