using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: remove this and do normal room management
            RoomControll.AddRoom("First and only room");

            var service = new ServiceHost(typeof (GameService));
            service.Open();
            LogSaver.Log("Server started");
            Console.ReadLine();
            service.Close();
            LogSaver.Log("Server shut down");
        }
    }
}
