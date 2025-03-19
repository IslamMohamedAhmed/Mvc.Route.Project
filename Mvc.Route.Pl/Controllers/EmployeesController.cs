using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Route.Bll.Interfaces;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Helper;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeesController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        //[HttpGet]

        public async Task<IActionResult> Index(string searchValue)
        {
            var emp = Enumerable.Empty<EmployeeViewModel>();
            if (string.IsNullOrEmpty(searchValue))
            {
                var res = await unitOfWork.EmployeeRepository.GetAllAsync();
             emp = mapper.Map<IEnumerable<EmployeeViewModel>>(res);

            }
            else
            {
                 var res = await unitOfWork.EmployeeRepository.GetByNameAsync(searchValue);
                emp = mapper.Map<IEnumerable<EmployeeViewModel>>(res);

            }
            return View(emp);
        }

        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var res = await unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.departments = mapper.Map<IEnumerable<DepartmentViewModel>>(res);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(employee.Image is not null)
                    {
                        employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");
                    }
                    var result = mapper.Map<Employee>(employee);
                    await unitOfWork.EmployeeRepository.AddAsync(result);
                    var count = await unitOfWork.saveAsync();
                    if (count > 0)
                    {
                        TempData["CreateEmployee"] = "Employee was added successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(employee);
        }


        [HttpGet]

        public async Task<IActionResult> Details(string ViewName = "Details", int? id = null)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            var result = mapper.Map<EmployeeViewModel>(employee);
            if (employee is null)
            {
                return NotFound();
            }
            return View(ViewName, result);

        }

        [HttpGet]

        public async Task<IActionResult> Update(int? id)
        {
            var res = await unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.departments = mapper.Map<IEnumerable<DepartmentViewModel>>(res);

            return await Details("Update", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                   
                    if(employee.Image is not null)
                    {
                        if (employee.ImageName is not null)
                        {
                            DocumentSettings.DeleteFile(employee.ImageName, "Images");
                        }
                        employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");
                    }
                    var result = mapper.Map<Employee>(employee);
                    unitOfWork.EmployeeRepository.Update(result);
                    var count = await unitOfWork.saveAsync();
                    if (count > 0)
                    {
                        TempData["EditEmployee"] = "Employee was updated successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(employee);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details("Delete", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = mapper.Map<Employee>(employee);
                    unitOfWork.EmployeeRepository.Delete(result);
                    var count = await unitOfWork.saveAsync();
                    if (count > 0)
                    {
                        if (employee.ImageName is not null)
                        {
                            DocumentSettings.DeleteFile(employee.ImageName, "Images");
                        }
                        TempData["DeleteEmployee"] = "Employee was removed successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(employee);
        }
    }
}
