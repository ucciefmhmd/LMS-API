using LMS.BL.Interface;
using LMS.DAL.Database;
using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Repository
{
    public class CourseRep : ICourseRep
    {
        private readonly LMSContext db;

        public CourseRep(LMSContext db)
        {
            this.db = db;
        }

        public void Add(Courses crs)
        {
            db.Courses.Add(crs);
            db.SaveChanges();
        }

        public void Delete(Courses crs)
        {
            db.Courses.Remove(crs);
            db.SaveChanges();
        }

        public IEnumerable<Courses> GetAllData()
        {
            return db.Courses.Select(a => a);
        }

        public Courses GetById(int id)
        {
            try
            {
                var course = db.Courses.Find(id);

                if (course == null)
                    throw new Exception("Course with provided ID not found.");

                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving course by ID.", ex);
            }
        }

        public Courses GetByName(string name)
        {
            try
            {
                var course = db.Courses.FirstOrDefault(a => a.Name == name);

                if (course == null)
                    throw new Exception("Course with provided Name not found.");

                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving course by Name.", ex);
            }
        }

        public void Update(Courses crs)
        {
            db.Courses.Update(crs);
            db.SaveChanges();
        }
    }
}
