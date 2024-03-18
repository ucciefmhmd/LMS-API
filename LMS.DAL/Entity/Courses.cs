using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Courses")]
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public ICollection<Exam> Exam { get; } = new HashSet<Exam>();
        public ICollection<InstructorCourse> InstructorCourse { get; } = new HashSet<InstructorCourse>();
    }
}
