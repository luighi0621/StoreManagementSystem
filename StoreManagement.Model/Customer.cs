using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagement.Model
{
    public partial class Customer
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        public string Firstname { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Lastname { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Address { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Email { get; set; }
        [RegularExpression(@"^[0-9]*$")]
        public string Phone { get; set; }
        [Display(Name = "Customer Code")]
        [Required]
        public string CustomerCode { get; set; }
    }
}
