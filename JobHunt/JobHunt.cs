using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JobHunt.Helpers;

namespace JobHunt
{
	public partial class JobHunt : Form
	{
        protected JobHuntLog _jobHuntLog;

		public JobHunt(IJobHuntLogDataGridViewHelper jobHuntLogDataGridViewHelper)
		{
            _jobHuntLog = new JobHuntLog(jobHuntLogDataGridViewHelper);

			InitializeComponent();
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnJobHuntLog_Click(object sender, EventArgs e)
		{
            _jobHuntLog.Show();
		}

		private void JobHunt_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			var abx = new AboutBox1();
			abx.ShowDialog();
			e.Cancel = true;
		}

	}
}
