using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Events")]
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Start_Date { get; set; }
        public DateOnly End_Date { get; set; }
        public string Description { get; set; }
        public string HyperLink { get; set; }
        public ICollection<UserEvent> UserEvent { get; } = new HashSet<UserEvent>();
        public ICollection<EventsCourses> EventsCourses { get; } = new HashSet<EventsCourses>();

    }
}
