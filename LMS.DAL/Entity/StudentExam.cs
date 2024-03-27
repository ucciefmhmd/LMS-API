using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("StudentExam")]
    public class StudentExam
    {
        public int Id { get; set; }
        [ForeignKey("Students")]
        public int Std_ID { get; set; }
        [ForeignKey("Exam")]
        public int Exam_ID { get; set; }
        public int Result { get; set; }
        public virtual Students Students { get; set; }
        public virtual Exam Exam { get; set; }

    }
}
