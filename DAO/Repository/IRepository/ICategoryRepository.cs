
using dotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet.DAO.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task Update(Category obj);
        
    }
}
