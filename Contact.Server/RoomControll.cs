using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    class RoomControll
    {
        private static int roomsCount;
        private static readonly Dictionary<int, Room> Rooms = new Dictionary<int, Room>();
        private static readonly List<User> OnlineUsers = new List<User>();

        public static int AddRoom(string name)
        {
            var newRoomId = roomsCount++;
            Rooms.Add(newRoomId, new Room(newRoomId, name));

            //TODO: remove this
            Rooms[newRoomId].StartGame();

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
        
        public static void DeleteOnlineUser(int userId)
        {
            User user;
            try
            {
                user = GetUserById(userId);
            }
            catch (Exception e)
            {
                // user already offline
                return;
            }

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

        public static GameState GetState(int userId)
        {
            var user = GetUserById(userId);
            return Rooms[user.RoomId].GetState();
        }

    }
}
