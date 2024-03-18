using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("InstructorCourse")]
    public class InstructorCourse
    {
        public int Id { get; set; } 
        [ForeignKey("Instructors")]
        public int inst_ID { get; set; }
        public virtual Instructors Instructors { get; set; }
        [ForeignKey("Courses")]
        public int Course_ID { get; set; }
        public virtual Courses Courses { get; set; }
        public ICollection<Group> Group { get; } = new HashSet<Group>();

    }
}
