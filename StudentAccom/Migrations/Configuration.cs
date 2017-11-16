namespace StudentAccom.Migrations {
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StudentAccom.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentAccom.Models.ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudentAccom.Models.ApplicationDbContext context) {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            const string name = "admin@studentaccom.com";
            const string password = "Admin1@studentaccom.com";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null) {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null) {
                user = new ApplicationUser {
                    UserName = name,
                    Email = name,
                    FirstName = "Admin",
                    LastName = "Admin"
                };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            //Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name)) {
                var result = userManager.AddToRole(user.Id, role.Name);
            }

            //Create Role Landlord
            const string userRoleName1 = "Landlord";
            role = roleManager.FindByName(userRoleName1);
            if (role == null) {
                role = new IdentityRole(userRoleName1);
                var roleresult = roleManager.Create(role);
            }

            //Create Role AccomodationOfficer
            const string userRoleName2 = "AccomodationOfficer";
            role = roleManager.FindByName(userRoleName2);
            if (role == null) {
                role = new IdentityRole(userRoleName2);
                var roleresult = roleManager.Create(role);
            }
        }
    }
}