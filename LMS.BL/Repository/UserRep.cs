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
            try
            {
                var user = db.Users.FirstOrDefault(a => a.Id == id);

                if (user is null)
                    throw new Exception("User with provided ID not found.");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving user by ID.", ex);
            }
        }

        public Users GetByName(string name)
        {
            try
            {
                var user = db.Users.FirstOrDefault(a => a.Name == name);

                if (user is null)
                    throw new Exception("User with provided Name not found.");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving user by ID.", ex);
            }
        }

        public void Update(Users user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}
