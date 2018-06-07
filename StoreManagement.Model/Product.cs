using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreManagement.Model
{
    public partial class Product
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(20, MinimumLength = 10)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 10)]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "Code")]
        public string ProductCode { get; set; }
        [Display(Name = "Supplier")]
        public int IdSupplier { get; set; }
        [ForeignKey("IdSupplier")]
        public virtual Supplier Supplier { get; set; }
    }
}
