using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        public int RoomId { get; set; }

        public IGameServiceCallback Callback { get; private set; }
        public Guid Token { get; private set; }

        // TODO: add construction from DB row
        public User(int id, string name)
        {
            Id = id;
            Name = name;
            Token = Guid.NewGuid();
            // get and store callback on users side
            Callback = OperationContext.Current.GetCallbackChannel<IGameServiceCallback>();
        }
    }
}
