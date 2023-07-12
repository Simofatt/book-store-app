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
        Task<IEnumerable<T>> GetAllAsync(string? includePropreties =null);

        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includePropreties = null);
        Task<T> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entity);
    }
}
