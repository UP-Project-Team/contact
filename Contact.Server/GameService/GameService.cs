using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Contact.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class GameService : IGameService
    {
        public UserData Login(string name, string password)
        {
            LogSaver.Log("Attempt to login. name="+name+" password="+password);
            var user = AccountControll.LoginUser(name, password);            
            LogSaver.Log("Loged in successfully. UserId= "+user.Id);            
            return user;
        }

        public void Registration(string name, string password)
        {
            LogSaver.Log("Attempt to registr. name=" + name + " password=" + password);                   
            DBAccess.UserReg(name, password);            
        }

        public void Logoff(Guid token)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("Logoff userId=" + user.Id);
            RoomControll.DeleteOnlineUser(user);
        }

        public GameState GetState(Guid token)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("GetState asked userId=" + user.Id);
            return RoomControll.GetState(user);
        }


        public void StartGame(Guid token)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("Start Game userId=" + user.Id);
            RoomControll.StartGame(user);
        }

        public void AskQuestion(Guid token, string question, string word)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("AskQuestion userId=" + user.Id);
            RoomControll.AskQuestion(user, question, word);
        }

        public void GiveCurrentWordVariant(Guid token, string word)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("GiveCurrentWordVariant userId="+user.Id);
            RoomControll.GiveCurrentWordVariant(user, word);
        }


        public void VoteForPlayerWord(Guid token, int wordId, bool up)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("VoteForPlayerWord userId=" + user.Id + " word=" + wordId + (up ? "up" : "down"));
            RoomControll.VoteForPlayerWord(user, wordId, up);
        }


        public List<Room> GetRoomsList(Guid token)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("GetRoomsList userId="+user.Id);

            return RoomControll.GetRoomsList(user);
        }

        public void GotoRoom(Guid token, int roomId)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("GotoRoom userId="+user.Id+" to room "+roomId);

            RoomControll.GotoRoom(user, roomId);
        }

        public void AddRoom(Guid token, string roomName)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("AddRoom userId="+user.Id+" roomName="+roomName);

            RoomControll.AddRoom(roomName);
        }
    }
}
