using Microsoft.EntityFrameworkCore;
using MyProject.BLL.Interfaces;
using MyProject.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BLL.Repositories
{
    public class UnitOFWork : IUnitOFWork ,IDisposable
    {
        public IEmployeeRepository employeeRepository { get; set; } = null;
        public IDepartmentRepository departmentRepository { get; set; } = null;
        public ProjectDbContext DbContext { get; }

        public UnitOFWork(ProjectDbContext dbContext)
        {
            employeeRepository = new EmployeeRepository(dbContext);
            departmentRepository = new DepartmentRepository(dbContext);
            DbContext = dbContext;
        }

        public int Compelete()
        {
            return DbContext.SaveChanges();
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }

        
    }
}
