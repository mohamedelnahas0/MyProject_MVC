using Microsoft.EntityFrameworkCore;
using MyProject.BLL.Interfaces;
using MyProject.DAL.Data;
using MyProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ProjectDbContext _dbContext; //Null
        public GenericRepository(ProjectDbContext DbContext) //ask clr to create object 
        {
            _dbContext = DbContext;
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {

            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            var Employees = _dbContext.employees.Local.Where(D => D.Id == id).FirstOrDefault();


            return _dbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }

       
    }
}
