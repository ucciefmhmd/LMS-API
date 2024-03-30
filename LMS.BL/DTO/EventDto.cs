using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Start_Date { get; set; }
        public DateOnly End_Date { get; set; }
        public string Description { get; set; }
        public string HyperLink { get; set; }
        public List<string> CoursesName { get; set; } = new List<string>();
    }
}
