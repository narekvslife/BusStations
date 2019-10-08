using System.Collections.Generic;
using HW3_AL.Core.Model;

namespace HW3_AL.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> GetAllItems();
    }
}