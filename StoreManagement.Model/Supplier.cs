using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagement.Model
{
    public partial class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SupplierCode { get; set; }
    }
}
