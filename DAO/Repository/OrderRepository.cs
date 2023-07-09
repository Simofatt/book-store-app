using dotNet.DAO.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet.Models;
using dotNet.DAO.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dotNet.DAO.Repository
{
    public  class OrderRepository : Repository<Order>, IOrderRepository
    {
        private  ApplicationDbContext _db; 

        public OrderRepository(ApplicationDbContext db) : base(db) 
        {
            _db=db;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Expression<Func<Order, bool>> filter, string? includeProperties = null)
        {
            IQueryable<Order> query = _db.Orders;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            query = query.Where(filter);

            return await query.ToListAsync();
        }



      

     
        
   } 
}
