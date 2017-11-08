using System;
using System.Windows.Forms;

namespace JobHunt.Helpers
{
    public interface IJobHuntLogDataGridViewHelper
    {
        void Initialise(DataGridView dataGridView);
        void Load(DataGridView dataGridView);
        void RowLeave(DataGridView dataGridView, string dateStampColumnName, int currentRowIndex, bool isRowDirty);
        void DataError(string message, Exception ex);
        void SaveChanges();

    }
}
