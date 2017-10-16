using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntData.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _DbContext;

        public GenericRepository(DbContext dbContext)
        {
            if (dbContext == null) 
                throw new ArgumentNullException("Null DbContext");
            _DbContext = dbContext;
        }

        public virtual IBindingList Load()
        {
            _DbContext.Set<T>().Load();
            return _DbContext.Set<T>().Local.ToBindingList();
        }

        public void SaveChanges()
        {
            _DbContext.SaveChanges();
        }
    }
}
