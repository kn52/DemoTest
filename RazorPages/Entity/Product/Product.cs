using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Entity.Product
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
