using AspNetTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity.Migrations;

[assembly: OwinStartupAttribute(typeof(AspNetTest.Startup))]
namespace AspNetTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AddAdmin();
        }

        private void AddAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "User" },
                new IdentityRole { Name = "Admin" }
                );

            string name = "admin@test.pl";

            var admin = new ApplicationUser
            {
                UserName = name,
                Email = name
            };
            string pass = "6028717Aa.";

            if (userManager.FindByName(name) != null) return;

            var createResult = userManager.Create(admin, pass);

            if (createResult.Succeeded)
            {
                userManager.AddToRole(admin.Id, "Admin");
            }
        }
    }
}
