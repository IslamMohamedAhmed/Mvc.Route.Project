using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Route.Bll.Repositories;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Helper;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            var user = Enumerable.Empty<UserViewModel>();
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                user = await userManager.Users.Where(w=>w.Email.ToLower().Contains(searchValue.ToLower())).Select(u=> new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = userManager.GetRolesAsync(u).Result
                }).ToListAsync();
                
                

            }
            else
            {
               
                user = await userManager.Users.Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = userManager.GetRolesAsync(u).Result
                }).ToListAsync();
            }
            return View(user);
        }
        [HttpGet]

  



        [HttpGet]

        public async Task<IActionResult> Details(string ViewName = "Details", string? id = null)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var user  = await userManager.FindByIdAsync(id);
            if(user is null)
            {
                return NotFound();
            }
            var res = mapper.Map<UserViewModel>(user);
            res.Roles = await userManager.GetRolesAsync(user);
            return View(ViewName, res);
        }
        [HttpGet]

        public async Task<IActionResult> Update(string? id)
        {
           

            return await Details("Update", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {


                    var user = await userManager.FindByIdAsync(id);
                    if (user is null)
                    {
                        return NotFound();
                    }
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.Email = userViewModel.Email;
                    var res = await userManager.UpdateAsync(user);
                    
                    if (res.Succeeded)
                    {
                        TempData["EditUser"] = "User was updated successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(userViewModel);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(string? id)
        {
            return await Details("Delete", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(userViewModel.Id);
                    if (user is null)
                    {
                        return NotFound();
                    }
                    
                    var res = await userManager.DeleteAsync(user);

                    if (res.Succeeded)
                    {
                        TempData["DeleteUser"] = "User was removed successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(userViewModel);
        }
    }
}
