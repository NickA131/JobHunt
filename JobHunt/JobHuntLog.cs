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
using NinjaSoftwareLtd.ErrorLogging;

namespace JobHunt
{
	public partial class JobHuntLog : Form
	{
	    protected IJobHuntLogDataGridViewHelper _jobHuntDataGridViewHelper;
        protected IErrorLogger _errorLogger;

		#region Constructors
		public JobHuntLog(IJobHuntLogDataGridViewHelper jobHuntDataGridVewHelper, IErrorLogger errorLogger)
		{
            _jobHuntDataGridViewHelper = jobHuntDataGridVewHelper;
            _errorLogger = errorLogger;
            _errorLogger.ClassName = this.GetType().ToString();

			InitializeComponent();
			
			try
			{
                _jobHuntDataGridViewHelper.Initialise(dgvJobHuntLog);
            }
			catch (Exception ex)
			{
                _errorLogger.LogError(ex.Message, ex);
				MessageBox.Show("Error opening table. " + ex.Message, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		#endregion

        private void JobHuntLog_Load(object sender, EventArgs e)
        {
            _jobHuntDataGridViewHelper.Load(dgvJobHuntLog);
        }

        #region Event Handlers
		private void btnSave_Click(object sender, EventArgs e)
		{
            _jobHuntDataGridViewHelper.SaveChanges();

            this.Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
            this.Hide();
        }

        private void dgvJobHuntLog_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            _jobHuntDataGridViewHelper.DataError(e.Context.ToString(), e.Exception);
            MessageBox.Show("Error happened " + e.Context.ToString(), "Data Update Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void dgvJobHuntLog_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            var currentRowIdx = dataGridView.CurrentRow.Index;

            _jobHuntDataGridViewHelper.RowLeave(dataGridView, "DateEntered", currentRowIdx, dataGridView.IsCurrentRowDirty);
        }
        #endregion
    }
}
