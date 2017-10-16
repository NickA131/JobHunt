using System;
using System.Windows.Forms;

namespace JobHunt.Helpers
{
    public interface IJobHuntLogDataGridViewHelper : IDataGridViewHelper
    {
        void DataError(object sender, DataGridViewDataErrorEventArgs anError);
        void Initialise(DataGridView dataGridView);
        void RowLeave(object sender, DataGridViewCellEventArgs e);
        void SaveChanges();

    }
}
