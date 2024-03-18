using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Exam")]
    public class Exam
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public double Max_Degree { get; set; }
        public double Min_Degree { get; set; }
        [ForeignKey("Courses")]
        public int Course_ID { get; set; }
        public virtual Courses Courses { get; set; }
        public ICollection<Questions> Questions { get; } = new HashSet<Questions>();
        public ICollection<StudentExam> StudentExam { get; } = new HashSet<StudentExam>();


    }
}
