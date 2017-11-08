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
using NinjaSoftwareLtd.ErrorLogging;

namespace JobHunt.Helpers
{
    public class JobHuntLogDataGridViewHelper : IJobHuntLogDataGridViewHelper
    {
        private IDataGridViewHelper _dataGridViewHelper;
        private IJobHuntLogRepository _jobHuntLogRepository;
        private IWhoFoundRepository _whoFoundRepository;
        private IWhereFoundRepository _whereFoundRepository;
        private IJobTypeRepository _jobTypeRepository;
        private IErrorLogger _errorLogger;

        public JobHuntLogDataGridViewHelper(IDataGridViewHelper dataGridViewHelper, IJobHuntLogRepository jobHuntLogRepository, IWhoFoundRepository whoFoundRepository, 
               IWhereFoundRepository whereFoundRepository, IJobTypeRepository jobTypeRepository, IErrorLogger errorLogger)
        {
            _dataGridViewHelper = dataGridViewHelper;
            _jobHuntLogRepository = jobHuntLogRepository;
            _whoFoundRepository = whoFoundRepository;
            _whereFoundRepository = whereFoundRepository;
            _jobTypeRepository = jobTypeRepository;
            _errorLogger = errorLogger;
            _errorLogger.ClassName = this.GetType().ToString();
        }

        public void Initialise(DataGridView dataGridView)
        {
            if (_dataGridViewHelper == null)
                throw new NullReferenceException("DataGridViewHelper is null");
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");

            // Fetch and bind data
            var jobHuntLogData = _jobHuntLogRepository.Load();
            dataGridView.DataSource = jobHuntLogData;

            // Convert FK fields to ComboBoxes
            var whereFoundColumnName = "WhereFoundId";
            var whereFoundData = _whereFoundRepository.Load();
            _dataGridViewHelper.ConvertColumnToComboBox(dataGridView, whereFoundColumnName, whereFoundData, "Source");

            var whoFoundColumnName = "WhoFoundId";
            var whoFoundData = _whoFoundRepository.Load();
            _dataGridViewHelper.ConvertColumnToComboBox(dataGridView, whoFoundColumnName, whoFoundData, "Source");
            
            var jobTypeColumnName = "JobTypeId";
            var jobTypeData = _jobTypeRepository.Load();
            _dataGridViewHelper.ConvertColumnToComboBox(dataGridView, jobTypeColumnName, jobTypeData, "Type");

        }

        public void Load(DataGridView dataGridView)
        {
            if (_dataGridViewHelper == null)
                throw new NullReferenceException("DataGridViewHelper is null");
            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView is null");

            // Prune last 3 columns
            _dataGridViewHelper.RTrim(dataGridView, 3);

            // Set Readonly Columns
            _dataGridViewHelper.SetReadOnly(dataGridView, 0);
            //dgvJobHuntLog.Columns[0].Width = 70;
            _dataGridViewHelper.SetReadOnly(dataGridView, dataGridView.Columns.Count - 1);

            // Select last row
            _dataGridViewHelper.SelectLastRow(dataGridView);

        }

        public void SaveChanges()
        {
            _jobHuntLogRepository.SaveChanges();
        }

        #region Event Handlers
        public void DataError(string message, Exception ex)
        {
            _errorLogger.LogError(message, ex);

        }

        public void RowLeave(DataGridView dataGridView, string dateStampColumnName, int currentRowIndex, bool isRowDirty)
        {
            // Last row and has changed
            if (isRowDirty && currentRowIndex == dataGridView.RowCount - 2)
            {
                if (!dataGridView.Columns.Contains(dateStampColumnName))
                    throw new ArgumentException("DataGridView does not contain specified DateStamp ColumnName");
                if (dataGridView.Columns[dateStampColumnName].ValueType != typeof(DateTime))
                    throw new ArgumentException("Specified ColumnName is not of type DateTime");

                if ((DateTime)dataGridView.CurrentRow.Cells[dateStampColumnName].Value == DateTime.MinValue)
                    dataGridView.CurrentRow.Cells[dateStampColumnName].Value = DateTime.Now;
            }
        }
        #endregion
    }
}
