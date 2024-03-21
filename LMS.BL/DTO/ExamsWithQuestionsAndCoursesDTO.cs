﻿using LMS.DAL.Entity;
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
        public DateTime Date { get; set; }
        public double Max_Degree { get; set; }
        public double Min_Degree { get; set; }
        public List<int> StudentIDs { get; set; } = new List<int>();
        public int Course_ID { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}