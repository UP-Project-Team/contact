using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Contact.Server
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IGameServiceCallback))]
    public interface IGameService
    {
        [OperationContract(IsInitiating = true)]
        bool Login(string name, string password);

    }


    //User callback
    public interface IGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Notify(GameMessage msg);
    }


    // User callback Message
    [DataContract]
    public class GameMessage
    {
        public enum ActionType
        {
            UserLeftRoom,
            UserJoinedRoom
        }

        [DataMember]
        private ActionType actionType;

        [DataMember] 
        private ActionAgrument actionAgrument;
    }

    [DataContract]
    public abstract class ActionAgrument {}
}
