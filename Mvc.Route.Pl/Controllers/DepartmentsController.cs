using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Route.Bll.Interfaces;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
       
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            var depa = await unitOfWork.DepartmentRepository.GetAllAsync();
            var result = mapper.Map<IEnumerable<DepartmentViewModel>>(depa);
            return View(result);
        }

        [HttpGet]
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = mapper.Map<Department>(department);
                    await unitOfWork.DepartmentRepository.AddAsync(result);
                    var count = await unitOfWork.saveAsync();
                    if (count > 0)
                    {
                        TempData["CreateDepartment"] = "Department was added successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(department);
        }


        [HttpGet]
        
        public async Task<IActionResult> Details(string ViewName = "Details", int? id = null)
        {

            if(id is null)
            {
                return BadRequest();
            }
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            var result = mapper.Map<DepartmentViewModel>(department);
            if (department is null)
            {
                return NotFound();
            }
            return View(ViewName,result);
        
        }

        [HttpGet]
        
        public async Task<IActionResult> Update(int? id)
        {
            return await Details("Update", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id,DepartmentViewModel department)
        {
            if(id != department.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = mapper.Map<Department>(department);
                    unitOfWork.DepartmentRepository.Update(result);
                    var count = await unitOfWork.saveAsync();
                    if (count > 0)
                    {
                        TempData["EditDepartment"] = "Department was updated successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(department);
        }

        [HttpGet]
        
        public Task<IActionResult> Delete(int? id) {
            return Details("Delete", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id,DepartmentViewModel department)
        {
            if (id != department.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = mapper.Map<Department>(department);
                    unitOfWork.DepartmentRepository.Delete(result);
                    var count = await unitOfWork.saveAsync();
                    if (count > 0)
                    {
                        TempData["DeleteDepartment"] = "Department was removed successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(department);
        }


    }
}
