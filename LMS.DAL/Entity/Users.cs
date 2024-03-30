using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Users")]
    public class Users
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public string Role { get; set; }
        public string SSN { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserAttachmentPath { get; set; }
        public ICollection<UserEvent> UserEvent { get; } = new HashSet<UserEvent>();

    }
}
