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
        public Guid Login(string name, string password)
        {
            LogSaver.Log("Attempt to login. name="+name+" password="+password);
            var token = AccountControll.LoginUser(name, password);
            LogSaver.Log("Loged in successfully. UserId=");
            return token;
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

        public void GiveCurrentWordVariant(Guid token, string word)
        {
            var user = RoomControll.GetUserByToken(token);
            LogSaver.Log("GiveCurrentWordVariant userId="+user.Id);
            RoomControll.GiveCurrentWordVariant(user, word);
        }
    }
}
