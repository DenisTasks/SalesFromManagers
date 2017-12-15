using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityApp.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole {Name = "Admin"};
            var role2 = new IdentityRole { Name = "Manager" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser {Email = "admin@yandex.ru", UserName = "admin@yandex.ru"};
            string password = "Square007!";
            var result = userManager.Create(admin, password);

            var manager = new ApplicationUser { Email = "manager@yandex.ru", UserName = "manager@yandex.ru" };
            string password2 = "Square007!";
            var result2 = userManager.Create(manager, password2);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }

            if (result2.Succeeded)
            {
                userManager.AddToRole(manager.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}