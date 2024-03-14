using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("StudentQuestion")]
    public class StudentQuestion
    {
        public int Id { get; set; }
        [ForeignKey("Students")]
        public int Std_ID { get; set; }

        [ForeignKey("Questions")]
        public int Ques_ID { get; set; }
        public virtual Students Students { get; set; }
        public virtual Questions Questions { get; set; }

    }
}
