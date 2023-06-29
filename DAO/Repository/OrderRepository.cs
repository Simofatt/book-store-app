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

namespace dotNet.DAO.Repository
{
    public  class OrderRepository : Repository<Order>, IOrderRepository
    {
        private  ApplicationDbContext _db; 

        public OrderRepository(ApplicationDbContext db) : base(db) 
        {
            _db=db;
        }

        public IQueryable<Order> GetOrders(Expression<Func<Order, bool>> filter, string? includePropreties = null)
        {
            IQueryable <Order> query = _db.Orders;
            
            if(!string.IsNullOrEmpty(includePropreties))
            {
                foreach (var includeProp in includePropreties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
               
            }
            query = query.Where(filter);

            return query; 
        }

        public string GetUserId(string? email)
        {

            if (email is not null)
            {
                var userId = (from u in _db.Users
                              where u.Email == email
                              select u.Id).FirstOrDefault();
                return userId;
            } 
            
            return string.Empty;
        }

        public void Update(Order entity)
        {
            if(entity!=null)
            {
                _db.Orders.Update(entity);

            }
        }
   } 
}
