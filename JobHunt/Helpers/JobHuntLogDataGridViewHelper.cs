using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobHuntData;
using JobHuntData.Repositories;

namespace JobHunt.Helpers
{
    public class JobHuntLogDataGridViewHelper : DataGridViewHelper, IJobHuntLogDataGridViewHelper
    {
        private IJobHuntLogRepository _jobHuntLogRepository;
        private IWhoFoundRepository _whoFoundRepository;
        private IWhereFoundRepository _whereFoundRepository;
        private IJobTypeRepository _jobTypeRepository;

        public JobHuntLogDataGridViewHelper(IJobHuntLogRepository jobHuntLogRepository, IWhoFoundRepository whoFoundRepository, IWhereFoundRepository whereFoundRepository, 
               IJobTypeRepository jobTypeRepository, IComboBoxColumnHelper comboBoxColumnHelper) : base(comboBoxColumnHelper)
        {
            _jobHuntLogRepository = jobHuntLogRepository;
            _whoFoundRepository = whoFoundRepository;
            _whereFoundRepository = whereFoundRepository;
            _jobTypeRepository = jobTypeRepository;
        }

        public void Initialise(DataGridView dataGridView)
        {
            // Fetch and bind data
            var jobHuntLogData = _jobHuntLogRepository.Load();
            dataGridView.DataError += new DataGridViewDataErrorEventHandler(DataError);
            dataGridView.RowLeave += new DataGridViewCellEventHandler(RowLeave);
            dataGridView.DataSource = jobHuntLogData;

            // Convert FK fields to ComboBoxes
            var whereFoundColumnName = "WhereFoundId";
            var whereFoundData = _whereFoundRepository.Load();
            ConvertColumnToComboBox(dataGridView, whereFoundColumnName, whereFoundData, "Source");

            var whoFoundColumnName = "WhoFoundId";
            var whoFoundData = _whoFoundRepository.Load();
            ConvertColumnToComboBox(dataGridView, whoFoundColumnName, whoFoundData, "Source");
            
            var jobTypeColumnName = "JobTypeId";
            var jobTypeData = _jobTypeRepository.Load();
            ConvertColumnToComboBox(dataGridView, jobTypeColumnName, jobTypeData, "Type");

        }
        
        public void SaveChanges()
        {
            _jobHuntLogRepository.SaveChanges();
        }

        #region Event Handlers
        public void DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Error happened " + anError.Context.ToString(), "Data Update Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        public void RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            var currentRowIdx = dataGridView.CurrentRow.Index;
            // Last row and has changed
            if (dataGridView.IsCurrentRowDirty && currentRowIdx == dataGridView.RowCount - 2)
            {
                if ((DateTime)dataGridView.CurrentRow.Cells["DateEntered"].Value == DateTime.MinValue)
                    dataGridView.CurrentRow.Cells["DateEntered"].Value = DateTime.Now;

            }

        }

        #endregion
    }
}
