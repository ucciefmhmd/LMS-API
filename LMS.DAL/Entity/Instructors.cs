using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Instructors")]
    public class Instructors
    {
        public string Specialization { get; set; }
        [Key, ForeignKey("Users")]
        public int userID { get; set; }
        public string Experience { get; set; }
        public virtual Users Users { get; set; }
        public ICollection<InstructorCourse> InstructorCourse { get; } = new HashSet<InstructorCourse>();

    }
}
