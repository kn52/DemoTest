using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity.Product
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Product> Products { get; set; }
    }
}
