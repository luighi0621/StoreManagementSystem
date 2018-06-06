using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagement.Model
{
    public partial class Supplier
    {
        public Supplier()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [StringLength(20, MinimumLength = 10)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Description { get; set; }
        [Display(Name ="Code")]
        [StringLength(20, MinimumLength = 4)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        public string SupplierCode { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
