using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Contact.Server
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof (IGameServiceCallback))]
    public interface IGameService
    {
        [OperationContract(IsInitiating = true)]
        [FaultContract(typeof(GameException))]
        void Login(string name, string password);

        [OperationContract(IsTerminating = true, IsInitiating = false, IsOneWay = true)]
        void Logoff();

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        GameState GetState();
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
        public ActionType actionType { get; private set; }

        [DataMember] 
        public ActionAgrument actionAgrument { get; private set; }


        public static GameMessage UserLeftRoomMessage(User user)
        {
            var args = new UserData {user = user};
            return new GameMessage {actionType = ActionType.UserLeftRoom, actionAgrument = args};
        }

        public static GameMessage UserJoinedRoomMessage(User user)
        {
            var args = new UserData {user = user};
            return new GameMessage {actionType = ActionType.UserJoinedRoom, actionAgrument = args};
        }
    }

    [DataContract]
    [KnownType(typeof(UserData))]
    public abstract class ActionAgrument {}

    //Возможно стоит просто занаследовать юзера от ActionArgument и не городит огород
    //Пока оставляю для единообразия
    [DataContract]
    public class UserData : ActionAgrument
    {
        [DataMember]
        public User user { get; set; }
    }
}
