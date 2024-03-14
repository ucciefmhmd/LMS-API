using LMS.BL.Interface;
using LMS.DAL.Database;
using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return db.Questions.Select(a => a);
        }

        public Questions GetDyId(int id)
        {
            return db.Questions.Find(id);
        }

        public void Update(Questions ques)
        {
            db.Questions.Update(ques);
            db.SaveChanges();
        }
    }
}
