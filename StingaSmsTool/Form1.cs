using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StingaSmsTool
{
    public partial class SMSForm : Form
    {
        public SMSForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;
            Rectangle res = Screen.PrimaryScreen.Bounds;
            /*Left = res.Width - 420;
            Top = res.Height - 320;*/
            for(int i=0; i<=40; i++)
            {
                combo_box_port.Items.Add($"COM {i}");
            }

            Container components = new Container();
            components.Add(this);

            // Create the NotifyIcon.
            NotifyIcon SysTray = new NotifyIcon(components)
            {
                Icon    = new Icon("C:\\Users\\rohanmorris\\source\\repos\\SMSTool\\StingaSmsTool\\Res\\icon.ico"),
                Text    = "Form1",
                Visible = true,
            };
            SysTray.DoubleClick += new EventHandler(this.notifyIconDoubleClick);
        }

        private void notifyIconDoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
        }
    }
}
