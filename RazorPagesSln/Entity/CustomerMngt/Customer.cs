using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity.CustomerMngt
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Column("CustomerId")]
        public int CustomerId { get; set; }
        [Column("CustomerName")]
        public string CustomerName { get; set; }
        [Column("CustomerContact")]
        public string CustomerContact { get; set; }
        [Column("CustomerPhone")]
        public string CustomerPhone { get; set; }
        [Column("CustomerEmail")]
        public string CustomerEmail { get; set; }
        public List<Project> Projects { get; set; }
    }
}
