using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{

    // Throws to user via FaultContract when something wrong happens
    [DataContract]
    public class GameException
    {
        [DataMember]
        public string Message { get; set; }

        public static void Throw(string message)
        {
            var exception = new GameException { Message = message };
            throw new FaultException<GameException>(exception);
        }
    }
}
