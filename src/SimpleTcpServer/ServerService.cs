using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceProcess;
using System.Threading;
using SimpleTcpServer.Server;


namespace SimpleTcpServer
{
    public partial class ServerService : ServiceBase
    {
        TcpServer _server;
        public ServerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            int portInt;
            string port;
            Config.GetSettings(out portInt, out port);

            _server = new TcpServer(portInt);
        }

        protected override void OnStop()
        {
            _server.Running = false;
        }


        /// Stolen from designer stuff

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "SimpleTcpService";
        }

    }


}
