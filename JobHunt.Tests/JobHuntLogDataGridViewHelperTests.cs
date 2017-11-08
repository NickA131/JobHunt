using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using JobHunt.Helpers;
using JobHuntData;
using JobHuntData.Repositories;
using NinjaSoftwareLtd.ErrorLogging;

namespace JobHunt.Tests
{
    [TestClass]
    public class JobHuntLogDataGridViewHelperTests
    {
        private Mock<IDataGridViewHelper> _dataGridViewHelper;
        private Mock<IJobHuntLogRepository> _jobHuntLogRepository;
        private Mock<IWhoFoundRepository> _whoFoundRepository;
        private Mock<IWhereFoundRepository> _whereFoundRepository;
        private Mock<IJobTypeRepository> _jobTypeRepository;
        private Mock<IErrorLogger> _errorLogger;
        private string _testDateStampColumnName = "DateEntered";
        private DataGridView _testDataGridView;
        private TestTableSource _testTableSource;

        private IJobHuntLogDataGridViewHelper _jobHuntLogDataGridViewHelper;
        
        private List<DataGridView> _convertToComboBoxArg0;
        private List<string> _convertToComboBoxArg1;
        private List<object> _convertToComboBoxArg2;
        private List<string> _convertToComboBoxArg3;

        private DataGridView _rTrimArg0;
        private int _rTrimArg1;

        private List<DataGridView> _setReadOnlyArg0;
        private List<int> _setReadOnlyArg1;

        private DataGridView _selectLastRowArg0;
        
        #region Test Setup
        [TestInitialize]
        public void TestInitialize()
        {
            _dataGridViewHelper = new Mock<IDataGridViewHelper>();
            _jobHuntLogRepository = new Mock<IJobHuntLogRepository>();
            _whoFoundRepository = new Mock<IWhoFoundRepository>();
            _whereFoundRepository = new Mock<IWhereFoundRepository>();
            _jobTypeRepository = new Mock<IJobTypeRepository>();

            var jobHuntLogData = new BindingList<JobHuntData.JobHuntLog>();
            jobHuntLogData.Add(new JobHuntData.JobHuntLog() { JobHuntId = 2, DateEntered = new DateTime(2017, 12, 22) });
            _jobHuntLogRepository.Setup(r => r.Load()).Returns(jobHuntLogData);

            var whereFound = new BindingList<WhereFound>();
            whereFound.Add(new WhereFound() { WhereFoundId = 3, DateEntered = new DateTime(2017, 1, 2) });
            _whereFoundRepository.Setup(r => r.Load()).Returns(whereFound);

            var whoFound = new BindingList<WhoFound>();
            whoFound.Add(new WhoFound() { WhoFoundId = 5, DateEntered = new DateTime(2017, 3, 4) });
            _whoFoundRepository.Setup(r => r.Load()).Returns(whoFound);

            var jobType = new BindingList<JobType>();
            jobType.Add(new JobType() { JobTypeId = 1, DateEntered = new DateTime(2016, 5, 9) });
            _jobTypeRepository.Setup(r => r.Load()).Returns(jobType);
            
            _testTableSource = new TestTableSource();

            _testDataGridView = new DataGridView();
            _testDataGridView.AutoGenerateColumns = true;
            _testDataGridView.DataSource = _testTableSource.Create();
            _testDataGridView.BindingContext = new BindingContext();

            var currentRowIndex = _testDataGridView.RowCount - 2;
            _testDataGridView.Rows[currentRowIndex].Selected = true;
            _testDataGridView.CurrentCell = _testDataGridView.Rows[currentRowIndex].Cells[1];

            _errorLogger = new Mock<IErrorLogger>();
            _errorLogger.SetupProperty(e => e.ClassName);

            _jobHuntLogDataGridViewHelper = new JobHuntLogDataGridViewHelper(_dataGridViewHelper.Object, _jobHuntLogRepository.Object, _whoFoundRepository.Object, _whereFoundRepository.Object,
                                                                             _jobTypeRepository.Object, _errorLogger.Object);

            _convertToComboBoxArg0 = new List<DataGridView>();
            _convertToComboBoxArg1 = new List<string>();
            _convertToComboBoxArg2 = new List<object>();
            _convertToComboBoxArg3 = new List<string>();

            _dataGridViewHelper.Setup(c => c.ConvertColumnToComboBox(It.IsAny<DataGridView>(), It.IsAny<string>(), It.IsAny<IBindingList>(), It.IsAny<string>()))
                               .Callback<DataGridView, string, IBindingList, string>((a, b, c, d) => { _convertToComboBoxArg0.Add(a); _convertToComboBoxArg1.Add(b); _convertToComboBoxArg2.Add(c); _convertToComboBoxArg3.Add(d); });

            _dataGridViewHelper.Setup(r => r.RTrim(It.IsAny<DataGridView>(), It.IsAny<int>()))
                               .Callback<DataGridView, int>((a, b) => { _rTrimArg0 = a; _rTrimArg1 = b; });

            _setReadOnlyArg0 = new List<DataGridView>();
            _setReadOnlyArg1 = new List<int>();

            _dataGridViewHelper.Setup(s => s.SetReadOnly(It.IsAny<DataGridView>(), It.IsAny<int>()))
                               .Callback<DataGridView, int>((a, b) => { _setReadOnlyArg0.Add(a); _setReadOnlyArg1.Add(b); });

            _dataGridViewHelper.Setup(s => s.SelectLastRow(It.IsAny<DataGridView>()))
                               .Callback<DataGridView>(a => _selectLastRowArg0 = a);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dataGridViewHelper = null;
            _jobHuntLogRepository = null;
            _whoFoundRepository = null;
            _whereFoundRepository = null;
            _jobTypeRepository = null;
            _errorLogger = null;
        }
        #endregion

        #region Initialise Tests
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void JobHuntLogDataGridViewHelperInitialise_DataGridViewHelperNull_NullReferenceException()
        {
            _jobHuntLogDataGridViewHelper = new JobHuntLogDataGridViewHelper(null, _jobHuntLogRepository.Object, _whoFoundRepository.Object, _whereFoundRepository.Object,
                                                                 _jobTypeRepository.Object, _errorLogger.Object);

            _jobHuntLogDataGridViewHelper.Initialise(_testDataGridView);

            // Expecting: NullReferenceException

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JobHuntLogDataGridViewHelperInitialise_DataGridViewNull_ArgumentNullException()
        {

            _jobHuntLogDataGridViewHelper.Initialise(null);

            // Expecting: ArgumentNullException

        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperInitialise_CheckDataSet_DataSourceSet()
        {

            var testDataGridView = new DataGridView();

            _jobHuntLogDataGridViewHelper.Initialise(testDataGridView);

            // Expecting: DataGridView's DataSource to be set
            Assert.IsNotNull(_testDataGridView.DataSource);

        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperInitialise_CheckComboBoxColumnsSet_ComboBoxColumnsCreated()
        {

            _jobHuntLogDataGridViewHelper.Initialise(_testDataGridView);

            // Expecting: x3 Calls to ConvertColumnToComboBox with certain string values.
            Assert.AreEqual(3, _convertToComboBoxArg0.Count);

            Assert.AreEqual("WhereFoundId", _convertToComboBoxArg1[0]);
            Assert.AreEqual("WhoFoundId", _convertToComboBoxArg1[1]);
            Assert.AreEqual("JobTypeId", _convertToComboBoxArg1[2]);

            Assert.AreEqual(typeof(BindingList<WhereFound>), _convertToComboBoxArg2[0].GetType());
            Assert.AreEqual(typeof(BindingList<WhoFound>), _convertToComboBoxArg2[1].GetType());
            Assert.AreEqual(typeof(BindingList<JobType>), _convertToComboBoxArg2[2].GetType());

            Assert.AreEqual("Source", _convertToComboBoxArg3[0]);
            Assert.AreEqual("Source", _convertToComboBoxArg3[1]);
            Assert.AreEqual("Type", _convertToComboBoxArg3[2]);

        }
        #endregion

        #region Load Tests
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void JobHuntLogDataGridViewHelperLoad_DataGridViewHelperNull_NullReferenceException()
        {
            _jobHuntLogDataGridViewHelper = new JobHuntLogDataGridViewHelper(null, _jobHuntLogRepository.Object, _whoFoundRepository.Object, _whereFoundRepository.Object,
                                                                 _jobTypeRepository.Object, _errorLogger.Object);

            _jobHuntLogDataGridViewHelper.Load(_testDataGridView);

            // Expecting: NullReferenceException

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JobHuntLogDataGridViewHelperLoad_DataGridViewNull_ArgumentNullException()
        {

            _jobHuntLogDataGridViewHelper.Load(null);

            // Expecting: ArgumentNullException

        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperLoad_CheckRHFKColumnsRemoved_RHFKColumnsRemoved()
        {

            _jobHuntLogDataGridViewHelper.Load(_testDataGridView);

            // Expecting: RTrim called with certain values
            Assert.AreEqual(_testDataGridView, _rTrimArg0);
            Assert.AreEqual(3, _rTrimArg1);
        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperLoad_CheckCertainColumnsSetReadUOnly_ColumnsSetReadUOnly()
        {

            _jobHuntLogDataGridViewHelper.Load(_testDataGridView);

            // Expecting: SetReadOnly to be called x2 with certain values
            Assert.AreEqual(2, _setReadOnlyArg0.Count);

            Assert.AreEqual(0, _setReadOnlyArg1[0]);
            Assert.AreEqual(_testDataGridView.ColumnCount - 1, _setReadOnlyArg1[1]);
        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperLoad_CheckLastRowSelected_LastRowSelected()
        {
            _jobHuntLogDataGridViewHelper.Load(_testDataGridView);

            // Expecting: DataGridView object to be passed through to SelectLastRow
            Assert.AreEqual(_testDataGridView, _selectLastRowArg0);
        }
        #endregion

        #region SaveChanges Tests
        [TestMethod]
        public void JobHuntLogDataGridViewHelperSaveChanges_CheckSaveChangesCalled_SaveChangesCalled()
        {
            var methodCalled = false;

            _jobHuntLogRepository.Setup(s => s.SaveChanges())
                                 .Callback(() => methodCalled = true);

            _jobHuntLogDataGridViewHelper.SaveChanges();

            // Expecting: SaveChanges is called for repository
            Assert.IsTrue(methodCalled);

        }
        #endregion

        #region DataError Tests
        [TestMethod]
        public void JobHuntLogDataGridViewHelperDataError_EventCalled_CheckErrorLogged()
        {

            var expectedMessage = "This is a test message";
            var expectedException = new ArgumentNullException("Argument exception");

            var actualMessage = string.Empty;
            Exception actualException = null;

            _errorLogger.Setup(l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()))
                        .Callback<string, Exception>((a, b) => { actualMessage = a; actualException = b; });

            _jobHuntLogDataGridViewHelper.DataError(expectedMessage, expectedException);

            // Expecting: Call to LogError
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(expectedException, actualException);

        }
        #endregion

        #region RowLeave Tests
        [TestMethod]
        public void JobHuntLogDataGridViewHelperRowLeave_EventCalledRowNotDirtyNotLastRow_DateNotStamped()
        {
            var currentRowIndex = 1;
            var rowDirty = false;

            _jobHuntLogDataGridViewHelper.RowLeave(_testDataGridView, _testDateStampColumnName, currentRowIndex, rowDirty);

            // Expecting: DateEntered unchanged
            Assert.AreEqual(DateTime.MinValue, _testDataGridView.Rows[_testDataGridView.RowCount - 2].Cells[_testDateStampColumnName].Value);

        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperRowLeave_EventCalledRowDirtyNotLastRow_DateNotStamped()
        {
            var currentRowIndex = 1;
            var rowDirty = true;

            _jobHuntLogDataGridViewHelper.RowLeave(_testDataGridView, _testDateStampColumnName, currentRowIndex, rowDirty);

            // Expecting: DateEntered unchanged
            Assert.AreEqual(DateTime.MinValue, _testDataGridView.Rows[_testDataGridView.RowCount - 2].Cells[_testDateStampColumnName].Value);

        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperRowLeave_EventCalledRowNotDirtyLastRow_DateNotStamped()
        {
            var currentRowIndex = _testDataGridView.RowCount - 2;
            var rowDirty = false;

            _jobHuntLogDataGridViewHelper.RowLeave(_testDataGridView, _testDateStampColumnName, currentRowIndex, rowDirty);

            // Expecting: DateEntered unchanged
            Assert.AreEqual(DateTime.MinValue, _testDataGridView.Rows[_testDataGridView.RowCount - 2].Cells[_testDateStampColumnName].Value);

        }

        [TestMethod]
        public void JobHuntLogDataGridViewHelperRowLeave_EventCalledRowDirtyLastRow_CheckDateStamped()
        {
            var currentRowIndex = _testDataGridView.RowCount - 2;
            var rowDirty = true;

            _jobHuntLogDataGridViewHelper.RowLeave(_testDataGridView, _testDateStampColumnName, currentRowIndex, rowDirty);

            // Expecting: DateEntered changed to today's date
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)_testDataGridView.Rows[_testDataGridView.RowCount - 2].Cells[_testDateStampColumnName].Value).Date);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobHuntLogDataGridViewHelperRowLeave_EventCalledRowDirtyLastRowColumnNameNotExist_ArgumentException()
        {
            var currentRowIndex = _testDataGridView.RowCount - 2;
            var rowDirty = true;

            _jobHuntLogDataGridViewHelper.RowLeave(_testDataGridView, "SpuriousColumn", currentRowIndex, rowDirty);

            // Expecting: ArgumentException
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void JobHuntLogDataGridViewHelperRowLeave_EventCalledRowDirtyLastRowColumnNameNotDateTime_ArgumentException()
        {
            var currentRowIndex = _testDataGridView.RowCount - 2;
            var rowDirty = true;

            _jobHuntLogDataGridViewHelper.RowLeave(_testDataGridView, "Text", currentRowIndex, rowDirty);

            // Expecting: ArgumentException

        }
        #endregion

    }
}
