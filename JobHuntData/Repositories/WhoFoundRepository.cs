using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntData.Repositories
{
    public class WhoFoundRepository : GenericRepository<WhoFound>, IWhoFoundRepository
    {
        public WhoFoundRepository(DbContext whoFoundContext) : base(whoFoundContext)
        { }

    }
}
