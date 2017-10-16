using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntData.Repositories
{
    public class JobTypeRepository : GenericRepository<JobType>, IJobTypeRepository
    {
        public JobTypeRepository(DbContext jobTypeContext) : base(jobTypeContext)
        { }


    }
}
