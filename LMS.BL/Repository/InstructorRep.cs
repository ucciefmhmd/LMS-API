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
    public class InstructorRep : IInstructorRep
    {
        private readonly LMSContext db;

        public InstructorRep(LMSContext db)
        {
            this.db = db;
        }

        public void Add(Instructors inst)
        {
            db.Instructors.Add(inst);
            db.SaveChanges();
        }

        public void Delete(Instructors inst)
        {
            db.Instructors.Remove(inst);
            db.SaveChanges();
        }

        public IEnumerable<Instructors> GetAllData()
        {
            return db.Instructors
               .Include(a => a.Users)
               .Include(a => a.InstructorCourse)
                   .ThenInclude(ic => ic.Courses) 
               .Where(a => a.Users.Role == "instructor")
               .ToList();
        }

        public Instructors GetById(int id)
        {
            try
            {
                var instructor = db.Instructors
                    .Include(a => a.Users)
                    .Include(a => a.InstructorCourse)
                        .ThenInclude(ic => ic.Courses)
                    .FirstOrDefault(a => a.userID == id && a.Users.Role == "instructor");

                if (instructor == null)
                {
                    throw new Exception("Instructor with provided ID not found.");
                }

                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving instructor by ID.", ex);
            }

        }

        public Instructors GetByName(string name)
        {
            try
            {
                var instructor = db.Instructors
                    .Include(a => a.Users)
                    .Include(a => a.InstructorCourse)
                        .ThenInclude(ic => ic.Courses)
                    .FirstOrDefault(a => a.Users.Name == name && a.Users.Role == "instructor");

                if (instructor == null)
                {
                    throw new Exception("Instructor with provided name not found.");
                }

                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving instructor by name.", ex);
            }
        }

        public void Update(Instructors inst)
        {
            db.Instructors.Update(inst);
            db.SaveChanges();
        }
    }
}
