using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace JobHunt.Helpers
{
    public interface IComboBoxColumnHelper
    {
        DataGridViewComboBoxColumn Configure(DataGridViewComboBoxColumn comboBoxCol, IBindingList dataSource, string propertyName, string displayMember, string valueMember);
        DataGridViewComboBoxColumn Create(string columnName);
    }
}
