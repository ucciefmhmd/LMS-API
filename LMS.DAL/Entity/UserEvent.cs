using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("UserEvent")]
    public class UserEvent
    {
        [ForeignKey("Users")]
        public int user_ID { get; set; }
        public virtual Users Users { get; set; }
        [ForeignKey("Events")]
        public int event_ID { get; set; }
        public virtual Events Events { get; set; }
    }
}
