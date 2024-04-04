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
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {

            _dbContext.Set<T>().Remove(entity);
        }

        public T Get(int id)
        {
            var Employees = _dbContext.employees.Local.Where(D => D.Id == id).FirstOrDefault();


            return  _dbContext.Find<T>(id);
        }

        public virtual IEnumerable<T> GetAll ()
        {
            if (typeof(T)== typeof(Employee)) {
            return (IEnumerable<T>)  _dbContext.employees.Include(E => E.department).AsNoTracking().ToList();
                    }
            else
            {
                return   _dbContext.Set<T>().AsNoTracking().ToList();
            }
            
        }

       
    }
}
