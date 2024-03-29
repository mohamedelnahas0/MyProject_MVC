using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DAL.Models
{
    public class Department:ModelBase
    {
      
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        //[InverseProperty(nameof(Employee.department))]
        //Navigational Property =>[Many] and intialize it
        public ICollection<Employee>  employees { get; set; } = new HashSet<Employee>();
    }
}
