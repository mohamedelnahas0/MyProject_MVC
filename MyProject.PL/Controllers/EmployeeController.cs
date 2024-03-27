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
        //private readonly IDepartmentRepository _departmentrepo;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment env/*, IDepartmentRepository departmentrepo*/)
        {
            _Employeerepository = EmployeeRepository;
            _env = env;
            //_departmentrepo = departmentrepo;
        }
     
        public IActionResult Index()
        {
            TempData.Keep();
            var Employee = _Employeerepository.GetAll();
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Department"]=_departmentrepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid) //server side Validation
            {
                var count = _Employeerepository.Add(Employee);
                if (count > 0)
                
                    TempData["Message"] = "Employees Is Added Succesfuly";
                
                else
                {
                    TempData["Message"] = "Ann Error Occoured , Employees not Added";
                }
                return RedirectToAction(nameof(Index));

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
            //ViewData["Department"] = _departmentrepo.GetAll();
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
