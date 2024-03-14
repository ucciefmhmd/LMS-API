﻿using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IUserRep
    {
        IEnumerable<Users> GetAllData();
        Users GetDyId(int id);
        Users GetDyName(string name);
        void Add(Users user);
        void Update(Users user);
        void Delete(Users user);
    }
}
