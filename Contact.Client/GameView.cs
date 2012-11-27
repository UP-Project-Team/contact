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

        public GameView(MainWindow window)
        {
            //bind data to window
            window.lstUsersOnline.DataContext = users;
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
    }
}
