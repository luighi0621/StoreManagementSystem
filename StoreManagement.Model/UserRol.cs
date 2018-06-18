using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreManagement.Model
{
    public class UserRol: Microsoft.AspNetCore.Identity.IdentityUserRole<int>
    {
        [Key]
        public override int RoleId { get => base.RoleId; set => base.RoleId = value; }
        [Key]
        public override int UserId { get => base.UserId; set => base.UserId = value; }
        public Rol Role { get; set; }
        public User User { get; set; }
    }
}
