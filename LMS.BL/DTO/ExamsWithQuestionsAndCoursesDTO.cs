using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class ExamsWithQuestionsAndCoursesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public double Max_Degree { get; set; }
        public double Min_Degree { get; set; }
        public ICollection<QuestionCrudDTO> AllQuestion { get; set; }
        public int Course_ID { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}
