using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Client.GameService;
using System.ComponentModel;

namespace Contact.Client
{
    class GameView : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; private set; }

        private GameState.State state;
        public GameState.State State { 
            get { return state; }
            set
            {
                if (value == state) return;
                state = value;
                OnPropertyChanged("State");
            }
        }

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
            Users = new ObservableCollection<User>();
            state = GameState.State.NotStarted;
        }


        public void UpdateFromGameState(GameState state)
        {
            // Update online users
            // Not sure that this is an optimal way. collection notifies listBox on every add operation
            Users.Clear();
            foreach (var user in state.Users)
            {
                Users.Add(user);
            }

            State = state.state;
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
