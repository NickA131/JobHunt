using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobHunt.Helpers
{
    public class DataGridViewHelper : IDataGridViewHelper
    {
        protected IComboBoxColumnHelper _comboBoxColumnHelper;

        public DataGridViewHelper(IComboBoxColumnHelper comboBoxColumnHelper)
        {
            _comboBoxColumnHelper = comboBoxColumnHelper;
        }

        public void ReplaceColumn(DataGridView dataGridView, string columnName, DataGridViewColumn dataGridViewColumn)
        {
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("ColumnName is null or empty");
            if (dataGridViewColumn == null)
                throw new ArgumentNullException("DataGridViewColumn is null");

            // Check column exists
            if (!dataGridView.Columns.Contains(columnName))
                throw new ArgumentException(string.Format("Specified column '{0}' does not exist in gridView", columnName));

            var colIdx = dataGridView.Columns[columnName].Index;
            dataGridView.Columns.RemoveAt(colIdx);
            dataGridView.Columns.Insert(colIdx, dataGridViewColumn);
        }

        public void RTrim(DataGridView dataGridView, int count)
        {
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero");
            if(count > dataGridView.ColumnCount)
                throw new ArgumentException("Count must be less than total columns");

            for (var i = 0; i < count; i++)
                dataGridView.Columns.RemoveAt(dataGridView.Columns.Count - 1);
        }

        public void SelectLastRow(DataGridView dataGridView)
        {
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");

            SelectRow(dataGridView, dataGridView.Rows.Count - 1);
        }

        public void SelectRow(DataGridView dataGridView, int rowIndex)
        {
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");

            
            rowIndex = (rowIndex > dataGridView.RowCount - 1 ? dataGridView.RowCount - 1 : rowIndex);
            if (rowIndex <= 1)
                throw new ArgumentException("DataGridView has no data rows");
            
            dataGridView.Rows[rowIndex].Selected = true;
        }

        public void SetReadOnly(DataGridView dataGridView, int colIndex)
        {
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");
            if (colIndex < 0)
                throw new ArgumentException("ColIdx is less than zero");

            if (dataGridView.ColumnCount == 0)
                throw new ArgumentException("DataGridView has no columns");
            if (colIndex >= dataGridView.ColumnCount)
                throw new ArgumentException("ColIdx is exceeds number of columns in DataGridView");
            
            dataGridView.Columns[colIndex].ReadOnly = true;
            dataGridView.Columns[colIndex].DefaultCellStyle.BackColor = Color.LightGray;
        }

        public void ConvertColumnToComboBox(DataGridView dataGridView, string columnName, IBindingList dataSource, string displayColumnName)
        {
            if (_comboBoxColumnHelper == null)
                throw new NullReferenceException("ComboBoxColumnHelper is null");

            var comboBoxColumn = _comboBoxColumnHelper.Create(columnName);
            comboBoxColumn = _comboBoxColumnHelper.Configure(comboBoxColumn, dataSource, columnName, displayColumnName, columnName);
            ReplaceColumn(dataGridView, columnName, comboBoxColumn);
            
        }

    }
}
