using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IExamRep
    {
        IEnumerable<Exam> GetAllData();
        Exam GetById(int id);
        Exam GetByIdIncludingChooseQuestions(int id);
        Exam GetByName(string name);
        void Add(Exam exam);
        void Update(Exam exam);
        void Delete(Exam exam);
    }
}
