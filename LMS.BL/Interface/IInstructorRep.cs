﻿using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IInstructorRep
    {
        IEnumerable<Instructors> GetAllData();
        Instructors GetById(int id);
        Instructors GetByName(string name);
        void Add(Instructors inst);
        void Update(Instructors inst);
        void Delete(Instructors inst);
    }
}
