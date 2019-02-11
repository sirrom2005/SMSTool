using SMSTool;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StingaSmsTool
{
    public partial class SMSForm : Form
    {
        private NotifyIcon SysTray;
        private MenuItem menu_action;
        private SMSTimer task = new SMSTimer();
        private bool IsRunning;

        public SMSForm()
        {
            InitializeComponent();
            ContextMenu contextMenu = new ContextMenu();

            menu_action = new MenuItem();
            menu_action.Index = 0;
            menu_action.Text  = "Start";
            menu_action.Click += new EventHandler(MenuActionClick);

            MenuItem menu_exit = new MenuItem();
            menu_exit.Index = 5;
            menu_exit.Text  = "Exit";
            menu_exit.Click += new EventHandler(MenuExitClick);

            contextMenu.MenuItems.AddRange(new MenuItem[] { menu_action, menu_exit });
   
            // Create the NotifyIcon.
            SysTray = new NotifyIcon()
            {
                Icon    = new Icon("icon.ico"),
                Text    = "SMS Service",
                Visible = true,
                ContextMenu     = contextMenu,
                BalloonTipIcon  = ToolTipIcon.Info,
                BalloonTipTitle = "SMS Server",
                BalloonTipText  = "Stinga SMS Service"
            };
        }

        private void MenuExitClick(object sender, EventArgs e)
        {
            task.Stop();
            Dispose(true);
        }

        private void MenuActionClick(object sender, EventArgs e)
        {
            if(IsRunning)
            {
                menu_action.Text = "Start";
                IsRunning   = false;
                SysTray.Text= "SMS Service Stopped";
                task.Stop();
            }
            else {
                menu_action.Text = "Stop";
                IsRunning   = true;
                SysTray.Text= "SMS Service Started";
                task.Start();
            }
        }
    }
}
