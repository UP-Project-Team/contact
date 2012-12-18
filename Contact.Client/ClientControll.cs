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
    public static class ClientControll
    {
        private static GameServiceClient proxy;
        private static MainWindow mainWindow;
        private static GameView gameState;
        private static UserData me;

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

            mainWindow.Show();
        }

        public static async void Login()
        {
            LogSaver.Log("Trying to login");
            try
            {
                me = await proxy.LoginAsync("dumb", "asd123");
                
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
            proxy.Logoff(me.Token);
        }

        public static void GetState()
        {
            var state = proxy.GetState(me.Token);
            gameState.UpdateFromGameState(state);
        }

        public static void StartGame()
        {
            proxy.StartGame(me.Token);
        }

        public static void GiveCurrentWordVariant(string answer)
        {
            proxy.GiveCurrentWordVariant(me.Token, answer);
        }

        public static void ChangeClientView(GameMessage message)
        {
            // temporary solution
            // TODO: do delegate
            switch (message.actionType)
            {
                case GameMessage.ActionType.UserJoinedRoom:
                    mainWindow.Dispatcher.Invoke(()=>gameState.AddUser(message.actionAgrument as User));
                    LogSaver.Log("User enter room ");
                    break;

                case GameMessage.ActionType.UserLeftRoom:
                    mainWindow.Dispatcher.Invoke(()=>gameState.RemoveUser(message.actionAgrument as User));
                    LogSaver.Log("User left room");
                    break;

                case GameMessage.ActionType.StateChanged:
                    mainWindow.Dispatcher.Invoke(() =>
                        {
                            gameState.State = (GameState.State) message.actionAgrument;
                        });
                    LogSaver.Log("State changed. New = "+gameState.State);
                    break;

                default:
                    throw new NotImplementedException("GameMessage with actionType="+message.actionType+"not Implemented");
            }
        }
    }
}
