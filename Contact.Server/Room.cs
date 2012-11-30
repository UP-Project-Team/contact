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

            BroadcastMessage(GameMessage.UserJoinedRoomMessage(user), user);
        }

        public void LeaveRoom(User user)
        {
            lock (gameState)
            {
                gameState.Users.Remove(user);
            }

            BroadcastMessage(GameMessage.UserLeftRoomMessage(user), user);
        }

        public GameState GetState()
        {
            // TODO: make sure that this works properly
            lock (gameState)
            {
                return gameState;
            }
        }

        public void StartGame()
        {
            //TODO: start with normal state
            ChangeState(GameState.State.HaveCurrentWord, null);
        }

        public void ChangeState(GameState.State state, User invoker)
        {
            lock (gameState)
            {
                gameState.state = state;
                SetStateTimer(state);
                BroadcastMessage(GameMessage.StateChangedMessage(state), invoker);
            }
        }

        private void BroadcastMessage(GameMessage message, User exclusion)
        {
            var deadUsers = new List<User>();
            lock (gameState)
            {
                foreach (var user in gameState.Users)
                {
                    if (user == exclusion) continue; // Do not notify this user
                    
                    try
                    {
                        user.Callback.Notify(message);
                    }
                    catch (CommunicationException e)
                    {
                        // assume that client is down. we can't logof it now - deadlock
                        deadUsers.Add(user);
                    }
                    catch (Exception e)
                    {
                        LogSaver.Log("FAIL!! mysterious callback exception");
                    }
                }
            }

            // logoff all deadUsers
            foreach (var deadUser in deadUsers)
            {
                LogSaver.Log("User "+deadUser.Id+" is dead. Logoff");
                RoomControll.DeleteOnlineUser(deadUser.Id);
            }
        }
    }
}
