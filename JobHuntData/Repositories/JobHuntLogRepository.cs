using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntData.Repositories
{
    public class JobHuntLogRepository : GenericRepository<JobHuntLog>, IJobHuntLogRepository
    {
        public JobHuntLogRepository(DbContext jobHuntLogContext) : base(jobHuntLogContext)
        { }
        

    }
}
