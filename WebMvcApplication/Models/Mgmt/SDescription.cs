﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcApplication.Models.Mgmt
{
    [Table("SDescriptions")]
    public class SDescription
    {
        [Key]
        [Column("SDescriptionId")]
        public int SDescriptionId { get; set; }
        [Column("SCode")]
        public string SCode { get; set; }
        [Column("Description")]
        public string Description { get; set; } 
    }
}
