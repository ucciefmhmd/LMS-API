using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    [Table("Students")]
    public class Students : Users
    {
        public int Age { get; set; }
        public string Title { get; set; }
        public ICollection<Group> Group { get; } = new HashSet<Group>();
    }
}
