using Microsoft.Azure.WebJobs;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Dx.Ted.Azure
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class JobRunnerShellProgram
    {
        private static bool _running = true;
        private static string _shutdownFile;

        public static bool Running
        {
            get { return _running; }
        }

        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        public static void Run(MethodInfo method)
        {
            if (null == method)
            {
                Helper.LogMessage("invalid method supplied", null);
                throw new ArgumentNullException("method");
            }


            var host = new JobHost();
            SetupShutdown();
            
            Helper.LogMessage(
                string.Format(
                "JobHost starting up {0}", 
                method.DeclaringType.FullName), 
                null);
            
            try
            {
                host.Call(method);
            }
            catch (Exception ex)
            {
                ex.LogException(null);
            }
            //host.RunAndBlock();
        }

        private static void SetupShutdown()
        {
            // Get the shutdown file path from the environment
            _shutdownFile = Environment.GetEnvironmentVariable("WEBJOBS_SHUTDOWN_FILE");

            var msg = string.Format("NOT ERROR: Will be watching file: {0}", _shutdownFile);

            Helper.LogMessage("Shutdown File(in watcher): " + msg, null);

            // Setup a file system watcher on that file's directory to know when the file is created

            try
            {
                var fileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(_shutdownFile));
                fileSystemWatcher.Created += OnChanged;
                fileSystemWatcher.Changed += OnChanged;
                fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite;
                fileSystemWatcher.IncludeSubdirectories = false;
                fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                Helper.LogMessage("couldn't setup file watch", null);
                ex.LogException(null);
            }
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.IndexOf(Path.GetFileName(_shutdownFile), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Found the file mark this WebJob as finished
                _running = false;
                Helper.LogMessage("Should be shutting down(from watcher)", null);
            }
        }
    }
}
