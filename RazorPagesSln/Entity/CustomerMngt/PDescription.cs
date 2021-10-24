using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity.CustomerMngt
{
    [Table("PDescriptions")]
    public class PDescription
    {
        [Key]
        [Column("PDescriptionId")]
        public int PDescriptionId { get; set; }
        [Column("PCode")]
        public string PCode { get; set; }
        [Column("Description")]
        public string Description { get; set; }
    }
}
