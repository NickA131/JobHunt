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
	public partial class EditDataTable : Form
	{
		private DataManager _dm = new DataManager(ConfigurationManager.ConnectionStrings["JobHuntConnection"].ConnectionString);
		private DataTable _dt = null;
		private SqlDataAdapter _sda = null;
		private String _tableName = String.Empty;
		private String _primaryKey= String.Empty;
		private String _fieldList = String.Empty;
		private FormInitCtrl _fmIC = null;
		private Boolean _IsError = false;

		#region Get and Set
		public Boolean IsError
		{
			get { return _IsError; }
		}
		#endregion

		#region Constructors
		// Hide default contsructor
		protected EditDataTable()
		{
		}

		public EditDataTable(String tableName, String primaryKey, String fieldList) :
			this (tableName, primaryKey, fieldList, "Edit Table")
		{ } // Do nothing extra

		public EditDataTable(String tableName, String primaryKey, String fieldList, String formTitle) : 
			this (tableName, primaryKey, fieldList, formTitle, null)
		{ } // Do nothing extra

		public EditDataTable(String tableName, String primaryKey, String fieldList, String formTitle, FormInitCtrl fmIC)
		{
			InitializeComponent();

			if ((tableName == null) || (tableName == String.Empty)
				|| (primaryKey == null) || (primaryKey == String.Empty)
				|| (fieldList == null) || (fieldList == String.Empty))
			{
				MessageBox.Show("Invalid Arguments Supplied. Cannot open this form.", "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_IsError = true;
				return;
			}

			_tableName = tableName;
			_primaryKey = primaryKey;
			_fieldList = fieldList;
			this.Text = formTitle;
			_fmIC = fmIC;

			try
			{
				_dt = _dm.GetDataTable(_dm.RunSQLQuery("SELECT " + _primaryKey + "," + _fieldList + " FROM " + _tableName + " ORDER BY " + _primaryKey));
				dgvContactTypes.DataSource = _dt;
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

		private void EditTypes_Load(object sender, EventArgs e)
		{
			dgvContactTypes.Columns[0].ReadOnly = true;
			dgvContactTypes.Columns[0].Width = 70;
			dgvContactTypes.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
			if (dgvContactTypes.Rows.Count > 0)
				dgvContactTypes.Rows[dgvContactTypes.Rows.Count - 1].Selected = true;

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			_sda.Update(_dt);

			if (_fmIC != null)
				_fmIC.RefreshControls();

			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}


	}
}
