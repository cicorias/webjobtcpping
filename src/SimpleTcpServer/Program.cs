using SimpleTcpServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {

            var service = new ServerService();

            if (!Environment.UserInteractive)
            {
                var servicesToRun = new ServiceBase[] { service };
                ServiceBase.Run(servicesToRun);
                return;
            }


            int portInt;
            string port;
            Config.GetSettings(out portInt, out port);

            var server = new TcpServer(portInt);

            Console.WriteLine("Running on  tcp://localhost:" + port);
            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine();
            server.Dispose();
        }


    }
}
