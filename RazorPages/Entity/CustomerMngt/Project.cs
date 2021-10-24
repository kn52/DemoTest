using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Entity.CustomerMngt
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        [Column("ProjectName")]
        public string ProjectName { get; set; }
        [Column("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int PD { get; set; }
        public List<ProjectSkill> ProjectSkills { get; set; }
    }
}
