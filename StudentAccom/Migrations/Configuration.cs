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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentAccom.Models.ApplicationDbContext context) {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            const string adminName = "admin@studentaccom.com";
            const string adminPassword = "Admin@1";
            const string adminRoleName = "Admin";

            //Create Role Admin if it does not exist
            var adminRole = roleManager.FindByName(adminRoleName);
            if (adminRole == null) {
                adminRole = new IdentityRole(adminRoleName);
                var roleresult = roleManager.Create(adminRole);
            }

            var adminUser = userManager.FindByName(adminName);
            if (adminUser == null) {
                adminUser = new ApplicationUser {
                    UserName = adminName,
                    Email = adminName,
                    FirstName = "Admin",
                    LastName = "Admin"
                };
                var result = userManager.Create(adminUser, adminPassword);
                result = userManager.SetLockoutEnabled(adminUser.Id, false);
            }

            //Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(adminUser.Id);
            if (!rolesForUser.Contains(adminRole.Name)) {
                var result = userManager.AddToRole(adminUser.Id, adminRole.Name);
            }


            //Create Role Landlord
            const string landlordUserRoleName = "Landlord";
            var landlordRole = roleManager.FindByName(landlordUserRoleName);
            if (landlordRole == null) {
                landlordRole = new IdentityRole(landlordUserRoleName);
                var roleresult = roleManager.Create(landlordRole);
            }


            //Creating Role AccommodationOfficer if it does not exist
            const string officerUserRoleName = "AccommodationOfficer";
            var officerRole = roleManager.FindByName(officerUserRoleName);
            if (officerRole == null) {
                officerRole = new IdentityRole(officerUserRoleName);
                var roleresult = roleManager.Create(officerRole);
            }

            //Creating user AccommodationOfficer if it does not exist
            const string officerName = "officer@studentaccom.com";
            const string officerPassword = "Officer@1";
            var officerUser = userManager.FindByName(officerName);

            if (officerUser == null) {
                officerUser = new ApplicationUser {
                    UserName = officerName,
                    Email = officerName,
                    FirstName = "Accommodation",
                    LastName = "Officer"
                };

                var result = userManager.Create(officerUser, officerPassword);
                result = userManager.SetLockoutEnabled(officerUser.Id, false);
            }

            //Add user Officer to Role AccommodationOfficer if not already added
            var rolesForOfficerUser = userManager.GetRoles(officerUser.Id);
            if (!rolesForUser.Contains(officerRole.Name)) {
                var result = userManager.AddToRole(officerUser.Id, officerRole.Name);
            }
        }
    }
}