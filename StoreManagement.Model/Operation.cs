using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StoreManagement.Model
{
    public partial class Operation
    {
        public int Id { get; set; }
        [Display(Name = "Operation Name")]
        public string Operation1 { get; set; }
        public DateTime Date { get; set; }
        public string Table { get; set; }
        public string Description { get; set; }
    }
}
