using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JobHunt
{
	public partial class JobHunt : Form
	{
		public JobHunt()
		{
			InitializeComponent();
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
			//Application.ExitThread();
		}

		private void btnJobHuntLog_Click(object sender, EventArgs e)
		{
			JobHuntLog jhl = new JobHuntLog();
			if(!jhl.IsError)
				jhl.Show();
		}

		private void JobHunt_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			AboutBox1 abx = new AboutBox1();
			abx.ShowDialog();
			e.Cancel = true;
		}

	}
}
