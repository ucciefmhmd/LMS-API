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
            return db.Instructors.Select(a => a);
        }

        public Instructors GetDyId(int id)
        {
            return db.Instructors.Find(id);
        }

        public Instructors GetDyName(string name)
        {
            return db.Instructors.FirstOrDefault(a => a.Name == name);
        }

        public void Update(Instructors inst)
        {
            db.Instructors.Update(inst);
            db.SaveChanges();
        }
    }
}
