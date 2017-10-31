using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentAccom.Models;
using System.Data.Entity;
using StudentAccom.DAL;

namespace StudentAccom.Controllers
{
    public class HomeController : Controller
    {
        private StudentAccomContext Context;
        private DbSet<User> UsersDB;
        private User[] Users;


        public HomeController() {
            Context = new StudentAccomContext();
        
            UsersDB = Context.UsersDB;
        
            UsersDB.Add(new User { ID = 1, Name = "Douglas" });
            Context.SaveChanges();
            Users = UsersDB.ToArray();
        }


        [Route("Home/Index")]
        [HttpGet]
        public ViewResult Index() {
            return View(Users);
        }
    }
}