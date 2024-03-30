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
        public string? UserAttachmentPath { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public DateOnly Start_Date { get; set; }
        public DateOnly End_Date { get; set; }
        public ICollection<Exam> Exam { get; } = new HashSet<Exam>();
        public ICollection<InstructorCourse> InstructorCourse { get; } = new HashSet<InstructorCourse>();
        public ICollection<EventsCourses> EventsCourses { get; } = new HashSet<EventsCourses>();
        
    }
}
