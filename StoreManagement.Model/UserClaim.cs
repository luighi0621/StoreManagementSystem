using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Model
{
    [NotMapped]
    public class UserClaim: IdentityUserClaim<int>
    {
        public User User { get; set; }

    }
}
