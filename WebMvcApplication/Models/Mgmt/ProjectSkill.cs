using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcApplication.Models.Mgmt
{
    [Table("Projectskills")]
    public class ProjectSkill
    {
        [Key]
        [Column("ProjectSkillId")]
        public int ProjectSkillId { get; set; }
        [Column("ProjectId")]
        public int ProjectId { get; set; }
        public int SD { get; set; }
        public Project Project { get; set; }
    }
}
