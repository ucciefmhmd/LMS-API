using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Questions")]
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string questionType { get; set; }
        public string CorrectAnswer { get; set; }
        [ForeignKey("Exam")]
        public int? Exam_ID { get; set; }
        public virtual Exam Exam { get; set; }
        public ICollection<StudentQuestion> StudentQuestion { get; } = new HashSet<StudentQuestion>();
        public ICollection<ChooseQuestion> ChooseQuestion { get; } = new HashSet<ChooseQuestion>();

    }
}
