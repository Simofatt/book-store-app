using dotNet.DAO.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet.DAO.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet.DAO.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { set;  get; }
        public IProductRepository Product { set; get; }
        public IOrderRepository Order { set; get; }
        private bool disposed;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);   
            Order = new OrderRepository(_db);   

        }

       

       public async Task<int> Commit()
        {
            //_db.SaveChanges();
            return await _db.SaveChangesAsync();
        }
        public Task Rollback()
        {
            _db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
           // _db.ChangeTracker.Entries().ToList().ForEach(x => x.GetType());
            return Task.CompletedTask;
        }

        //DELETE THE CONNEXTION WITH DB 
        public void Dispose()
        {
            Dispose(true);
            //GC : GARBAGE COLLECTOR (supprimer de la corbeille ) 
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _db.Dispose();
                }
            }
            disposed = true;
            //dispose unmanaged resources logic....(soon) 

        }

    }
}
