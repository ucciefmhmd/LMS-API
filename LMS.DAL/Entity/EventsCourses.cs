using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    public class EventsCourses
    {
        public int Id { get; set; }

        [ForeignKey("Events")]
        public int Event_ID { get; set; }
        public virtual Events Events { get; set; }
        [ForeignKey("Courses")]
        public int Course_ID { get; set; }
        public virtual Courses Courses { get; set; }

    }
}
