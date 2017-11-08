using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Tests
{
    public class TestTableSource
    {
        public DataTable CreateEmptyTable()
        {
            var dt = new DataTable("TestTable");
            var primaryKeyCol = new DataColumn("PK", typeof(int));
            dt.Columns.Add(primaryKeyCol);
            dt.Columns.Add(new DataColumn("FK", typeof(int)));
            dt.Columns.Add(new DataColumn("LookupTableId", typeof(int)));
            dt.Columns.Add(new DataColumn("Text", typeof(string)));
            dt.Columns.Add(new DataColumn("DateEntered", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("SpuriousFK1", typeof(int)));
            dt.Columns.Add(new DataColumn("SpuriousFK2", typeof(int)));
            
            DataColumn[] keys = new DataColumn[1] { primaryKeyCol };
            dt.PrimaryKey = keys;

            return dt;
        }
        
        public DataTable Create()
        {
            var dt = CreateEmptyTable();

            object[,] data = new object[,]{ { 1, 1, 1, "String1", DateTime.MinValue, 2, 5 }, 
                                            { 2, 2, 1, "String2", DateTime.MinValue, 3, 5 },
                                            { 3, 1, 2, "String3", DateTime.MinValue, 3, 4 },
                                            { 4, 3, 4, "String4", DateTime.MinValue, 1, 3 }};

            for(var row = 0; row < data.GetLength(0); row++)
            {
                var dr = dt.NewRow();
                for (var col = 0; col < data.GetLength(1); col++)
                    dr[col] = data[row, col];
                dt.Rows.Add(dr);
            }

            return dt;
        }
        
    }
}
