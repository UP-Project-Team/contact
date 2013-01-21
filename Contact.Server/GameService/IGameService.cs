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
        UserData Login(string name, string password);

        [OperationContract(IsTerminating = true, IsInitiating = false, IsOneWay = true)]
        void Logoff(Guid token);

        [OperationContract(IsInitiating = false, IsTerminating = false, IsOneWay=false)]
        GameState GetState(Guid token);

        [OperationContract(IsInitiating = false, IsTerminating = false, IsOneWay = false)]
        List<Room> GetRoomsList(Guid token);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void GotoRoom(Guid token, int roomId);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void GiveChiefWord(Guid token, string word);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void AddRoom(Guid token, string roomName);

        [OperationContract(IsInitiating = true, IsTerminating = false, IsOneWay=false)]
        [FaultContract(typeof(GameException))]
        void Registration(string name, string password);

        [OperationContract(IsInitiating = false, IsOneWay = true, IsTerminating = false)]
        void StartGame(Guid token);

        [OperationContract(IsInitiating = false, IsOneWay = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void LeaveRoom(Guid token);

        [OperationContract(IsInitiating = false, IsOneWay = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void GiveCurrentWordVariant(Guid token, string word);

        [OperationContract(IsInitiating = false, IsOneWay = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void VoteForPlayerWord(Guid token, int wordId, bool up);

        [OperationContract(IsInitiating = false, IsOneWay = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void VoteForChiefWord(Guid token, bool up);
        
        [OperationContract(IsInitiating = false, IsOneWay = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void AskQuestion(Guid token, string question, string word);

        [OperationContract(IsInitiating = false, IsOneWay = false, IsTerminating = false)]
        [FaultContract(typeof(GameException))]
        void SetPrimaryWord(Guid token, string primaryWord);
    }
    
    //User callback
    public interface IGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Notify(GameMessage msg);
    }


    // User callback Message
    [DataContract]
    [KnownType(typeof(User))]
    [KnownType(typeof(GameState.State))]
    [KnownType(typeof(User.Role))]
    [KnownType(typeof(Tuple<User, User.Role>))]
    [KnownType(typeof(Tuple<string, string>))]
    [KnownType(typeof(Tuple<string, int>))]
    public class GameMessage
    {
        public enum ActionType
        {
            UserLeftRoom,
            UserJoinedRoom,
            StateChanged,
            VarOfCurWordChanged,
            UserRoleChanged,
            PrimaryWordGiven,
            PrimaryWordCharOpened,
            UsedWordAdded,
            QuestionAsked,
            LogoffUser,
            WeHaveChiefWord,
            AddedRoom
        }

        [DataMember]
        public ActionType actionType { get; private set; }

        [DataMember] 
        public object actionAgrument { get; private set; }

        #region Message Constructors
        public static GameMessage QuestionAsked(string question, string word)
        {
            return new GameMessage {actionType = ActionType.QuestionAsked, actionAgrument = new Tuple<string, string>(question, word)};
        }
        public static GameMessage WeHaveChiefWord(string word)
        {
            return new GameMessage { actionType = ActionType.WeHaveChiefWord, actionAgrument = word }; 
        }
        public static GameMessage AddedRoom(string Name, int Id)
        {
            return new GameMessage { actionType = ActionType.AddedRoom, actionAgrument = new Tuple<string, int>(Name, Id) };
        }
        public static GameMessage LogoffUser()
        {
            return new GameMessage { actionType = ActionType.LogoffUser };
        }
        public static GameMessage UsedWordAddedMessage(string word)
        {
            return new GameMessage {actionType = ActionType.UsedWordAdded, actionAgrument = word};
        }

        public static GameMessage PrimaryWordCharOpened()
        {
            return new GameMessage { actionType = ActionType.PrimaryWordCharOpened };
        }

        public static GameMessage PrimaryWordGiven(string primaryWord)
        {
            return new GameMessage { actionType = ActionType.PrimaryWordGiven, actionAgrument = primaryWord };
        }

        public static GameMessage UserRoleChangedMessage(User user, User.Role role)
        {
            return new GameMessage { actionType = ActionType.UserRoleChanged, actionAgrument  = new Tuple<User, User.Role>(user, role) };
        }

        public static GameMessage UserLeftRoomMessage(User user)
        {
            return new GameMessage {actionType = ActionType.UserLeftRoom, actionAgrument = user};
        }

        public static GameMessage UserJoinedRoomMessage(User user)
        {
            return new GameMessage {actionType = ActionType.UserJoinedRoom, actionAgrument = user};
        }

        public static GameMessage StateChangedMessage(GameState.State state)
        {
            return new GameMessage {actionType = ActionType.StateChanged, actionAgrument = state};
        }

        public static GameMessage VarOfCurWordChangedMessage(string word)
        {
            return new GameMessage {actionType = ActionType.VarOfCurWordChanged, actionAgrument = word};
        }
        #endregion
    }
}
