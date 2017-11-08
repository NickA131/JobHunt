using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using JobHunt.Helpers;

namespace JobHunt.Tests
{
    [TestClass]
    public class DataGridViewHelperTests
    {
        private Mock<IComboBoxColumnHelper> _comboBoxColumnHelper;
        private string _testCreateArg;
        private DataGridViewComboBoxColumn _testComboBoxColumn; 
        private DataGridViewComboBoxColumn _testConfigureArg0;
        private IBindingList _testConfigureArg1;
        private string _testConfigureArg2, _testConfigureArg3, _testConfigureArg4;
        private DataGridViewComboBoxColumn _testComboBoxColumnConfigured;

        private DataGridView _testDataGridView;
        private DataGridViewColumn _testDataGridViewColumn;
        private TestTableSource _testTableSource;
        private string _testComboBoxColumnName = "dgvCol1";
        private string _testTableReplaceColumnName = "LookupTableId";
        private string _testTableReplaceColumnNameInvalid = "InvalidColumnName";
        private TestLookupSource _testLookupSource;
        private IBindingList _testLookupSourceBindingList;
        private string _testDisplayColumnName = "Text";

        private DataGridViewHelper _dataGridViewHelper;

        #region Test Setup
        [TestInitialize]
        public void TestInitialize()
        {
            _testLookupSource = new TestLookupSource();
            _testLookupSourceBindingList = _testLookupSource.Create();

            _testComboBoxColumn = new DataGridViewComboBoxColumn()
            {
                DataPropertyName = _testTableReplaceColumnName,
                HeaderText = _testTableReplaceColumnName
            };

            _testComboBoxColumnConfigured = new DataGridViewComboBoxColumn()
            {
                Name = _testTableReplaceColumnName,
                DataPropertyName = _testTableReplaceColumnName,
                HeaderText = _testTableReplaceColumnName,
                DataSource = _testLookupSourceBindingList,
                DisplayMember = _testDisplayColumnName,
                ValueMember = _testTableReplaceColumnName
            };

            _comboBoxColumnHelper = new Mock<IComboBoxColumnHelper>();
            _comboBoxColumnHelper.Setup(c => c.Create(It.IsAny<string>()))
                                 .Returns(_testComboBoxColumn)
                                 .Callback<string>(a => _testCreateArg = a);
            
            _comboBoxColumnHelper.Setup(c => c.Configure(It.IsAny<DataGridViewComboBoxColumn>(), It.IsAny<IBindingList>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(_testComboBoxColumnConfigured)
                                 .Callback<DataGridViewComboBoxColumn, IBindingList, string, string, string>((a, b, c, d, e) => { _testConfigureArg0 = a; _testConfigureArg1 = b; _testConfigureArg2 = c; _testConfigureArg3 = d; _testConfigureArg4 = e; });

            _testTableSource = new TestTableSource();

            _testDataGridView = new DataGridView();
            _testDataGridView.AutoGenerateColumns = true;
            _testDataGridView.DataSource = _testTableSource.Create();
            _testDataGridView.BindingContext = new BindingContext();

            _testDataGridViewColumn = new DataGridViewColumn(new DataGridViewTextBoxCell()) { Name = _testComboBoxColumnName };

            _dataGridViewHelper = new DataGridViewHelper(_comboBoxColumnHelper.Object);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _comboBoxColumnHelper = null;
            _testDataGridView = null;
            _testDataGridViewColumn = null;

        }
        #endregion

        #region ReplaceColumn Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperReplaceColumn_DataGridViewNull_ArgumentNullException()
        {
            _dataGridViewHelper.ReplaceColumn(null, _testTableReplaceColumnName, _testDataGridViewColumn);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperReplaceColumn_ColumnNameNull_ArgumentNullException()
        {
            _dataGridViewHelper.ReplaceColumn(_testDataGridView, null, _testDataGridViewColumn);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperReplaceColumn_DataGridViewColumnNull_ArgumentNullException()
        {
            _dataGridViewHelper.ReplaceColumn(_testDataGridView, _testTableReplaceColumnName, null);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperReplaceColumn_ColumnNameDoesntExist_ArgumentException()
        {
            _dataGridViewHelper.ReplaceColumn(_testDataGridView, _testTableReplaceColumnNameInvalid, _testDataGridViewColumn);

            // Expecting: ArgumentException
        }

        [TestMethod]
        public void DataGridViewHelperReplaceColumn_ColumnNameExists_ColumnReplaced()
        {
            _dataGridViewHelper.ReplaceColumn(_testDataGridView, _testTableReplaceColumnName, _testDataGridViewColumn);

            // Check column was replaced
            Assert.IsTrue(_testDataGridView.Columns.Contains(_testDataGridViewColumn.Name));
            Assert.IsFalse(_testDataGridView.Columns.Contains(_testTableReplaceColumnName));
        }
        #endregion

        #region RTrim Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperRTrim_DataGridViewNull_ArgumentNullException()
        {
            _dataGridViewHelper.RTrim(null, 1);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperRTrim_CountEqualToZero_ArgumentException()
        {
            _dataGridViewHelper.RTrim(_testDataGridView, 0);

            // Expecting: ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperRTrim_CountLessThanZero_ArgumentException()
        {
            _dataGridViewHelper.RTrim(_testDataGridView, -2);

            // Expecting: ArgumentException
        }

        [TestMethod]
        public void DataGridViewHelperRTrim_ValidCount_ColumnsRemoved()
        {
            var columnNames = new List<string>();
            foreach (DataGridViewColumn column in _testDataGridView.Columns)
                columnNames.Add(column.Name);

            _dataGridViewHelper.RTrim(_testDataGridView, 2);

            // Check Last 2 columns have been removed.
            for (var idx = 0; idx < columnNames.Count - 3; idx++)
                Assert.AreEqual(columnNames[idx], _testDataGridView.Columns[idx].Name);

            Assert.IsFalse(_testDataGridView.Columns.Contains(columnNames[columnNames.Count - 1]));
            Assert.IsFalse(_testDataGridView.Columns.Contains(columnNames[columnNames.Count - 2]));
        }
        #endregion

        #region SelectLastRow Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperSelectLastRow_DataGridViewNull_ArgumentNullException()
        {
            _dataGridViewHelper.SelectLastRow(null);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperSelectLastRow_DataGridViewNoDataRows_ArgumentException()
        {
            var testDataGridViewNoRows = new DataGridView();
            testDataGridViewNoRows.AutoGenerateColumns = true;
            testDataGridViewNoRows.DataSource = _testTableSource.CreateEmptyTable();
            testDataGridViewNoRows.BindingContext = new BindingContext();

            _dataGridViewHelper.SelectLastRow(testDataGridViewNoRows);

            // Expecting: ArgumentException
        }

        [TestMethod]
        public void DataGridViewHelperSelectLastRow_DataGridViewHasRows_LastRowSelected()
        {
            _dataGridViewHelper.SelectLastRow(_testDataGridView);

            // Check last row selected
            Assert.AreEqual(_testDataGridView.RowCount - 1, _testDataGridView.SelectedRows[0].Index);

        }
        #endregion

        #region SelectRow Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperSelecttRow_DataGridViewNull_ArgumentNullException()
        {
            _dataGridViewHelper.SelectRow(null, 1);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperSelecttRow_DataGridViewNoDataRows_ArgumentException()
        {
            var testDataGridViewNoRows = new DataGridView();
            testDataGridViewNoRows.AutoGenerateColumns = true;
            testDataGridViewNoRows.DataSource = _testTableSource.CreateEmptyTable();
            testDataGridViewNoRows.BindingContext = new BindingContext();

            _dataGridViewHelper.SelectRow(testDataGridViewNoRows, 1);

            // Expecting: ArgumentException
        }

        [TestMethod]
        public void DataGridViewHelperSelecttRow_DataGridViewHasLessRows_LastRowSelected()
        {
            _dataGridViewHelper.SelectRow(_testDataGridView, _testDataGridView.RowCount + 2);

            // Check last row selected
            Assert.AreEqual(_testDataGridView.RowCount - 1, _testDataGridView.SelectedRows[0].Index);

        }

        [TestMethod]
        public void DataGridViewHelperSelecttRow_DataGridViewHasMoreRows_SpecifiedRowSelected()
        {
            var selectRow = _testDataGridView.RowCount - 2;
            _dataGridViewHelper.SelectRow(_testDataGridView, selectRow);

            // Check last specified selected
            Assert.AreEqual(selectRow,  _testDataGridView.SelectedRows[0].Index);

        }
        #endregion

        #region SetReadOnly Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DataGridViewHelperSetReadOnly_DataGridViewNull_ArgumentNullException()
        {
            _dataGridViewHelper.SetReadOnly(null, 1);

            // Expecting: ArgumentNullException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperSetReadOnly_DataGridViewNoColumns_ArgumentException()
        {
            var testDataGridViewNoColumns = new DataGridView();
            testDataGridViewNoColumns.AutoGenerateColumns = true;
            testDataGridViewNoColumns.DataSource = new DataTable();
            testDataGridViewNoColumns.BindingContext = new BindingContext();
            
            _dataGridViewHelper.SetReadOnly(testDataGridViewNoColumns, 1);

            // Expecting: ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperSetReadOnly_ColIdxLessThanZero_ArgumentException()
        {
            _dataGridViewHelper.SetReadOnly(_testDataGridView, -1);

            // Expecting: ArgumentException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DataGridViewHelperSetReadOnly_ColIdxGreaterThanTotalColumns_ArgumentException()
        {
            _dataGridViewHelper.SetReadOnly(_testDataGridView, _testDataGridView.ColumnCount);

            // Expecting: ArgumentException
        }

        [TestMethod]
        public void DataGridViewHelperSetReadOnly_ColIdxIs2_Column2SetReadOnly()
        {
            _dataGridViewHelper.SetReadOnly(_testDataGridView, 2);

            // Expecting: ArgumentException
            Assert.IsTrue(_testDataGridView.Columns[2].ReadOnly);
            Assert.AreEqual(Color.LightGray, _testDataGridView.Columns[2].DefaultCellStyle.BackColor);

        }
        #endregion

        #region ConvertColumnToComboBox Tests
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DataGridViewHelperConvertColumnToComboBox_ComboBoxColumnNull_ArgumentNullException()
        {

            var dataGridViewHelper = new DataGridViewHelper(null);
            dataGridViewHelper.ConvertColumnToComboBox(_testDataGridView, _testTableReplaceColumnName, _testLookupSourceBindingList, _testDisplayColumnName);

            // Expecting: NullReferenceException
        }

        [TestMethod]
        public void DataGridViewHelperConvertColumnToComboBox_ArgsValid_CheckCreateCallArgs()
        {
            //_comboBoxColumnHelper.Setup(c => c.Create(It.IsAny<string>()))
            //                     .Returns(_testComboBoxColumn)
            //                     .Callback<string>(a => _testCreateArg = a);
            
            _dataGridViewHelper.ConvertColumnToComboBox(_testDataGridView, _testTableReplaceColumnName, _testLookupSourceBindingList, _testDisplayColumnName);

            // Expecting: Create to be called with _testTableReplaceColumnName arg
            Assert.AreEqual(_testTableReplaceColumnName, _testCreateArg);
        }

        [TestMethod]
        public void DataGridViewHelperConvertColumnToComboBox_ArgsValid_CheckConfigureCallArgs()
        {
            //_comboBoxColumnHelper.Setup(c => c.Configure(It.IsAny<DataGridViewComboBoxColumn>(), It.IsAny<IBindingList>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            //                     .Returns(_testComboBoxColumnConfigured)
            //                     .Callback<DataGridViewComboBoxColumn, IBindingList, string, string, string>((a, b, c, d, e) => { _testConfigureArg0 = a; _testConfigureArg1 = b; _testConfigureArg2 = c; _testConfigureArg3 = d; _testConfigureArg4 = e; });
            
            _dataGridViewHelper.ConvertColumnToComboBox(_testDataGridView, _testTableReplaceColumnName, _testLookupSourceBindingList, _testDisplayColumnName);

            // Expecting: Configure to be called with correct args
            Assert.AreEqual(_testComboBoxColumn, _testConfigureArg0);
            Assert.AreEqual(_testLookupSourceBindingList, _testConfigureArg1);
            Assert.AreEqual(_testTableReplaceColumnName, _testConfigureArg2);
            Assert.AreEqual(_testDisplayColumnName, _testConfigureArg3);
            Assert.AreEqual(_testTableReplaceColumnName, _testConfigureArg4);
        }

        [TestMethod]
        public void DataGridViewHelperConvertColumnToComboBox_ArgsValid_ComboBoxColumnConverted()
        {
            _dataGridViewHelper.ConvertColumnToComboBox(_testDataGridView, _testTableReplaceColumnName, _testLookupSourceBindingList, _testDisplayColumnName);

            // Expecting: Column to be replaced
            Assert.IsTrue(_testDataGridView.Columns.Contains(_testTableReplaceColumnName));
            Assert.AreEqual(typeof(DataGridViewComboBoxColumn), _testDataGridView.Columns[_testTableReplaceColumnName].GetType());

        }
        #endregion
    }
}
