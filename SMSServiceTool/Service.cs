using SMSTool;
using System.ServiceProcess;

namespace SMSServiceTool
{
    public partial class Service : ServiceBase
    {
        private SMSTimer tool;
        public Service()
        {
            tool = new SMSTimer();
            InitializeComponent();
        }

        protected override void OnStart(string[] args) { tool.Start(); }

        protected override void OnStop() { tool.Stop(); }
    }
}
