using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace StoreManagement.Model
{
    public class UserClaim: IdentityUserClaim<int>
    {
        public User User { get; set; }

    }
}
