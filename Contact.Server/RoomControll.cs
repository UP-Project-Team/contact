using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace Contact.Server
{
    // it's room controll
    class RoomControll
    {
        private static int roomsCount;
        private static readonly ConcurrentDictionary<int, Room> Rooms = new ConcurrentDictionary<int, Room>();
        private static readonly List<User> OnlineUsers = new List<User>();

        public static User GetUserByToken(Guid token)
        {
            lock (OnlineUsers)
            {
                foreach (var user in OnlineUsers.Where(user => user.Token == token))
                {
                    return user;
                }
            }

            LogSaver.Log("Asked for user with unknown GUID");
            throw new AccessViolationException("Asked for user with unknown GUID");
        }

        public static void AddRoom(string name)
        {
            foreach(var asd in Rooms)
            {
                if(asd.Value.Name==name)
                GameException.Throw("Комната с таким именем уже есть");
            }
            
            var newRoomId = roomsCount;
            Interlocked.Increment(ref roomsCount);
            Rooms.TryAdd(newRoomId, new Room(newRoomId, name));
            GameMessage message = GameMessage.AddedRoom(name, newRoomId);
            lock (OnlineUsers)
            {
                foreach (var user in OnlineUsers)
                {
                    try
                    {
                        user.Callback.Notify(message);
                    }
                    catch (Exception e)
                    {
                        LogSaver.Log("FAIL!! mysterious callback exception: " + e.Message);
                    }
                }
            }
        }

        private static User GetUserById(int userId)
        {
            User user;
            lock (OnlineUsers)
            {
                try
                {
                    user = OnlineUsers.Find(curUser => curUser.Id == userId);
                }
                catch (Exception e)
                {
                    //It should be ok even if this happens
                    LogSaver.Log("Tried to GetUserById wher user not online. userId="+userId);
                    throw;
                }
            }

            return user;
        }
        public static User GetUserByName(string username)
        {
            return OnlineUsers.Find(p => p.Name == username);
        }
        public static void LogoffUser(User user)
        {
            User us = RoomControll.GetUserByName(user.Name);
            GameMessage msg = new GameMessage();
            msg = GameMessage.LogoffUser();           
            DeleteOnlineUser(us);
            try { us.Callback.Notify(msg); }
            catch { }
        }
        public static void AddOnlineUser(User user)
        {
            lock (OnlineUsers)
            {
                if (RoomControll.CheckUser(user.Name))
                {                    
                    LogoffUser(user);            
                }
                OnlineUsers.Add(user);
            }
            
            // assign user to lobby
            user.RoomId = 0;
            Rooms[0].EnterRoom(user);
        }
        public static bool CheckUser(string username)
        {
            if (OnlineUsers.Exists(p => p.Name == username))
                return true;
            else
                return false;
        }
        public static void DeleteOnlineUser(User user)
        {
            lock (OnlineUsers)
            {
                //TODO: error handling?
                try
                {
                    OnlineUsers.Remove(user);
                }
                catch (Exception e)
                {
                    LogSaver.Log("Tried to delete user "+user.Id+" but it is not online");
                    //throw;
                }
            }
            
            Rooms[user.RoomId].LeaveRoom(user);
        }
        public static void UserLeaveRoom(User user)
        {
            DeleteOnlineUser(user);
            AddOnlineUser(user);
        }
        public static GameState GetState(User user)
        {
            return Rooms[user.RoomId].GetState();
        }

        public static void StartGame(User user)
        {
            Rooms[user.RoomId].StartGame();
        }
        public static void LeaveRoom(User user)
        {
            DeleteOnlineUser(user);
            AddOnlineUser(user);
        }
        public static void AskQuestion(User user, string question, string word)
        {
            Rooms[user.RoomId].AcceptQuestion(user, question, word);
        }

        public static void GiveCurrentWordVariant(User user, string word)
        {
            Rooms[user.RoomId].AcceptCurrentWordVariant(user, word);
        }

        public static void GiveChiefWord(User user, string word)
        {
            Rooms[user.RoomId].AcceptChiefWord(user, word);
        }
        public static void VoteForPlayerWord(User user, int wordId, bool up)
        {
            Rooms[user.RoomId].VoteForPlayerWord(user, wordId, up);
        }

        public static void VoteForChifWord(User user, bool up)
        {
            Rooms[user.RoomId].VoteForChiefWord(user, up);
        }

        public static List<Room> GetRoomsList(User user)
        {
            return Rooms.Values.ToList();
        }

        public static void BroadcastChatMessageToRoom(User user, string message)
        {
            Rooms[user.RoomId].BroadcastChatMessage(message, user);
        }

        public static void GotoRoom(User user, int roomId)
        {
            if (!Rooms.ContainsKey(roomId))
            {
                LogSaver.Log("!?! Trying to go to nonexistent room "+roomId+" userId="+user.Id);
                GameException.Throw("Комнаты с таким номером нет");
            }

            if (roomId == user.RoomId) return;

            Rooms[user.RoomId].LeaveRoom(user);
            user.RoomId = roomId;
            Rooms[roomId].EnterRoom(user);
        }
    }
}