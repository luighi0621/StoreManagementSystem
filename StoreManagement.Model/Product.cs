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
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public byte[] Image { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public int IdSupplier { get; set; }
        [ForeignKey("IdSupplier")]
        public virtual Supplier Supplier { get; set; }
    }
}
