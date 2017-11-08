using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using JobHunt.Helpers;

namespace JobHunt.Tests
{
    [TestClass]
    public class ComboBoxColumnHelperTests
    {
        private IComboBoxColumnHelper _comboBoxColumnHelper;
        private string _testColumnName = "TestColumn";
        private string _testColumnNameId = "TestColumnId";

        private DataGridViewComboBoxColumn _testDataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
        private IBindingList _testDataSource = new BindingList<JobHuntLog>();
        private string _testPropertyName = "PropertyName";
        private string _testDisplayMember = "DisplayMember";
        private string _testValueMember = "ValueMember";

        #region Test Setup
        [TestInitialize]
        public void TestInitialize()
        {
            _comboBoxColumnHelper = new ComboBoxColumnHelper();
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _comboBoxColumnHelper = null;
        }
        #endregion

        #region Create Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComboBoxColumnHelperCreate_ColumnNameNull_ArgumentNullException()
        {
            _comboBoxColumnHelper.Create(null);

            // Expecting: ArgumentNullException

        }

        [TestMethod]
        public void ComboBoxColumnHelperCreate_ColumnName_ColumnCreated()
        {
            var result = _comboBoxColumnHelper.Create(_testColumnName);

            // Check return object populated as expected
            Assert.IsInstanceOfType(result, typeof(DataGridViewComboBoxColumn));
            Assert.AreEqual(_testColumnName, result.DataPropertyName);
            Assert.AreEqual(_testColumnName, result.HeaderText);
            Assert.AreEqual(160, result.DropDownWidth);
            Assert.AreEqual(90, result.Width);
            Assert.AreEqual(3, result.MaxDropDownItems);
            Assert.AreEqual(FlatStyle.Flat, result.FlatStyle);

        }

        [TestMethod]
        public void ComboBoxColumnHelperCreate_ColumnNameWithId_IdSuffixStrippedFromHeader()
        {
            var result = _comboBoxColumnHelper.Create(_testColumnNameId);

            // Check return object is populated as expected
            Assert.IsInstanceOfType(result, typeof(DataGridViewComboBoxColumn));
            Assert.AreEqual(_testColumnName, result.HeaderText);

        }
        #endregion

        #region Configure Tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComboBoxColumnHelperConfigure_ComboBoxColNull_ArgumentNullException()
        {
            _comboBoxColumnHelper.Configure(null, _testDataSource, _testPropertyName, _testDisplayMember, _testValueMember);

            // Expecting: ArgumentNullException

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ComboBoxColumnHelperConfigure_DataSourceNull_ArgumentNullException()
        {
            _comboBoxColumnHelper.Configure(_testDataGridViewComboBoxColumn, null, _testPropertyName, _testDisplayMember, _testValueMember);

            // Expecting: ArgumentNullException

        }

        [TestMethod]
        public void ComboBoxColumnHelperConfigure_AllArgsValid_ConfiguredComboBoxColumn()
        {
            var result = _comboBoxColumnHelper.Configure(_testDataGridViewComboBoxColumn, _testDataSource, _testPropertyName, _testDisplayMember, _testValueMember);

            // Check return object is populated as expected
            Assert.IsInstanceOfType(result, typeof(DataGridViewComboBoxColumn));
            Assert.AreEqual(_testDataSource, result.DataSource);
            Assert.AreEqual(_testPropertyName, result.DataPropertyName);
            Assert.AreEqual(_testDisplayMember, result.DisplayMember);
            Assert.AreEqual(_testValueMember, result.ValueMember);
        }
        #endregion
    }
}
