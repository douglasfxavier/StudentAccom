using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace StudentAccom.Migrations.StudentAccomConfiguration {
    internal sealed class StudentAccomConfiguration : DbMigrationsConfiguration<StudentAccom.DAL.StudentAccomContext> {
        public StudentAccomConfiguration() {
            AutomaticMigrationsEnabled = false;
        }
    }
}