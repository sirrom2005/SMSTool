using SMSTool;
using System.ServiceProcess;

namespace SMSServiceTool
{
    public partial class SMSService : ServiceBase
    {
        private SMSTimer tool;
        public SMSService()
        {
            InitializeComponent();
            tool = new SMSTimer();
        }

        protected override void OnStart(string[] args) { tool.Start(); }

        protected override void OnStop() { tool.Stop(); }
    }
}
