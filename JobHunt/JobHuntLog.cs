using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JobHunt.Helpers;
using JobHuntData;

namespace JobHunt
{
	public partial class JobHuntLog : Form
	{
	    protected IJobHuntLogDataGridViewHelper _jobHuntDataGridViewHelper;

		#region Constructors
		public JobHuntLog(IJobHuntLogDataGridViewHelper jobHuntDataGridVewHelper)
		{
            _jobHuntDataGridViewHelper = jobHuntDataGridVewHelper;

			InitializeComponent();
			
			try
			{
                _jobHuntDataGridViewHelper.Initialise(dgvJobHuntLog);
            }
			catch (Exception ex)
			{
				MessageBox.Show("Error opening table. " + ex.Message, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		#endregion

        private void JobHuntLog_Load(object sender, EventArgs e)
        {
            // Prune last 3 columns
            _jobHuntDataGridViewHelper.RTrim(dgvJobHuntLog, 3);

            // Set Readonly Columns
            _jobHuntDataGridViewHelper.SetReadOnly(dgvJobHuntLog, 0);
            //dgvJobHuntLog.Columns[0].Width = 70;
            _jobHuntDataGridViewHelper.SetReadOnly(dgvJobHuntLog, dgvJobHuntLog.Columns.Count - 1);

            // Select last row
            _jobHuntDataGridViewHelper.SelectLastRow(dgvJobHuntLog);

        }

		private void btnSave_Click(object sender, EventArgs e)
		{
            _jobHuntDataGridViewHelper.SaveChanges();

            this.Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
            this.Hide();
		}
    }
}
