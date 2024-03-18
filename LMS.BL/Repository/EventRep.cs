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
    public class EventRep : IEventRep
    {
        private readonly LMSContext db;

        public EventRep(LMSContext db)
        {
            this.db = db;
        }

        public void Add(Events eve)
        {
            db.Events.Add(eve);
            db.SaveChanges();
        }

        public void Delete(Events eve)
        {
            db.Events.Remove(eve);
            db.SaveChanges();
        }

        public IEnumerable<Events> GetAllData()
        {
            return db.Events.Select(a => a);
        }

        public Events GetById(int id)
        {
            return db.Events.Find(id);
        }

        public void Update(Events eve)
        {
            db.Events.Update(eve);
            db.SaveChanges();
        }
    }
}
