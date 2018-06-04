using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.Model
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string ProductCode { get; set; }
    }
}
