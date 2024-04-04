using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class StudentWithCourseDTO
    {
        //public int CourseId { get; set; }
        //public int InstructorId { get; set; }
        
        public List<string> CourseName { get; set; } = new List<string>();
        public List<int> InstructorIDs { get; set; } = new List<int>();


    }
}
