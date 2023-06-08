using dotNet.DAO.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet.DAO.Data;

namespace dotNet.DAO.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { set;  get; }
        public IProductRepository Product { set; get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);   

        }

       

       public void Save()
        {
            _db.SaveChanges();
        }

    
    }
}
