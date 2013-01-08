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
        public void Notify(GameMessage msg)
        {
            LogSaver.Log("Callback invoked "+msg.actionType.ToString());
            ClientControll.ChangeClientView(msg);
        }
    }
}
