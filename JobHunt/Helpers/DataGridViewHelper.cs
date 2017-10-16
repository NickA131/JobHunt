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
            var colIdx = dataGridView.Columns[columnName].Index;

            dataGridView.Columns.RemoveAt(colIdx);
            dataGridView.Columns.Insert(colIdx, dataGridViewColumn);
        }

        public void RTrim(DataGridView dataGridView, int count)
        {
            for (var i = 0; i < count; i++)
                dataGridView.Columns.RemoveAt(dataGridView.Columns.Count - 1);
        }

        public void SelectLastRow(DataGridView dataGridView)
        {
            SelectRow(dataGridView, dataGridView.Rows.Count - 1);
        }

        public void SelectRow(DataGridView dataGridView, int rowIndex)
        {
            rowIndex = (rowIndex > dataGridView.Rows.Count - 1 ? dataGridView.Rows.Count - 1 : rowIndex);
            if (rowIndex > 0)
                dataGridView.Rows[rowIndex].Selected = true;
        }

        public void SetReadOnly(DataGridView dataGridView, int colIndex)
        {
            dataGridView.Columns[colIndex].ReadOnly = true;
            dataGridView.Columns[colIndex].DefaultCellStyle.BackColor = Color.LightGray;
        }

        public void ConvertColumnToComboBox(DataGridView dataGridView, string columnName, IBindingList dataSource, string displayColumnName)
        {
            var comboBoxColumn = _comboBoxColumnHelper.Create(columnName);
            comboBoxColumn = _comboBoxColumnHelper.Configure(comboBoxColumn, dataSource, columnName, displayColumnName, columnName);
            ReplaceColumn(dataGridView, columnName, comboBoxColumn);
            
        }

    }
}
