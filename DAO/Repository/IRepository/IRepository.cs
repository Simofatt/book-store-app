using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet.DAO.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        //T - Category
        IEnumerable<T> GetAll(string? includePropreties =null);

        T Get(Expression<Func<T, bool>> filter, string? includePropreties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
