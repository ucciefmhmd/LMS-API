using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class StudentCrudDTO
    {
        public int Id { get; set; }
        public int SSN { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserAttachmentPath { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<string> CourseName { get; set; } = new List<string>();
        public List<int> InstructorIDs { get; set; } = new List<int>();

    }
}
