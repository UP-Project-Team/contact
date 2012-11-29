﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    class RoomControll
    {
        private static int roomsCount;
        private static readonly List<Room> Rooms = new List<Room>();
        private static readonly List<User> OnlineUsers = new List<User>();

        public static int AddRoom(string name)
        {
            var newRoomId = roomsCount++;
            Rooms.Add(new Room(newRoomId, name));

            //TODO: remove this
            Rooms[0].StartGame();

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
                    //Not sure that this can happen
                    LogSaver.Log("FAIL!!! Tried to GeyUserById wher user not online. userId="+userId);
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
            var user = GetUserById(userId);

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
