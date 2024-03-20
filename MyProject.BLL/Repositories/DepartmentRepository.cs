using MyProject.BLL.Interfaces;
using MyProject.DAL.Data;
using MyProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MyProject.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private  readonly ProjectDbContext _dbContext; //Null
        public DepartmentRepository(ProjectDbContext DbContext) //ask clr to create object 
        {
            _dbContext = DbContext;
        }

        public int Add(Department entity)
        {
            _dbContext.departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public Department Get(int id)
        {
          var departments = _dbContext.departments.Local.Where(D => D.Id == id).FirstOrDefault();


            return _dbContext.Find<Department>(id); //EF  core 3.1 Feature


            //return _dbContext.departments.Find(id);


            //if (departments == null)
            //{
            //    departments = _dbContext.departments.Where(D => D.Id == id).FirstOrDefault();

            //}
            //return departments;
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.departments.AsNoTracking().ToList();
        }
    }
}
 