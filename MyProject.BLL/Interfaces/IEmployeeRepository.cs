using MyProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BLL.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
       
        IQueryable<Employee>GetEmployeesByAdress(string adress);

        IEnumerable<Employee> SearchbyName(string Name);
    }
}
