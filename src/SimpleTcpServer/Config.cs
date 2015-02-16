using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTcpServer
{
    public class Config
    {
        public static void GetSettings(out int portInt, out string port)
        {
            portInt = 8999;
            port = ConfigurationManager.AppSettings["serverPort"];
            if (!string.IsNullOrEmpty(port))
            {
                int.TryParse(port, out portInt);
            }
        }
    }
}
