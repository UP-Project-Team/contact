using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Contact.Client.GameService;

namespace Contact.Client
{
    public static partial class ClientControll
    {
        private static GameServiceClient proxy;
        private static MainWindow mainWindow;
        public static GameView gameState;
        public static UserData Me { get; private set; }

        // actual application start point at the moment
        // MOVE IT SOMEWHERE ELSE? maybe make it static?
        public static void Run()
        {
            // create connection to server and callback
            var instanceContext = new InstanceContext(new ClientCallback());
            proxy = new GameServiceClient(instanceContext);

            // create and show main window
            mainWindow = new MainWindow();
            gameState = new GameView();
            mainWindow.DataContext = gameState;
            gameState.PropertyChanged += mainWindow.gameState_PropertyChanged;

            mainWindow.Show();
        }

        public static async void Login()
        {
            LogSaver.Log("Trying to login");
            try
            {
                Me = await proxy.LoginAsync("dumb", "asd123");
                
                // TODO: do this not here
                GetState();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Logoff()
        {
            proxy.Logoff(Me.Token);
        }

        public static void GetState()
        {
            var state = proxy.GetState(Me.Token);
            gameState.UpdateFromGameState(state);
        }

        public static void StartGame()
        {
            proxy.StartGame(Me.Token);
        }

        public static void GiveCurrentWordVariant(string answer)
        {
            proxy.GiveCurrentWordVariant(Me.Token, answer);
        }

        
    }
}
