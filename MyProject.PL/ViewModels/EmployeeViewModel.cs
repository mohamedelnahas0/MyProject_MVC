using MyProject.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MyProject.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

         
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        public int? age { get; set; }

        public string Adress { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [Phone]
        public String PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }

        public Gender gender { get; set; }

        public int? DepartmentId { get; set; } 

      
        public Department department { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        
    }
}
