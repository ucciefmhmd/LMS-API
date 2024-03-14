using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Instructors")]
    public class Instructors : Users
    {
        public string Specialization { get; set; }
    }
}
