using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BLL.Interfaces
{
    public interface IUnitOFWork
    {
        public IEmployeeRepository employeeRepository { get; set; }
        public IDepartmentRepository  departmentRepository { get; set; }

        int Compelete();

        

    }
}
