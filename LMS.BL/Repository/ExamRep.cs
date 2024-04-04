using LMS.BL.Interface;
using LMS.DAL.Database;
using LMS.DAL.Entity;
using Microsoft.EntityFrameworkCore;
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
            return db.Exam
                     .Include(e => e.Questions)
                        .ThenInclude(q => q.ChooseQuestion)
                     .ToList();
        }

        public Exam GetById(int id)
        {
            try
            {
                var exam = db.Exam
                         .Include(a => a.Questions)
                         .Include(a => a.StudentExam)
                            .ThenInclude(se => se.Students)
                         .FirstOrDefault(a => a.Id == id);

                if (exam == null)
                    throw new Exception("Exam with provided ID not found.");

                return exam;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving exam by ID.", ex);
            }
        }

        public Exam GetByIdIncludingChooseQuestions(int id)
        {
            try
            {
                var exam = db.Exam
                .Include(e => e.Questions)
                    .ThenInclude(q => q.ChooseQuestion)
                .FirstOrDefault(e => e.Id == id);
                if (exam == null)
                    throw new Exception("Exam with provided ID not found.");

                return exam;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving exam by ID.", ex);
            }
        }

        public Exam GetByName(string name)
        {
            try
            {
                var exam = db.Exam
                         .Include(a => a.Questions)
                         .Include(a => a.StudentExam)
                            .ThenInclude(se => se.Students)
                         .FirstOrDefault(a => a.Name == name);

                if (exam == null)
                    throw new Exception("Exam with provided Name not found.");

                return exam;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving exam by name.", ex);
            }
        }

        public void Update(Exam exam)
        {
            db.Exam.Update(exam);
            db.SaveChanges();
        }
    }
}
