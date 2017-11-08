using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobHunt.Helpers;
using JobHuntData;
using JobHuntData.Repositories;
using NinjaSoftwareLtd.ErrorLogging;

namespace JobHunt
{
    public class DependencyInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<JobHuntEntities>();
            Bind<IJobHuntLogRepository>().To<JobHuntLogRepository>();
            Bind<IWhoFoundRepository>().To<WhoFoundRepository>();
            Bind<IWhereFoundRepository>().To<WhereFoundRepository>();
            Bind<IJobTypeRepository>().To<JobTypeRepository>();
            Bind<IErrorLogger>().To<NLogErrorLogger>();
            Bind<IComboBoxColumnHelper>().To<ComboBoxColumnHelper>();

            Bind<IDataGridViewHelper>().To<DataGridViewHelper>();
            Bind<IJobHuntLogDataGridViewHelper>().To<JobHuntLogDataGridViewHelper>();
        }
        
    }
}
