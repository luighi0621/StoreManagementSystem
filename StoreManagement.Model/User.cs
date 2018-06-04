using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.Model
{
    public partial class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] AvatarImage { get; set; }
    }
}
