using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IStudentRep
    {
        IEnumerable<Students> GetAllData();
        Students GetById(int id);
        Students GetByName(string name);
        void Add(Students std);
        void Update(Students std);
        void Delete(Students std);
    }
}
