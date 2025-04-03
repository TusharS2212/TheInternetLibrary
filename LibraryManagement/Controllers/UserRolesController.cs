using LibraryManagement.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class UserRolesController : Controller
    {
        private  ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UserRolesController() { }
        public UserRolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
          
            UserManager = userManager;
            RoleManager = roleManager;
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
       
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            var users = await UserManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.Id = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<RoleViewModel>();
            foreach (var role in RoleManager.Roles.ToList())
            {
                var userRolesViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                if (await UserManager.IsInRoleAsync(userId, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Manage(List<RoleViewModel> model, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
           
            var roles = (await UserManager.GetRolesAsync(userId)).ToArray();
            var result = await UserManager.RemoveFromRolesAsync(userId, roles);
 
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            //put all selected in an array to update the roles

            var modelrole = model.Where(c => c.Selected).Select(y => y.Name).ToArray();
            result = await UserManager.AddToRolesAsync(user.Id, modelrole);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await UserManager.GetRolesAsync(user.Id));
        }
    }
}