namespace SimpleTcpServer
{
    partial class ProjectInstaller
    {
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SimpleServerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SimpleServerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SimpleServerProcessInstaller
            // 
            this.SimpleServerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.SimpleServerProcessInstaller.Password = null;
            this.SimpleServerProcessInstaller.Username = null;
            // 
            // SimpleServerInstaller
            // 
            this.SimpleServerInstaller.DisplayName = "Simple Server";
            this.SimpleServerInstaller.ServiceName = "SimpleTcpService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SimpleServerProcessInstaller,
            this.SimpleServerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SimpleServerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SimpleServerInstaller;
    }
}