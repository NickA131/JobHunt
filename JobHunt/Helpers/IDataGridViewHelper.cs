using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JobHunt.Helpers
{
    public interface IDataGridViewHelper
    {
        void ReplaceColumn(DataGridView dataGridView, string columnName, DataGridViewColumn dataGridViewColumn);
        void RTrim(DataGridView dataGridView, int count);
        void SelectLastRow(DataGridView dataGridView);
        void SelectRow(DataGridView dataGridView, int rowIndex);
        void SetReadOnly(DataGridView dataGridView, int colIndex);
        void ConvertColumnToComboBox(DataGridView dataGridView, string columnName, IBindingList dataSource, string displayColumnName);
    }
}
