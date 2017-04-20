using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNetTest.Models
{
    public class RoleModel
    {
        [Display(Name = "Użytkownik")]
        public ApplicationUser User { get; set; }

        [Display(Name = "Rola")]
        public List<IdentityRole> Role { get; set; }
    }

    public class UserViewModel
    {

        [Display(Name = "Użytkownik")]
        public ApplicationUser User { get; set; }

        [Display(Name = "Rola")]
        public string Role { get; set; }
    }

    public class GroupedUserViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }
}