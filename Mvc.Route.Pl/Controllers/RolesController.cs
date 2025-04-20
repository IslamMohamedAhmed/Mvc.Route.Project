using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Route.Bll.Repositories;
using Mvc.Route.Dal.Models;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            var role = Enumerable.Empty<RoleViewModel>();
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                role = await roleManager.Roles.Where(w => w.Name.ToLower().Contains(searchValue.ToLower())).Select(u => new RoleViewModel()
                {
                    Id = u.Id,
                    RoleName = u.Name,
                  
                }).ToListAsync();



            }
            else
            {

                role = await roleManager.Roles.Select(u => new RoleViewModel()
                {
                    Id = u.Id,
                    RoleName = u.Name,
                }).ToListAsync();
            }
            return View(role);
        }
        [HttpGet]





        [HttpGet]

        public async Task<IActionResult> Details(string ViewName = "Details", string? id = null)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound();
            }
            var res = new RoleViewModel() { 
            Id = role.Id,
            RoleName = role.Name
            };

            
            return View(ViewName, res);
        }
        [HttpGet]

        public async Task<IActionResult> Update(string? id)
        {


            return await Details("Update", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string? id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {


                    var role = await roleManager.FindByIdAsync(id);
                    if (role is null)
                    {
                        return NotFound();
                    }
                    role.Name = roleViewModel.RoleName;
                    var res = await roleManager.UpdateAsync(role);

                    if (res.Succeeded)
                    {
                        TempData["EditRole"] = "role was updated successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(roleViewModel);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(string? id)
        {
            return await Details("Delete", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(roleViewModel.Id);
                    if (role is null)
                    {
                        return NotFound();
                    }

                    var res = await roleManager.DeleteAsync(role);

                    if (res.Succeeded)
                    {
                        TempData["DeleteRole"] = "Role was removed successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(roleViewModel);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = new IdentityRole()
                    {
                        Name = role.RoleName
                    };
                    var res = await roleManager.CreateAsync(result); 
                    if (res.Succeeded)
                    {
                        TempData["CreateRole"] = "Role was added successfully!";
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }


            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if(role is null)
            {
                return NotFound();
            }
            var usersInRole = new List<UserInRoleViewModel>();
            ViewBag.roleId = roleId;
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users) {
                var userInRole = new UserInRoleViewModel();
                userInRole.UserId = user.Id;
                userInRole.UserName = user.UserName;
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;

                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleViewModel> users)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var res = await userManager.FindByIdAsync(user.UserId);
                    if (res is not null)
                    {
                        if (user.IsSelected && !await userManager.IsInRoleAsync(res, role.Name))
                        {
                            await userManager.AddToRoleAsync(res, role.Name);
                        }
                        else if (!user.IsSelected && await userManager.IsInRoleAsync(res, role.Name))
                        {
                            await userManager.RemoveFromRoleAsync(res, role.Name);
                        }
                    }

                }
                return RedirectToAction("Update", new { id = roleId });
            }
            ModelState.AddModelError(string.Empty, "Invaid Operation");
            return View(users);

        }

    }
}
