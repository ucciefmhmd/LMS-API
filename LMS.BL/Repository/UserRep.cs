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
    public class UserRep : IUserRep
    {
        private readonly LMSContext db;

        public UserRep(LMSContext db)
        {
            this.db = db;
        }

        public void Add(Users user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Delete(Users user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public IEnumerable<Users> GetAllData()
        {
            return db.Users.Select(a => a);
        }

        public Users GetById(int id)
        {
            return db.Users.Find(id);
        }

        public Users GetByName(string name)
        {
            return db.Users.FirstOrDefault(a => a.Name == name);
        }

        public void Update(Users user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}
