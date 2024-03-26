using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entity
{
    public class ChooseQuestion
    {
        public int Id { get; set; }
        public string Choose { get; set; }

        [ForeignKey("Questions")]
        public int Ques_ID { get; set; }
        public Questions Questions { get; set; }
    }
}
