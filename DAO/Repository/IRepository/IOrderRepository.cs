using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using dotNet.Models; 

namespace dotNet.DAO.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order entity);
         IQueryable<Order> GetOrders( Expression<Func<Order, bool>> filter, string? includePropreties = null) ;
         string GetUserId(string? email);
    }
}
