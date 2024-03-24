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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ProjectDbContext projectDbContext) : base(projectDbContext)
        {

        }
    }
}
 