using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSln.Entity
{
    [Table("UserCred")]
    public class LoginCredentials
    {
        [Key]
        [Column("UserId")]
        public string Id { get; set; }

        [Required]
        [Column("Username")]
        [MaxLength(7,ErrorMessage = "Enter valid  username")]
        [RegularExpression(@"[A-Za-z]{7}", ErrorMessage = "Enter valid  username")]
        public string Username { get; set; }

        [Required]
        [Column("Password")]
        [MaxLength(4, ErrorMessage = "Enter valid  username")]
        public string Password { get; set; }
    }
}
