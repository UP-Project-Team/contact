using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    [DataContract]
    public class GameState
    {
        [DataMember]
        public readonly List<User> Users = new List<User>();

        public enum State
        {
            HaveCurrentWord,
            HaveCurrentWordVariant
        }

        [DataMember]
        public State state { get; set; }
    }
}
