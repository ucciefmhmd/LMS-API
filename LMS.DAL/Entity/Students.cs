using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Students")]
    public class Students
    {
        public int Age { get; set; }
        public string Title { get; set; }
        [Key, ForeignKey("Users")]
        public int userID { get; set; }
        public virtual Users Users { get; set; }
        public ICollection<Group> Group { get; } = new HashSet<Group>();
        public ICollection<StudentExam> StudentExam { get; } = new HashSet<StudentExam>();
        public ICollection<StudentQuestion> StudentQuestion { get; } = new HashSet<StudentQuestion>();
    }
}
