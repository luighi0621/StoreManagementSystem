using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.Model
{
    public class Rol: Microsoft.AspNetCore.Identity.IdentityRole<int>
    {
        public Rol()
        {
            UserRol = new HashSet<UserRol>();
        }

        public ICollection<UserRol> UserRol { get; set; }

    }
}
