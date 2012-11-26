using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{

    [DataContract]
    public class Room
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
        }

        public void EnterRoom(User user)
        {
            // TODO: notify others
            lock (gameState)
            {
                gameState.Users.Add(user);
            }
        }

        public void LeaveRoom(User user)
        {
            // TODO: notify all other users that user left
            lock (gameState)
            {
                gameState.Users.Remove(user);
            }
        }

        public GameState GetState()
        {
            // TODO: make sure that this works properly
            lock (gameState)
            {
                return gameState;
            }
        }
    }
}
