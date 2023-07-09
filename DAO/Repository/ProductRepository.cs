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
        public async Task Update(Product pro)
        {
            var objFromDb = _db.Products.FirstOrDefault(u =>u.Id ==pro.Id);
            if(objFromDb !=null)
            { 
                objFromDb.Title = pro.Title;
                objFromDb.Description = pro.Description;    
                objFromDb.ISBN = pro.ISBN;
                objFromDb.Author = pro.Author;  
                objFromDb.ListPrice = pro.ListPrice;    
                objFromDb.Price = pro.Price;
                objFromDb.Price50 = pro.Price50;
                objFromDb.Price100 = pro.Price100;
                objFromDb.CategoryId = pro.CategoryId;

                if(pro.ImgUrl !=null)
                {
                   objFromDb.ImgUrl = pro.ImgUrl;
                }
                await _db.SaveChangesAsync();

            }

          //_db.Update(pro);
        }

    }
}
