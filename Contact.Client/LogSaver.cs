using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Contact.Client
{
    class LogSaver
    {
        private static readonly StreamWriter LogWriter;
        static LogSaver()
        {
            try
            {
                string str = Guid.NewGuid().ToString();
                LogWriter = new StreamWriter(str + ".txt");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

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
