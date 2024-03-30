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
    public class StudentRep : IStudentRep
    {
        private readonly LMSContext db;
        private readonly IUploadFile uploadFile;

        public StudentRep(LMSContext db, IUploadFile uploadFile)
        {
            this.db = db;
            this.uploadFile = uploadFile;
        }

        public void Add(Students std)
        {
            db.Add(std);
            db.SaveChanges();
        }

        public void Delete(Students std)
        {
            db.Remove(std);
            db.SaveChanges();
        }

        public IEnumerable<Students> GetAllData()
        {
            return db.Students
                .Include(s => s.Users)
                .Include(s => s.StudentExam)
                    .ThenInclude(se => se.Exam)
                .Include(s => s.Group)
                    .ThenInclude(g => g.InstructorCourse)
                        .ThenInclude(ic => ic.Courses)
                .Include(s => s.Group)
                    .ThenInclude(g => g.InstructorCourse)
                        .ThenInclude(ic => ic.Instructors)
                .Where(s => s.Users.Role == "student")
                .ToList();
        }



        public Students GetById(int id)
        {
            try
            {
                var student = db.Students
                .Include(s => s.Users)
                .Include(s => s.StudentExam)
                    .ThenInclude(se => se.Exam)
                .Include(s => s.Group)
                    .ThenInclude(g => g.InstructorCourse)
                        .ThenInclude(ic => ic.Courses)
                .Where(s => s.Users.Role == "student")
                .FirstOrDefault(a => a.userID == id);

                if (student == null)
                    throw new Exception("Student with provided ID not found.");

                return student;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving instructor by ID.", ex);
            }
        }

        public Students GetByName(string name)
        {
            try
            {
                var student = db.Students
                .Include(s => s.Users)
                .Include(s => s.StudentExam)
                    .ThenInclude(se => se.Exam)
                .Include(s => s.Group)
                    .ThenInclude(g => g.InstructorCourse)
                        .ThenInclude(ic => ic.Courses)
                .Where(s => s.Users.Role == "student")
                .FirstOrDefault(a => a.Users.Name == name);

                if (student == null)
                    throw new Exception("Student with provided Name not found.");

                return student;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving instructor by name.", ex);
            }
        }

        public void Update(Students std)
        {
            db.Update(std);
            db.SaveChanges();
        }
    }
}
