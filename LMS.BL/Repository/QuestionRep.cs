using LMS.BL.Interface;
using LMS.DAL.Database;
using LMS.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LMS.BL.Repository
{
    public class QuestionRep : IQuestionRep
    {
        private readonly LMSContext db;

        public QuestionRep(LMSContext db)
        {
            this.db = db;
        }

        public void Add(Questions ques)
        {
            db.Questions.Add(ques);
            db.SaveChanges();
        }

        public void Delete(Questions ques)
        {
            db.Questions.Remove(ques);
            db.SaveChanges();
        }

        public IEnumerable<Questions> GetAllData()
        {
            return db.Questions.Include(a => a.Exam).Include(a => a.ChooseQuestion).Include(a=>a.Exam.Courses).ToList();
        }

        public IEnumerable<Questions> GetByExamId(int examId)
        {
            return db.Questions.Where(q => q.Exam_ID == examId).ToList();
        }
        public Questions GetById(int id)
        {
            try
            {
                var question = db.Questions.Include(a => a.Exam).Include(a => a.ChooseQuestion).FirstOrDefault(a => a.Id == id);

                if (question == null)
                    throw new Exception("Question with provided ID not found.");

                return question;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving question by ID.", ex);
            }
        }

        public void Update(Questions ques)
        {
            db.Questions.Update(ques);
            db.SaveChanges();
        }
    }
}
