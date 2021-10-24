using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity
{
    [Table("UserDetail")]
    public class UserDetails
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("City")]
        public string City { get; set; }
    }
}
