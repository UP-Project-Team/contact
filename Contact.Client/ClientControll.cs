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
    public sealed class ClientControll
    {
        private GameServiceClient proxy;
        private MainWindow mainWindow;
        private GameView gameState;
        private Guid token;

        // actual application start point at the moment
        // MOVE IT SOMEWHERE ELSE?
        public ClientControll()
        {
            // create connection to server and callback
            var instanceContext = new InstanceContext(new ClientCallback(this));
            proxy = new GameServiceClient(instanceContext);

            // create and show main window
            mainWindow = new MainWindow(this);
            gameState = new GameView(mainWindow);

            mainWindow.Show();
        }

        public async void Login()
        {
            LogSaver.Log("Trying to login");
            try
            {
                token = await proxy.LoginAsync("dumb", "asd123");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Logoff()
        {
            proxy.Logoff(token);
        }

        public void GetState()
        {
            gameState.UpdateFromGameState(proxy.GetState(token));
        }

        public void ChangeClientView(GameMessage message)
        {
            // temporary solution
            // TODO: do delegate
            switch (message.actionType)
            {
                case GameMessage.ActionType.UserJoinedRoom:
                    gameState.AddUser(message.actionAgrument as User);
                    LogSaver.Log("User enter room ");
                    break;

                case GameMessage.ActionType.UserLeftRoom:
                    gameState.RemoveUser(message.actionAgrument as User);
                    LogSaver.Log("User left room");
                    break;

                case GameMessage.ActionType.StateChanged:
                    // TODO: do not change textbox directly, use binding
                    mainWindow.txtState.Text = ((GameState.State)message.actionAgrument).ToString();
                    LogSaver.Log("State changed. New = "+gameState.State);
                    break;

                default:
                    throw new NotImplementedException("GameMessage with actionType="+message.actionType+"not Implemented");
            }
        }
    }
}
