using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Contact.Server
{
    
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class GameService : IGameService
    {
        // Id of user who started this session
        private int UserId;

        public void Login(string name, string password)
        {
            LogSaver.Log("Attempt to login. name="+name+" password="+password);
            UserId = AccountControll.LoginUser(name, password);
            LogSaver.Log("Loged in successfully. UserId="+UserId);
        }

        public void Logoff()
        {
            LogSaver.Log("Logoff userId="+UserId);
            RoomControll.DeleteOnlineUser(UserId);
        }

        public GameState GetState()
        {
            LogSaver.Log("GetState asked userId="+UserId);
            return RoomControll.GetState(UserId);
        }
    }
}
