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

        public bool Login(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
