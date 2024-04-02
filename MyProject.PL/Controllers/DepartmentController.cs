using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyProject.BLL.Interfaces;
using MyProject.BLL.Repositories;
using MyProject.DAL.Models;
using System;

namespace MyProject.PL.Controllers
{

    public class DepartmentController : Controller
    {
       
        private readonly IUnitOFWork _unitOFWork;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOFWork unitOFWork, IWebHostEnvironment env)
        {
         
            this._unitOFWork = unitOFWork;
            this._env = env;
        }

        public IActionResult Index()
        {
            var department = _unitOFWork.departmentRepository.GetAll();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) //server side Validation
            {
                _unitOFWork.departmentRepository.Add(department);

                _unitOFWork.Compelete();
                return RedirectToAction(nameof(Index));
            }

            return View(department);

        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400
            var department = _unitOFWork.departmentRepository.Get(id.Value);
            if (department is null)
                return NotFound(); //404
            return View(viewname, department);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

            //if (!id.HasValue)
            //    return BadRequest();
            //var department =_departmentrepository.Get(id.Value);
            //if (department is null)
            //{
            //    return NotFound();
            //}
            //return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {

            if (id != department.Id)
                return BadRequest("An Error ya 7abeb a5ook");
            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _unitOFWork.departmentRepository.Update(department);
                _unitOFWork.Compelete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //1.log exception in development mode
                if (_env.IsDevelopment())
                    ModelState.AddModelError(String.Empty, ex.Message);

                else
                    ModelState.AddModelError(String.Empty, "An Error Has Occoured during updating the Department");

                return View(department);

            }
        }

            [HttpGet]
            public IActionResult Delete(int? id)
            {
                return Details(id , "Delete");
            }
        [HttpPost]
        public IActionResult Delete(Department department)
        {

            try
            
            {
                _unitOFWork.departmentRepository.Delete(department);
                _unitOFWork.Compelete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(String.Empty, ex.Message);

                else
                    ModelState.AddModelError(String.Empty, "An Error Has Occoured during Deleting the Department");

                return View(department);

            }
        }
        }
    }







