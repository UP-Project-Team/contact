using System.Collections.ObjectModel;
using System.Linq;
using Contact.Client.GameService;
using System.ComponentModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace Contact.Client
{
    public class GameView : INotifyPropertyChanged
    {
        #region Observable properties
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users 
        { 
            get { return _users; }
            private set
            {
                if (value == _users) return;
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        private ObservableCollection<Room> _rooms = new ObservableCollection<Room>();
        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set
            {
                if(_rooms == value) return;
                _rooms = value;
                OnPropertyChanged("Rooms");
            }
        }
       
        private GameState.State _state;
        public GameState.State State { 
            get { return _state; }
            set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged("State");
            }
        }

        private ObservableCollection<string> _usedWords = new ObservableCollection<string>(); 
        public ObservableCollection<string> UsedWords
        {
            get { return _usedWords; }
            set 
            { 
                if (value == _usedWords) return;
                _usedWords = value;
                OnPropertyChanged("UsedWords");
            }
        }


        private ObservableCollection<string> _chatMessages = new ObservableCollection<string>();
        public ObservableCollection<string> ChatMessages
        {
            get { return _chatMessages; }
            set
            {
                if (value == _chatMessages) return;
                    _chatMessages = value;
                    _chatMessages.CollectionChanged += _chatMessages_CollectionChanged;
                OnPropertyChanged("ChatMessages");
            }
        }

        private string _currentWord;
        public string CurrentWord
        {
            get { return _currentWord; }
            set
            {
                if (value == _currentWord) return;
                _currentWord = value;
                OnPropertyChanged("CurrentWord");
            }
        }

        private string _varOfCurWord;
        public string VarOfCurWord
        {
            get { return _varOfCurWord; }
            set
            {
                if (value == _varOfCurWord) return;
                _varOfCurWord = value;
                OnPropertyChanged("VarOfCurWord");
            }
        }

        private string _question;
        public string Question
        {
            get { return _question; }
            set
            {
                if (value == _question) return;
                _question = value;
                OnPropertyChanged("Question");
            }
        }

        private string _chiefWord;
        public string ChiefWord
        {
            get { return _chiefWord; }
            set
            {
                if (value == _chiefWord) return;
                _chiefWord = value;
                OnPropertyChanged("ChiefWord");
            }
        }

        private int _numberOfOpenChars;
        public int NumberOfOpenChars
        {
            get { return _numberOfOpenChars; }
            set
            {
                if (value == _numberOfOpenChars) return;
                _numberOfOpenChars = value;
                OnPropertyChanged("NumberOfOpenChars");
                OnPropertyChanged("PrimaryWordKnownLetters");
            }
        }


        private string _primaryWord;
        public string PrimaryWord
        {
            get { return _primaryWord; }
            set
            {
                if(value == _primaryWord) return;
                _primaryWord = value;
                OnPropertyChanged("PrimaryWord");
                OnPropertyChanged("PrimaryWordKnownLetters");
            }
        }


        public string PrimaryWordKnownLetters
        {
            get { return PrimaryWord.Substring(0, NumberOfOpenChars); }
        }

        private UserData _me;
        public UserData Me
        {
            get { return _me; }
            set 
            {
                if (value == _me) return;
                _me = value;
                _me.PropertyChanged += _me_PropertyChanged;
                OnPropertyChanged("Me");
            }
        }
        // Watch for changes in UserData and notify
        void _me_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Me."+e.PropertyName);
        }


        private int currentRoomId=-1;
        public int CurrentRoomId
        {
            get { return currentRoomId; }
            set
            {
                if (value == currentRoomId) return;
                currentRoomId = value;
                OnPropertyChanged("CurrentRoomId");
                OnPropertyChanged("CurrentRoom");
            }
        }

        public Room CurrentRoom
        {
            get { return Rooms.SingleOrDefault(room => room.Id == currentRoomId); }
        }

        #endregion

        #region Implementation of InotifyPropertyCHanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public GameView()
        {
            State = GameState.State.NotStarted;
        }


        public void UpdateFromGameState(GameState state)
        {
            Users = new ObservableCollection<User>(state.Users);
            UsedWords = new ObservableCollection<string>(state.UsedWords);

            // maybe some better way exists?
            State = state.state;
            CurrentWord = state.CurrentWord;
            VarOfCurWord = state.VarOfCurWord;
            NumberOfOpenChars = state.NumberOfOpenChars;
            ChiefWord = state.ChiefWord;
            Question = state.Question;
            PrimaryWord = state.PrimaryWord;
        }


        public void AddUser(User user)
        {
            // TODO: synchronization?
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            var res = from t in Users where t.Id == user.Id select t;
            Users.Remove(res.First());
        }

        public void AddChatMessage(string username, string message)
        {
            var rtbChatMessages = ClientControll.mainWindow.rtbChatMessages;

            var usernameColor = Brushes.Green;

            if (username == null)
            {
                username = "[System]";
                usernameColor = Brushes.Red;
            }

            if (rtbChatMessages.Document.Blocks.FirstBlock == null)
                rtbChatMessages.Document.Blocks.Add(new Paragraph());

            var paraInlines = ((Paragraph)(rtbChatMessages.Document.Blocks.FirstBlock)).Inlines;

            var run1 = new Run(username + ": ");
            run1.Foreground = usernameColor;
            paraInlines.Add(run1);

            var run2 = new Run(message);
            run2.Foreground = Brushes.Black;
            paraInlines.Add(run2);

            paraInlines.Add(new Run(System.Environment.NewLine));
            
            rtbChatMessages.ScrollToEnd();

            // ChatMessages.Add(username + ": " + message);
        }

        private void _chatMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // var lstChatMessages = ClientControll.mainWindow.lstChatMessages;
            // lstChatMessages.ScrollIntoView(lstChatMessages.Items[lstChatMessages.Items.Count - 1]);
        }


    }
}
