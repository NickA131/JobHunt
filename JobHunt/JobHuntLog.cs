using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JobHuntData;

namespace JobHunt
{
	public partial class JobHuntLog : Form
	{
	    private readonly JobHuntEntities _jobHuntContext = new JobHuntEntities();
		private readonly Boolean _isError;

		#region Get and Set
		public Boolean IsError
		{
			get { return _isError; }
		}
		#endregion

		#region Constructors
		// Hide default contsructor
		public JobHuntLog()
		{
			InitializeComponent();
			
			try
			{
                _jobHuntContext.JobHuntLogs.Load();
			    dgvJobHuntLog.DataSource = _jobHuntContext.JobHuntLogs.Local.ToBindingList();
			    
            
            }
			catch (Exception ex)
			{
				MessageBox.Show("Error opening table. " + ex.Message, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_isError = true;
				return;
			}
		}

		#endregion

		private void JobHuntLog_Load(object sender, EventArgs e)
		{
			dgvJobHuntLog.Columns[0].ReadOnly = true;
			dgvJobHuntLog.Columns[0].Width = 70;
			dgvJobHuntLog.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
			if (dgvJobHuntLog.Rows.Count > 0)
				dgvJobHuntLog.Rows[dgvJobHuntLog.Rows.Count - 1].Selected = true;

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
            _jobHuntContext.SaveChanges();

			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}



	}
}
