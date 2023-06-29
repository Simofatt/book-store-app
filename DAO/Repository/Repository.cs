using dotNet.DAO.Data;
using dotNet.DAO.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using dotNet.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace dotNet.DAO.Repository
{
    public class Repository<T> : IRepository<T> where T : class

    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();   //_db.Categories == dbSet
            
        }
        public void Add(T entity)
        {
            
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,string? includePropreties = null)
        {
          
             IQueryable<T> query = dbSet.Where(filter);
            if (!string.IsNullOrEmpty(includePropreties))
            {
                foreach (var includeProp in  includePropreties.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp); 
                    
                }
            }
           
                return query.FirstOrDefault();
            
        }


        public IEnumerable<T> GetAll(string? includePropreties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includePropreties)) {
                foreach(var includeProp in includePropreties.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries) )
                {
                    query = query.Include(includeProp); 

                }
                
            }
            return query;
        }

        

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
