using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet.DAO.Data;
using dotNet.Models;    

namespace dotNet.DAO.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        Task Update(Product prod);
    }
}
