﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHuntData.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IBindingList Load();
        void SaveChanges();
    }
}
