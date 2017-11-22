using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentAccom.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace StudentAccom.Migrations.StudentAccomConfiguration {
    internal sealed class StudentAccomConfiguration : DbMigrationsConfiguration<StudentAccom.DAL.StudentAccomContext> {
        public StudentAccomConfiguration() {
            AutomaticMigrationsEnabled = true;
        }


    }
}