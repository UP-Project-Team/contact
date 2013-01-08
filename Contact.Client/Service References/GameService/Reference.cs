﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Contact.Client.GameService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserData", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
    [System.SerializableAttribute()]
    public partial class UserData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RoomIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid TokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Contact.Client.GameService.User.Role roleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RoomId {
            get {
                return this.RoomIdField;
            }
            set {
                if ((this.RoomIdField.Equals(value) != true)) {
                    this.RoomIdField = value;
                    this.RaisePropertyChanged("RoomId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Token {
            get {
                return this.TokenField;
            }
            set {
                if ((this.TokenField.Equals(value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Contact.Client.GameService.User.Role role {
            get {
                return this.roleField;
            }
            set {
                if ((this.roleField.Equals(value) != true)) {
                    this.roleField = value;
                    this.RaisePropertyChanged("role");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Contact.Client.GameService.User.Role roleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Contact.Client.GameService.User.Role role {
            get {
                return this.roleField;
            }
            set {
                if ((this.roleField.Equals(value) != true)) {
                    this.roleField = value;
                    this.RaisePropertyChanged("role");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="User.Role", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        public enum Role : int {
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Host = 0,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Qwestioner = 1,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            Contacter = 2,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            None = 3,
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameException", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
    [System.SerializableAttribute()]
    public partial class GameException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameState", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
    [System.SerializableAttribute()]
    public partial class GameState : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ChiefWordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CurrentWordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfOpenCharsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrimaryWordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string QuestionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] UsedWordsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Contact.Client.GameService.User[] UsersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VarOfCurWordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Contact.Client.GameService.GameState.State stateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ChiefWord {
            get {
                return this.ChiefWordField;
            }
            set {
                if ((object.ReferenceEquals(this.ChiefWordField, value) != true)) {
                    this.ChiefWordField = value;
                    this.RaisePropertyChanged("ChiefWord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CurrentWord {
            get {
                return this.CurrentWordField;
            }
            set {
                if ((object.ReferenceEquals(this.CurrentWordField, value) != true)) {
                    this.CurrentWordField = value;
                    this.RaisePropertyChanged("CurrentWord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfOpenChars {
            get {
                return this.NumberOfOpenCharsField;
            }
            set {
                if ((this.NumberOfOpenCharsField.Equals(value) != true)) {
                    this.NumberOfOpenCharsField = value;
                    this.RaisePropertyChanged("NumberOfOpenChars");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PrimaryWord {
            get {
                return this.PrimaryWordField;
            }
            set {
                if ((object.ReferenceEquals(this.PrimaryWordField, value) != true)) {
                    this.PrimaryWordField = value;
                    this.RaisePropertyChanged("PrimaryWord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Question {
            get {
                return this.QuestionField;
            }
            set {
                if ((object.ReferenceEquals(this.QuestionField, value) != true)) {
                    this.QuestionField = value;
                    this.RaisePropertyChanged("Question");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] UsedWords {
            get {
                return this.UsedWordsField;
            }
            set {
                if ((object.ReferenceEquals(this.UsedWordsField, value) != true)) {
                    this.UsedWordsField = value;
                    this.RaisePropertyChanged("UsedWords");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Contact.Client.GameService.User[] Users {
            get {
                return this.UsersField;
            }
            set {
                if ((object.ReferenceEquals(this.UsersField, value) != true)) {
                    this.UsersField = value;
                    this.RaisePropertyChanged("Users");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VarOfCurWord {
            get {
                return this.VarOfCurWordField;
            }
            set {
                if ((object.ReferenceEquals(this.VarOfCurWordField, value) != true)) {
                    this.VarOfCurWordField = value;
                    this.RaisePropertyChanged("VarOfCurWord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Contact.Client.GameService.GameState.State state {
            get {
                return this.stateField;
            }
            set {
                if ((this.stateField.Equals(value) != true)) {
                    this.stateField = value;
                    this.RaisePropertyChanged("state");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="GameState.State", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        public enum State : int {
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            NotStarted = 0,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            HaveCurrentWord = 1,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            HaveCurrentWordVariant = 2,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            VotingForHostWord = 3,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            VotingForPlayersWords = 4,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            GameOver = 5,
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameMessage", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.UserData))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.User.Role))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.GameException))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.GameState))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.User[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.User))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.GameState.State))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Contact.Client.GameService.GameMessage.ActionType))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(string[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Tuple<Contact.Client.GameService.User, Contact.Client.GameService.User.Role>))]
    public partial class GameMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object actionAgrumentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Contact.Client.GameService.GameMessage.ActionType actionTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object actionAgrument {
            get {
                return this.actionAgrumentField;
            }
            set {
                if ((object.ReferenceEquals(this.actionAgrumentField, value) != true)) {
                    this.actionAgrumentField = value;
                    this.RaisePropertyChanged("actionAgrument");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Contact.Client.GameService.GameMessage.ActionType actionType {
            get {
                return this.actionTypeField;
            }
            set {
                if ((this.actionTypeField.Equals(value) != true)) {
                    this.actionTypeField = value;
                    this.RaisePropertyChanged("actionType");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
        [System.Runtime.Serialization.DataContractAttribute(Name="GameMessage.ActionType", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        public enum ActionType : int {
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            UserLeftRoom = 0,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            UserJoinedRoom = 1,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            StateChanged = 2,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            VarOfCurWordChanged = 3,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            UserRoleChanged = 4,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            PrimaryWordCharOpened = 5,
            
            [System.Runtime.Serialization.EnumMemberAttribute()]
            UsedWordAdded = 6,
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameService.IGameService", CallbackContract=typeof(Contact.Client.GameService.IGameServiceCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Login", ReplyAction="http://tempuri.org/IGameService/LoginResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Contact.Client.GameService.GameException), Action="http://tempuri.org/IGameService/LoginGameExceptionFault", Name="GameException", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        Contact.Client.GameService.UserData Login(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Login", ReplyAction="http://tempuri.org/IGameService/LoginResponse")]
        System.Threading.Tasks.Task<Contact.Client.GameService.UserData> LoginAsync(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, IsInitiating=false, Action="http://tempuri.org/IGameService/Logoff")]
        void Logoff(System.Guid token);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsTerminating=true, IsInitiating=false, Action="http://tempuri.org/IGameService/Logoff")]
        System.Threading.Tasks.Task LogoffAsync(System.Guid token);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/IGameService/GetState", ReplyAction="http://tempuri.org/IGameService/GetStateResponse")]
        Contact.Client.GameService.GameState GetState(System.Guid token);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/IGameService/GetState", ReplyAction="http://tempuri.org/IGameService/GetStateResponse")]
        System.Threading.Tasks.Task<Contact.Client.GameService.GameState> GetStateAsync(System.Guid token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Registration", ReplyAction="http://tempuri.org/IGameService/RegistrationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Contact.Client.GameService.GameException), Action="http://tempuri.org/IGameService/RegistrationGameExceptionFault", Name="GameException", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        void Registration(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Registration", ReplyAction="http://tempuri.org/IGameService/RegistrationResponse")]
        System.Threading.Tasks.Task RegistrationAsync(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="http://tempuri.org/IGameService/StartGame")]
        void StartGame(System.Guid token);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, IsInitiating=false, Action="http://tempuri.org/IGameService/StartGame")]
        System.Threading.Tasks.Task StartGameAsync(System.Guid token);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/IGameService/GiveCurrentWordVariant", ReplyAction="http://tempuri.org/IGameService/GiveCurrentWordVariantResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Contact.Client.GameService.GameException), Action="http://tempuri.org/IGameService/GiveCurrentWordVariantGameExceptionFault", Name="GameException", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        void GiveCurrentWordVariant(System.Guid token, string word);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/IGameService/GiveCurrentWordVariant", ReplyAction="http://tempuri.org/IGameService/GiveCurrentWordVariantResponse")]
        System.Threading.Tasks.Task GiveCurrentWordVariantAsync(System.Guid token, string word);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/IGameService/VoteForPlayerWord", ReplyAction="http://tempuri.org/IGameService/VoteForPlayerWordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Contact.Client.GameService.GameException), Action="http://tempuri.org/IGameService/VoteForPlayerWordGameExceptionFault", Name="GameException", Namespace="http://schemas.datacontract.org/2004/07/Contact.Server")]
        void VoteForPlayerWord(System.Guid token, int wordId, bool up);
        
        [System.ServiceModel.OperationContractAttribute(IsInitiating=false, Action="http://tempuri.org/IGameService/VoteForPlayerWord", ReplyAction="http://tempuri.org/IGameService/VoteForPlayerWordResponse")]
        System.Threading.Tasks.Task VoteForPlayerWordAsync(System.Guid token, int wordId, bool up);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/Notify")]
        void Notify(Contact.Client.GameService.GameMessage msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceChannel : Contact.Client.GameService.IGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameServiceClient : System.ServiceModel.DuplexClientBase<Contact.Client.GameService.IGameService>, Contact.Client.GameService.IGameService {
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public Contact.Client.GameService.UserData Login(string name, string password) {
            return base.Channel.Login(name, password);
        }
        
        public System.Threading.Tasks.Task<Contact.Client.GameService.UserData> LoginAsync(string name, string password) {
            return base.Channel.LoginAsync(name, password);
        }
        
        public void Logoff(System.Guid token) {
            base.Channel.Logoff(token);
        }
        
        public System.Threading.Tasks.Task LogoffAsync(System.Guid token) {
            return base.Channel.LogoffAsync(token);
        }
        
        public Contact.Client.GameService.GameState GetState(System.Guid token) {
            return base.Channel.GetState(token);
        }
        
        public System.Threading.Tasks.Task<Contact.Client.GameService.GameState> GetStateAsync(System.Guid token) {
            return base.Channel.GetStateAsync(token);
        }
        
        public void Registration(string name, string password) {
            base.Channel.Registration(name, password);
        }
        
        public System.Threading.Tasks.Task RegistrationAsync(string name, string password) {
            return base.Channel.RegistrationAsync(name, password);
        }
        
        public void StartGame(System.Guid token) {
            base.Channel.StartGame(token);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(System.Guid token) {
            return base.Channel.StartGameAsync(token);
        }
        
        public void GiveCurrentWordVariant(System.Guid token, string word) {
            base.Channel.GiveCurrentWordVariant(token, word);
        }
        
        public System.Threading.Tasks.Task GiveCurrentWordVariantAsync(System.Guid token, string word) {
            return base.Channel.GiveCurrentWordVariantAsync(token, word);
        }
        
        public void VoteForPlayerWord(System.Guid token, int wordId, bool up) {
            base.Channel.VoteForPlayerWord(token, wordId, up);
        }
        
        public System.Threading.Tasks.Task VoteForPlayerWordAsync(System.Guid token, int wordId, bool up) {
            return base.Channel.VoteForPlayerWordAsync(token, wordId, up);
        }
    }
}
