using StudentAccom.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentAccom.DAL {

    public class StudentAccomContext : DbContext {
        public StudentAccomContext() : base("StudentAccomDB") { }
        public DbSet<User> UsersDB { set; get; }
    }
}