using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using NickUtils;

namespace JobHunt
{
	public partial class JobHuntLog : Form
	{
		private DataManager _dm = new DataManager(ConfigurationManager.ConnectionStrings["JobHuntConnection"].ConnectionString);
		private DataTable _dt = null;
		private SqlDataAdapter _sda = null;
		private Boolean _IsError = false;

		#region Get and Set
		public Boolean IsError
		{
			get { return _IsError; }
		}
		#endregion

		#region Constructors
		// Hide default contsructor
		public JobHuntLog()
		{
			InitializeComponent();
			
			try
			{
				_dt = _dm.GetDataTable(_dm.RunSQLQuery("SELECT * FROM  JobHuntLog ORDER BY JobHuntId"));
				dgvJobHuntLog.DataSource = _dt;
				_sda = _dm.Sda;
				SqlCommandBuilder builder = new SqlCommandBuilder(_sda);
				//MessageBox.Show(builder.GetInsertCommand().CommandText);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error opening table. " + ex.Message, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_IsError = true;
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
			_sda.Update(_dt);

			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}
