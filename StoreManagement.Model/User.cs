using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreManagement.Model
{
    public partial class User: IdentityUser<int>
    {
        public User()
        {
            UserClaim = new HashSet<UserClaim>();
            UserRol = new HashSet<UserRol>();
        }

        [Required(ErrorMessage = "Debe empezar con mayuscula")]
        [StringLength(30, MinimumLength =5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage ="Debe empezar con mayuscula")]
        public string Firstname { get; set; }
        [StringLength(30, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Lastname { get; set; }
        public byte[] AvatarImage { get; set; }

        public ICollection<UserClaim> UserClaim { get; set; }
        public ICollection<UserRol> UserRol { get; set; }
    }
}
