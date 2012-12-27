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
            if (user.Id==-1)
            {
                GameException.Throw("Password incorrect");
            }
            LogSaver.Log("Loged in successfully. UserId= "+user.Id);            
            return user;
        }

        public void Registration(string name, string password)
        {
            LogSaver.Log("Attempt to registr. name=" + name + " password=" + password);                   
            int i=DBAccess.UserReg(name, password);
            if (i != 0)
            {
                GameException.Throw("This name is already registred");
            }
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
