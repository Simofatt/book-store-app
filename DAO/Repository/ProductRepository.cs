using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet.DAO.Data;
using dotNet.DAO.Repository.IRepository;
using dotNet.Models;

namespace dotNet.DAO.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;  

        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db; 
        }
        public void Update(Product pro)
        {

          _db.Update(pro);
        }

    }
}
