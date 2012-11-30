using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Client.GameService;

namespace Contact.Client
{
    class GameView
    {
        private readonly ObservableCollection<User> users = new ObservableCollection<User>();
        public string State { get; set; } // TODO: store it as enum, not as string

        public GameView(MainWindow window)
        {
            //bind data to window
            window.lstUsersOnline.DataContext = users;
            window.txtState.DataContext = State;
        }

        public void UpdateFromGameState(GameState state)
        {
            // Update online users
            // Not sure that this is an optimal way. collection notifies listBox on every add operation
            users.Clear();
            foreach (var user in state.Users)
            {
                users.Add(user);
            }
        }

        public void AddUser(User user)
        {
            // TODO: synchronization?
            users.Add(user);
        }

        public void RemoveUser(User user)
        {
            var res = from t in users where t.Id == user.Id select t;
            users.Remove(res.First());
        }
    }
}
