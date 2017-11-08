using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Tests
{
    public class TestLookupSource
    {

        public IBindingList Create()
        {
            object[,] data = new object[,]{ { 1, "Item1", }, 
                                            { 2, "Item2", },
                                            { 3, "Item3" }};

            var lookupSourceList = new List<TestLookupSourceItem>();

            for (var row = 0; row < data.GetLength(0); row++)
            {
                lookupSourceList.Add(new TestLookupSourceItem() { LookupTableId = (int)data[row, 0], Text = (string)data[row, 1] });
            }

            return new BindingList<TestLookupSourceItem>(lookupSourceList);
        }

    }
}
