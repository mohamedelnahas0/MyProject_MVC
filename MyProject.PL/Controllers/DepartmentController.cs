using Microsoft.AspNetCore.Mvc;
using MyProject.BLL.Interfaces;
using MyProject.BLL.Repositories;
using MyProject.DAL.Models;

namespace MyProject.PL.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentrepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
          _departmentrepository = departmentRepository;
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
               var count=  _departmentrepository.Add(department);
                if (count > 0)
                {
                   return RedirectToAction(nameof(Index));
                }
            }
           
            return View(department);

        }
        [HttpGet]
        public IActionResult Details (int? id)
        {
            if (!id.HasValue)
            
                return BadRequest(); //400

                 var department = _departmentrepository.Get(id.Value);

                if (department is null)
                {
                    return NotFound(); //404
                }

                return View(department);
            }
        }

    }
