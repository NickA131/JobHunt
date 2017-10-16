using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntData.Repositories
{
    public class WhereFoundRepository : GenericRepository<WhereFound>, IWhereFoundRepository
    {
        public WhereFoundRepository(DbContext whereFoundContext) : base(whereFoundContext)
        { }
        

    }
}
