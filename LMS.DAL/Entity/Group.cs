using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Group")]
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Chat { get; set; }

        [ForeignKey("Students")]
        public int Std_ID { get; set; }

        [ForeignKey("InstructorCourse")]
        public int InstCos_ID { get; set; }

        public virtual Students Students { get; set; }
        public virtual InstructorCourse InstructorCourse { get; set; }
    }
}
