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

        public void Login()
        {
            LogSaver.Log("Trying to login");
            try
            {
                proxy.Login("dumb", "asd123");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Logoff()
        {
            proxy.Logoff();
        }

        public void GetState()
        {
            gameState.UpdateFromGameState(proxy.GetState());
        }

        public void ChangeClientView(GameMessage message)
        {
            // temporary solution
            // TODO: do delegate
            switch (message.actionType)
            {
                case GameMessage.ActionType.UserJoinedRoom:
                    gameState.AddUser((message.actionAgrument as UserData).user);
                    LogSaver.Log("User enter room ");
                    break;

                case GameMessage.ActionType.UserLeftRoom:
                    gameState.RemoveUser((message.actionAgrument as UserData).user);
                    LogSaver.Log("User left room");
                    break;

                default:
                    throw new NotImplementedException("GameMessage with actionType="+message.actionType+"not Implemented");
            }
        }
    }
}
