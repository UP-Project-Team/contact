using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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
            proxy.Login("dumb", "asd123");
        }

        public void Logoff()
        {
            proxy.Logoff();
        }

        public void GetState()
        {
            gameState.UpdateFromGameState(proxy.GetState());
        }
    }
}
