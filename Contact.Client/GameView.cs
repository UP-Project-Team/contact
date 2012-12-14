using System.Collections.ObjectModel;
using System.Linq;
using Contact.Client.GameService;
using System.ComponentModel;

namespace Contact.Client
{
    class GameView : INotifyPropertyChanged
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
        public ObservableCollection<string> UserdWords
        {
            get { return _usedWords; }
            set 
            { 
                if (value == _usedWords) return;
                _usedWords = value;
                OnPropertyChanged("UsedWords");
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
            }
        }

        private string _primaryWordKnownLetters;
        public string PrimaryWordKnownLetters
        {
            get { return _primaryWordKnownLetters; }
            set
            {
                if (value == _primaryWordKnownLetters) return;
                _primaryWordKnownLetters = value;
                OnPropertyChanged("PrimaryWordKnownLetters");
            }
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
            UserdWords = new ObservableCollection<string>(state.UsedWords);

            // maybe some better way exists?
            State = state.state;
            CurrentWord = state.CurrentWord;
            VarOfCurWord = state.VarOfCurWord;
            NumberOfOpenChars = state.NumberOfOpenChars;
            ChiefWord = state.ChiefWord;
            Question = state.Question;
            PrimaryWordKnownLetters = state.PrimaryWordKnownLetters;
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
    }
}
