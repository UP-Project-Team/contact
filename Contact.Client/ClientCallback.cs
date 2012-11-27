using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Client.GameService;

namespace Contact.Client
{
    class ClientCallback : GameService.IGameServiceCallback
    {
        private ClientControll controll;
        public ClientCallback(ClientControll controll)
        {
            this.controll = controll;
        }

        public void Notify(GameMessage msg)
        {
            LogSaver.Log("Callback invoked");
            controll.ChangeClientView(msg);
        }
    }
}
