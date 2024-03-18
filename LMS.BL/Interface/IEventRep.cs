using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IEventRep
    {
        IEnumerable<Events> GetAllData();
        Events GetById(int id);
        void Add(Events eve);
        void Update(Events eve);
        void Delete(Events eve);
    }
}
