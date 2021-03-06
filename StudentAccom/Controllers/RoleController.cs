﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Net;
using StudentAccom.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace StudentAccom.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {

        public RoleController() {

        }

        public RoleController(ApplicationUserManager userManager, ApplicationRoleManager roleManager) {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager {
            get {
                return _userManager ??
                        HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager {
            get {
                return _roleManager ??
                    HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
             }
             private set {
                _roleManager = value;
             }
        }

        
        public ActionResult List()
        {
            return View(RoleManager.Roles);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();
            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList()) {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name)) {
                    users.Add(user);
                }
            }
            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ViewModel roleViewModel) {
            if (ModelState.IsValid) {
                var role = new IdentityRole(roleViewModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded) {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("List");
            }
            return View();
        }

        
        [HttpGet]
        public async Task<ActionResult> Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null) {
                return HttpNotFound();
            }
            ViewModel roleModel = new ViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ViewModel roleModel) {
            if (ModelState.IsValid) {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null) {
                return HttpNotFound();
            }
            return View(role);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id) {
            if (ModelState.IsValid) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null) {
                    return HttpNotFound();
                }
                IdentityResult result = await RoleManager.DeleteAsync(role);
                
                if (!result.Succeeded) {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("List");
            }
            return View();
        }
    }
}
