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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository

    {
        public EmployeeRepository(ProjectDbContext projectDbContext) : base(projectDbContext)
        {
            
        }
        public IQueryable<Employee> GetEmployeesByAdress(string adress)
        {
            return _dbContext.employees.Where(E => E.Adress.ToLower() == adress.ToLower());
        }

        public IEnumerable<Employee> SearchbyName(string Name)
        => _dbContext.employees.Where(E => E.Name.ToLower().Contains(Name));
    }
}
 