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
            return db.Events.Include(a => a.EventsCourses).ThenInclude(a=>a.Courses).ToList();
        }

        public Events GetById(int id)
        {
            try
            {
                var eventData = db.Events.Include(a => a.EventsCourses).ThenInclude(a => a.Courses).FirstOrDefault(a => a.Id == id);

                if (eventData == null)
                    throw new Exception("Event with provided ID not found.");

                return eventData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving event by ID.", ex);
            }
        }

        public void Update(Events eve)
        {
            db.Events.Update(eve);
            db.SaveChanges();
        }
    }
}
