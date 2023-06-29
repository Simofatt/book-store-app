using dotNet.DAO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet.DAO.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category {  get;}
        IProductRepository Product { get; }
        IOrderRepository Order { get; }   
       

        void Save();


    }

}
