using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace AlTilde {
	public partial class MainWindow : Window {
		public NotifyIcon TrayNotify = new System.Windows.Forms.NotifyIcon();
		private ToolStripMenuItem CloseMenuItem = new ToolStripMenuItem("종료");

		private void InitTray() {
			IntPtr iconHandle = AlTilde.Properties.Resources.Windows.Handle;
			TrayNotify.Icon = System.Drawing.Icon.FromHandle(iconHandle);
			TrayNotify.Visible = true;
			TrayNotify.Text = "AlTilde";

			ContextMenuStrip contextStrip = new ContextMenuStrip();
			contextStrip.Items.Add(CloseMenuItem);
			TrayNotify.ContextMenuStrip = contextStrip;
			
			CloseMenuItem.Click += MenuShutdown_Click;
		}

		private void MenuShutdown_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
