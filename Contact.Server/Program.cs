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
            var service = new ServiceHost(typeof (GameService));
            service.Open();
            Console.WriteLine("Ready");
            Console.ReadLine();
            service.Close();
        }
    }
}
