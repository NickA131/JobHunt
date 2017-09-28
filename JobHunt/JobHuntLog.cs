using System;
using System.ComponentModel;
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
                InitialiseDataGrid();

            }
			catch (Exception ex)
			{
				MessageBox.Show("Error opening table. " + ex.Message, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_isError = true;
				return;
			}
		}

		#endregion

        private void InitialiseDataGrid()
        {
            // Fetch and bind data
            _jobHuntContext.JobHuntLogs.Load();
            dgvJobHuntLog.DataError += new DataGridViewDataErrorEventHandler(dgvJobHuntLog_DataError);
            dgvJobHuntLog.DataSource = _jobHuntContext.JobHuntLogs.Local.ToBindingList();

            // Convert FK fields to ComboBoxes
            CreateComboBoxes();
            
        }

        private void JobHuntLog_Load(object sender, EventArgs e)
        {
            // Prune last 3 columns
            for (var i = 0; i < 3; i++)
                dgvJobHuntLog.Columns.RemoveAt(dgvJobHuntLog.Columns.Count - 1);

            // Set Readonly Columns
            SetReadOnly(0);
            dgvJobHuntLog.Columns[0].Width = 70;
            SetReadOnly(dgvJobHuntLog.Columns.Count - 1);

            // Select last row
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

        #region Private Members
        private void dgvJobHuntLog_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Error happened " + anError.Context.ToString(), "Data Update Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void dgvJobHuntLog_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            var currentRowIdx = dgvJobHuntLog.CurrentRow.Index;
            // Last row and has changed
            if (dgvJobHuntLog.IsCurrentRowDirty && currentRowIdx == dgvJobHuntLog.RowCount -2)
            {
                if ((DateTime)dgvJobHuntLog.CurrentRow.Cells["DateEntered"].Value == DateTime.MinValue)
                    dgvJobHuntLog.CurrentRow.Cells["DateEntered"].Value = DateTime.Now;

            }

        }

        private void SetReadOnly(int colIdx)
        {
            dgvJobHuntLog.Columns[colIdx].ReadOnly = true;
            dgvJobHuntLog.Columns[colIdx].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void CreateComboBoxes()
        {
            var comboBoxColumn = ConvertToComboBoxColumn("WhereFoundId");
            _jobHuntContext.WhereFounds.Load();
            comboBoxColumn.DataSource = _jobHuntContext.WhereFounds.Local.ToBindingList();
            comboBoxColumn.DataPropertyName = "WhereFoundId";
            comboBoxColumn.DisplayMember = "Source";
            comboBoxColumn.ValueMember = "WhereFoundId";

            comboBoxColumn = ConvertToComboBoxColumn("WhoFoundId");
            _jobHuntContext.WhoFounds.Load();
            comboBoxColumn.DataSource = _jobHuntContext.WhoFounds.Local.ToBindingList();
            comboBoxColumn.DataPropertyName = "WhoFoundId";
            comboBoxColumn.DisplayMember = "Source";
            comboBoxColumn.ValueMember = "WhoFoundId";

            comboBoxColumn = ConvertToComboBoxColumn("JobTypeId");
            _jobHuntContext.JobTypes.Load();
            comboBoxColumn.DataSource = _jobHuntContext.JobTypes.Local.ToBindingList();
            comboBoxColumn.DataPropertyName = "JobTypeId";
            comboBoxColumn.DisplayMember = "Type";
            comboBoxColumn.ValueMember = "JobTypeId";

        }

        private DataGridViewComboBoxColumn ConvertToComboBoxColumn(string columnName)
        {
            var colIdx = dgvJobHuntLog.Columns[columnName].Index;
            var cbcColumn = new DataGridViewComboBoxColumn()
            {
                DataPropertyName = columnName,
                HeaderText = (columnName.ToLower().EndsWith("id") ? columnName.Substring(0, columnName.Length - 2) : columnName),
                DropDownWidth = 160,
                Width = 90,
                MaxDropDownItems = 3,
                FlatStyle = FlatStyle.Flat
            };

            dgvJobHuntLog.Columns.RemoveAt(colIdx);
            dgvJobHuntLog.Columns.Insert(colIdx, cbcColumn);

            return (DataGridViewComboBoxColumn)dgvJobHuntLog.Columns[colIdx];
        }
        #endregion

    }
}
