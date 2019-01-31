namespace SMSServiceTool
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
            this.ServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.StingaLtdSmsTool = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServiceProcessInstaller
            // 
            this.ServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServiceProcessInstaller.Password = null;
            this.ServiceProcessInstaller.Username = null;
            // 
            // StingaLtdSmsTool
            // 
            this.StingaLtdSmsTool.Description = "Read SMS message about infrastructure activities";
            this.StingaLtdSmsTool.DisplayName = "StingaLtd SMS Tool";
            this.StingaLtdSmsTool.ServiceName = "Service1";
            this.StingaLtdSmsTool.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServiceProcessInstaller,
            this.StingaLtdSmsTool});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller StingaLtdSmsTool;
    }
}