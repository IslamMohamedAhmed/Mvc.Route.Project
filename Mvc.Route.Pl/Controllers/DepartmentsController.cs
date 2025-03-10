using Microsoft.AspNetCore.Mvc;
using Mvc.Route.Bll.Interfaces;
using Mvc.Route.Dal.Models;

namespace Mvc.Route.Pl.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var depa = departmentRepository.GetAll();
            return View(depa);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }


            }
            return View(department);
        }


    }
}
