using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    // it's room controll
    class RoomControll
    {
        private static int roomsCount;
        private static readonly Dictionary<int, Room> Rooms = new Dictionary<int, Room>();
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

        public static int AddRoom(string name)
        {
            var newRoomId = roomsCount++;
            Rooms.Add(newRoomId, new Room(newRoomId, name));
            
            return newRoomId;
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

        public static void AddOnlineUser(User user)
        {
            lock (OnlineUsers)
            {
                //TODO: what if user already online?
                OnlineUsers.Add(user);
            }
            
            //TODO: assign user to lobby, not to artificial room
            user.RoomId = 0;
            Rooms[0].EnterRoom(user);
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

        public static GameState GetState(User user)
        {
            return Rooms[user.RoomId].GetState();
        }

        public static void StartGame(User user)
        {
            Rooms[user.RoomId].StartGame();
        }

        public static void GiveQuestion(User user, string word)
        {
            Rooms[user.RoomId].AcceptQuestion(user, word);
        }

        public static void GiveCurrentWordVariant(User user, string word)
        {
            Rooms[user.RoomId].AcceptCurrentWordVariant(user, word);
        }

    }
}
