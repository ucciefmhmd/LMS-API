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

        public StudentRep(LMSContext db)
        {
            this.db = db;
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
            return db.Students.Include(a => a.Group).Select(a => a);
        }

        public Students GetDyId(int id)
        {
            return db.Students.Find(id);
        }

        public Students GetDyName(string name)
        {
            return db.Students.FirstOrDefault(a=>a.Name == name);
        }

        public void Update(Students std)
        {
            db.Update(std);
            db.SaveChanges();
        }
    }
}
