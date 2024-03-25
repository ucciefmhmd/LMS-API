using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.DTO
{
    public class QuestionWithExamNameDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string questionType { get; set; }
        public string CorrectAnswer { get; set; }
        public int Exam_ID { get; set; }
        public string ExamName { get; set; }
        public string chooseOne { get; set; }
        public string chooseTwo { get; set; }
        public string chooseThree { get; set; }
        public string chooseFour { get; set; }
    }
}
