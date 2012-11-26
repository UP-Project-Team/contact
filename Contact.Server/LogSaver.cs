using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Server
{
    class LogSaver
    {
        private static readonly StreamWriter LogWriter = File.AppendText("log.txt");

        public static void Log(string msg)
        {
            var record = String.Format(
                                        "{0} {1}: {2}", 
                                        DateTime.Now.ToLongTimeString(), 
                                        DateTime.Now.ToLongDateString(),
                                        msg
                                        );


            lock (LogWriter)
            {
                Console.WriteLine(record);
                LogWriter.WriteLine(record);
                LogWriter.Flush();
            }
        }
    }
}
