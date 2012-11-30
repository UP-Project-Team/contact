﻿using System;
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
    [KnownType(typeof(User))]
    [KnownType(typeof(GameState.State))]
    public class GameMessage
    {
        public enum ActionType
        {
            UserLeftRoom,
            UserJoinedRoom,
            StateChanged
        }

        [DataMember]
        public ActionType actionType { get; private set; }

        [DataMember] 
        public object actionAgrument { get; private set; }

        #region Message Constructors
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
        #endregion
    }
}