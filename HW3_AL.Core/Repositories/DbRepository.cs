using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW3_AL.Core;
using HW3_AL.Core.Model;

namespace HW3_AL.Core.Repositories
{
    public abstract class DbRepository<T> where T : class
    {
        MyContex _context;

        public DbRepository(MyContex context)
        {
            _context = context;
        }
        public void Add(T item)
        {
            if (item != null)
                _context.Set<T>().Add(item);
        }
        public void Remove(T item)
        {
            if (item != null)
                _context.Set<T>().Remove(item);
        }
        public abstract IEnumerable<T> GetAllItems();
    }
}
