using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreManagement.Model
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe empezar con mayuscula")]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage ="Debe empezar con mayuscula")]
        public string Firstname { get; set; }
        [StringLength(30, MinimumLength = 4)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Lastname { get; set; }
        [StringLength(30, MinimumLength = 4)]
        public string Login { get; set; }
        public byte[] AvatarImage { get; set; }
        
    }
}
