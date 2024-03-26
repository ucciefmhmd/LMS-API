using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class QuestionCrudDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string questionType { get; set; }
        public string CorrectAnswer { get; set; }
        public int Exam_ID { get; set; }
        public List<string> ChoosesName { get; set; } = new List<string>();

    }
}
