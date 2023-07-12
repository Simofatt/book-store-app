using dotNet.DAO.Repository.IRepository;

using dotNet.DAO.Data;
using dotNet.DAO.Repository;
using dotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dotNet.DAO.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public Task Update(Category obj)
        {
             _db.Categories.Update(obj);
            return Task.CompletedTask;
            
        }
    }
}
