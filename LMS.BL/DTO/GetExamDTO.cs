using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class GetExamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public double Max_Degree { get; set; }
        public double Min_Degree { get; set; }
        public List<QuestionCrudDTO> AllQuestion { get; set; } = new List<QuestionCrudDTO>();
        public int Course_ID { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}
