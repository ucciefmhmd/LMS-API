using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class StudentWithExamAndInstrcutorCourses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Photo { get; set; }
        public List<string> CourseName { get; set; } = new List<string>();
        public List<string> GroupName { get; set; } = new List<string>();
        public List<string> ExamName { get; set; } = new List<string>();

    }
}
