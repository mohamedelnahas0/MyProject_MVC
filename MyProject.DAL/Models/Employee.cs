using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value="Male")]
        Male=1,
        [EnumMember(Value = "Female")]
        Female =2
    }
   public enum Emptype
    {
        Fulltime=1,
        Parttime=2
    }
    public class Employee:ModelBase
    {
      

        [Required(ErrorMessage ="Name Is Required")] 
        public string Name { get; set; }

        public int? age { get; set; }

        public string Adress { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="IsActive")]
        public  bool IsActive { get; set; }
        [EmailAddress] 
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Phone]
        public String PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }

        public DateTime CreationDay { get; set; }= DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public Gender  gender { get; set; }

        public Emptype Employeetype { get; set;}
        
        public int? DepartmentId { get; set; } //ForigenKey (Nullable)

       //[InverseProperty(nameof(Models.Department.employees))]
        //Navigational Property=>[One] [Related data]
        public Department department { get; set; }

    }
}
