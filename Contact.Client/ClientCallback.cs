using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Client
{
    class ClientCallback : GameService.IGameServiceCallback
    {
        private ClientControll controll;
        public ClientCallback(ClientControll controll)
        {
            this.controll = controll;
        }

        public void Notify(GameService.GameMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
