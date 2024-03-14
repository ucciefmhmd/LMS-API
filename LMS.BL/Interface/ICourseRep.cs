using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface ICourseRep
    {
        IEnumerable<Courses> GetAllData();
        Courses GetDyId(int id);
        Courses GetDyName(string name);
        void Add(Courses crs);
        void Update(Courses crs);
        void Delete(Courses crs);
    }
}
