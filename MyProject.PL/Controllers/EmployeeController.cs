 using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyProject.BLL.Interfaces;
using MyProject.DAL.Models;
using System;

namespace MyProject.PL.Controllers
{
    public class EmployeeController: Controller
    {

        private readonly IEmployeeRepository _Employeerepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment env)
        {
            _Employeerepository = EmployeeRepository;
            _env = env;
        }

        public IActionResult Index()
        {
            var Employee = _Employeerepository.GetAll();
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid) //server side Validation
            {
                var count = _Employeerepository.Add(Employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(Employee);

        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400
            var Employee = _Employeerepository.Get(id.Value);
            if (Employee is null)
                return NotFound(); //404
            return View(viewname, Employee);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

          
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {

            if (id != employee.Id)
                return BadRequest("An Error ya 7abeb a5ook");
            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                _Employeerepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
              
                         if (_env.IsDevelopment())
                    ModelState.AddModelError(String.Empty, ex.Message);

                else
                    ModelState.AddModelError(String.Empty, "An Error Has Occoured during updating the Employee");

                return View(employee);

            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee Employee)
        {

            try

            {
                _Employeerepository.Delete(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(String.Empty, ex.Message);

                else
                    ModelState.AddModelError(String.Empty, "An Error Has Occoured during Deleting the Employee");

                return View(Employee);

            }
        }
    }
}
