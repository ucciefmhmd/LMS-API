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
    public class ExamRep : IExamRep
    {
        private readonly LMSContext db;

        public ExamRep(LMSContext db)
        {
            this.db = db;
        }

        public void Add(Exam exam)
        {
            db.Exam.Add(exam);
            db.SaveChanges();
        }

        public void Delete(Exam exam)
        {
            db.Exam.Remove(exam);
            db.SaveChanges();
        }

        public IEnumerable<Exam> GetAllData()
        {
            return db.Exam.Select(a => a);
        }

        public Exam GetById(int id)
        {
            return db.Exam.Find(id);
        }

        public void Update(Exam exam)
        {
            db.Exam.Update(exam);
            db.SaveChanges();
        }
    }
}
