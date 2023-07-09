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

       Task<IEnumerable<Order>> GetOrdersAsync( Expression<Func<Order, bool>> filter, string? includePropreties = null) ;
       
    }
}
