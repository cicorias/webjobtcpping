using Dx.Ted.Azure;
using Microsoft.Azure.WebJobs;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace TcpPing
{
    public class Job
    {
        static DateTime s_startTime = DateTime.UtcNow;
        static long s_totalCalls = 0;
        static long s_totalErrors = 0;

        [NoAutomaticTrigger]
        public static void RunCalls(TextWriter writer)
        {
            var shutdownFile = Environment.GetEnvironmentVariable("WEBJOBS_SHUTDOWN_FILE");
            var msg = string.Format("NOT ERROR2:  Will be watching file: {0}", shutdownFile);
            Helper.LogMessage(msg, writer);

            string ip;
            int portInt;
            int loopReportCount;
            int loopDelayMS;
            GetArguments(out ip, out portInt, out loopDelayMS, out loopReportCount);

            int i = 0;
            try
            {
                while (JobRunnerShellProgram.Running)
                {
                    s_totalCalls++;

                    if (i % loopReportCount == 0)
                    {
                        Helper.LogMessage(
                            string.Format("in loop at {0} ...", i.ToString()),
                            writer);

                        Helper.LogMessage(
                            LongMessage(),
                            writer);

                    }

                    PingPort(ip, portInt, writer);
                    Thread.Sleep(loopDelayMS);
                    i++;
                }

                Helper.LogMessage("Exiting loop...", writer);
            }
            catch (Exception ex)
            {
                s_totalErrors++;
                ex.LogException(writer);
                Helper.LogMessage(
                    LongMessage(),
                    writer);
            }


            Helper.LogMessage("out of loop...", writer);

        }

        private static string LongMessage()
        {
            var ts = DateTime.UtcNow - s_startTime;

            var totalTimeMsg = ts.ToString(@"hh\:mm\:ss");

            var longMessage = string.Format("Running since:{0}  - total: {1}  - errors: {2}  -- {3}",
                s_startTime.ToString("s"),
                s_totalCalls,
                s_totalErrors,
                totalTimeMsg);

            return longMessage;
        }

        private static void GetArguments(out string ip, out int portInt, out int loopDelayMs, out int loopReportCount)
        {
            var port = ConfigurationManager.AppSettings["sqlPort"];
            ip = ConfigurationManager.AppSettings["sqlIp"];

            if (string.IsNullOrEmpty(port))
                throw new ArgumentNullException("sqlPort", "Missing sqlPort setting");

            if (string.IsNullOrEmpty(ip))
                throw new ArgumentNullException("sqlIp", "Missing sqlPort setting");


            var portParse = int.TryParse(port, out portInt);

            if (!portParse)
                throw new ArgumentException("bad format for port", "sqlPort");


            //Loop stuff
            var loopDelayMsStr = ConfigurationManager.AppSettings["loopDelayMS"];

            if (!int.TryParse(loopDelayMsStr, out loopDelayMs))
                loopDelayMs = 1000;

            var loopReportCountStr = ConfigurationManager.AppSettings["loopReportCount"];

            if (!int.TryParse(loopReportCountStr, out loopReportCount))
                loopReportCount = 10;



        }


        static void PingPort(string server, int port, TextWriter writer)
        {
            try
            {
                var ipAddress = System.Net.IPAddress.Parse(server);
                var socket = new System.Net.Sockets.TcpClient();
                socket.Connect(ipAddress, port);
                if (socket.Connected)
                {
                    socket.Close();
                }
            }
            catch (SocketException ex)
            {
                s_totalErrors++;
                ex.LogException(writer);
                Helper.LogMessage(
                    LongMessage(),
                    writer);
            }
        }

    }
}
