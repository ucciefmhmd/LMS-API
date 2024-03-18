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
            return db.Courses.Find(id);
        }

        public Courses GetByName(string name)
        {
            return db.Courses.FirstOrDefault(a => a.Name == name);
        }

        public void Update(Courses crs)
        {
            db.Courses.Update(crs);
            db.SaveChanges();
        }
    }
}
