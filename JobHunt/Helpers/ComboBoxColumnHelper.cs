using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobHuntData.Repositories;

namespace JobHunt.Helpers
{
    public class ComboBoxColumnHelper : IComboBoxColumnHelper
    {

        public DataGridViewComboBoxColumn Create(string columnName)
        {
            var comboBoxCol = new DataGridViewComboBoxColumn()
            {
                DataPropertyName = columnName,
                HeaderText = (columnName.ToLower().EndsWith("id") ? columnName.Substring(0, columnName.Length - 2) : columnName),
                DropDownWidth = 160,
                Width = 90,
                MaxDropDownItems = 3,
                FlatStyle = FlatStyle.Flat
            };

            return comboBoxCol;
        }

        public DataGridViewComboBoxColumn Configure(DataGridViewComboBoxColumn comboBoxCol, IBindingList dataSource, string propertyName, string displayMember, string valueMember)
        {

            comboBoxCol.DataSource = dataSource;
            comboBoxCol.DataPropertyName = propertyName;
            comboBoxCol.DisplayMember = displayMember;
            comboBoxCol.ValueMember = valueMember;

            return comboBoxCol;
        }
        
    }
}
