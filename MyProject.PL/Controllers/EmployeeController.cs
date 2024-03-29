using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyProject.BLL.Interfaces;
using MyProject.DAL.Models;
using MyProject.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.PL.Controllers
{
    public class EmployeeController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _Employeerepository;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentrepo;

        public EmployeeController( IMapper mapper,IEmployeeRepository EmployeeRepository, IWebHostEnvironment env/*, IDepartmentRepository departmentrepo*/)
        {
            _mapper = mapper;
            _Employeerepository = EmployeeRepository;
            _env = env;
            //_departmentrepo = departmentrepo;
        }
     
        public IActionResult Index(string SearchInput)
        {
            var emp = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(SearchInput))
                emp = _Employeerepository.GetAll();      
            else 
            
                emp = _Employeerepository.SearchbyName(SearchInput.ToLower());
                var mappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);
                return View(mappedEmps);
            
                        
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Department"]=_departmentrepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel EmployeeVm)
        {
            if (ModelState.IsValid) //server side Validation
            {

                var mappedEmp = _mapper.Map<EmployeeViewModel ,Employee >(EmployeeVm);
                var count = _Employeerepository.Add(mappedEmp);
                if (count > 0)
                
                    TempData["Message"] = "Employees Is Added Succesfuly";
                
                else
                {
                    TempData["Message"] = "Ann Error Occoured , Employees not Added";
                }
                return RedirectToAction(nameof(Index));

            }

            return View(EmployeeVm);

        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400
            var Employee = _Employeerepository.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);


            if (mappedEmp is null)
                return NotFound(); //404
            return View(viewname, mappedEmp);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //ViewData["Department"] = _departmentrepo.GetAll();
            return Details(id, "Edit");
          


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVm)
        {

            if (id != EmployeeVm.Id)
                return BadRequest("An Error ya 7abeb a5ook");
            if (!ModelState.IsValid)
               

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);

                _Employeerepository.Update(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
              
                         if (_env.IsDevelopment())
                    ModelState.AddModelError(String.Empty, ex.Message);

                else
                    ModelState.AddModelError(String.Empty, "An Error Has Occoured during updating the Employee");


            }

            return View(EmployeeVm); 
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel EmployeeVm)
        {

            try

            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);

                _Employeerepository.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(String.Empty, ex.Message);

                else
                    ModelState.AddModelError(String.Empty, "An Error Has Occoured during Deleting the Employee");

                return View(EmployeeVm);

            }
        }
    }
}
