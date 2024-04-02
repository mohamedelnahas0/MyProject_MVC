using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyProject.BLL.Interfaces;
using MyProject.DAL.Models;
using MyProject.PL.Helpers;
using MyProject.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace MyProject.PL.Controllers
{
    public class EmployeeController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOFWork _unitOFWork;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentrepo;

        public EmployeeController( IMapper mapper, IUnitOFWork unitOFWork, IWebHostEnvironment env/*, IDepartmentRepository departmentrepo*/)
        {
            _mapper = mapper;
            _unitOFWork = unitOFWork;
            _env = env;
            //_departmentrepo = departmentrepo;
        }
     
        public IActionResult Index(string SearchInput)
        {
            var emp = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(SearchInput))
                emp = _unitOFWork.employeeRepository.GetAll();      
            else 
            
                emp = _unitOFWork.employeeRepository.SearchbyName(SearchInput.ToLower());
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
                EmployeeVm.ImageName= DocumentSetting.UpLoadFile(EmployeeVm.Image, "images");


                var mappedEmp = _mapper.Map<EmployeeViewModel ,Employee >(EmployeeVm);
               
                  _unitOFWork.employeeRepository.Add(mappedEmp);

                _unitOFWork.Compelete();
                return RedirectToAction(nameof(Index));

            }

            return View(EmployeeVm);

        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400
            var Employee = _unitOFWork.employeeRepository.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);


            if (mappedEmp is null)
                return NotFound(); //404

            if(viewname.Equals("Delete" , StringComparison.OrdinalIgnoreCase))
            TempData["ImageName"] = Employee.ImageName;

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
                return View(EmployeeVm);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);

                    _unitOFWork.employeeRepository.Update(mappedEmp);
                    _unitOFWork.Compelete();
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
                EmployeeVm.ImageName = TempData["ImageName"] as string;

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVm);

                _unitOFWork.employeeRepository.Delete(mappedEmp);
                 var count =_unitOFWork.Compelete();
                if (count > 0)
                {
                    DocumentSetting.DeleteFile(EmployeeVm.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                return View(EmployeeVm);
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
