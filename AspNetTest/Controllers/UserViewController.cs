using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AspNetTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNetTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: UserView
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var allUsers = db.Users.ToList();
            GroupedUserViewModel grouped = new GroupedUserViewModel()
            {
                Users = new List<UserViewModel>()
            };
            foreach (var user in allUsers)
            {
                string role = userManager.GetRoles(user.Id).FirstOrDefault();
                if (userManager.IsInRole(user.Id, "Admin"))
                    role = "Admin";
                grouped.Users.Add(new UserViewModel { User = user, Role = role });
            }
            return View(grouped);
        }

        public ActionResult EditRole(string id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var roles = new RoleModel()
            {
                User = user,
                Role = new List<IdentityRole>()
            };

            foreach (var role in db.Roles.ToList())
            {
                roles.Role.Add(role);
            }

            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include = "Id")] ApplicationUser user, [Bind(Include = "Role")] string role)
        {
            if (db.Users.FirstOrDefault(u => u.Id == user.Id) == null)
                return HttpNotFound();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (userManager.IsInRole(user.Id, "Admin"))
                userManager.RemoveFromRole(user.Id, "Admin");

            userManager.AddToRole(user.Id, role);

            return RedirectToAction("Index");
        }

    }
}