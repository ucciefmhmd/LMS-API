using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;

namespace LMS.BL.DTO
{
    public class InstructorsWithCourseNameDTO
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Experience { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserAttachmentPath { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Specialization { get; set; }
        public List<string> CourseName { get; set; } = new List<string>();
        public List<int> CourseIDs { get; set; } = new List<int>();
    }
}
