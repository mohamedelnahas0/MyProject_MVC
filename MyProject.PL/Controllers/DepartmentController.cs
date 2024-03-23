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
        private readonly IDepartmentRepository _departmentrepository;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository, IWebHostEnvironment env)
        {
            _departmentrepository = departmentRepository;
            this._env = env;
        }

        public IActionResult Index()
        {
            var department = _departmentrepository.GetAll();
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
                var count = _departmentrepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);

        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); //400
            var department = _departmentrepository.Get(id.Value);
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
                _departmentrepository.Update(department);
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
                _departmentrepository.Delete(department);
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







