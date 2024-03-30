using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IQuestionRep
    {
        IEnumerable<Questions> GetAllData();
        IEnumerable<Questions> GetByExamId(int examId);
        Questions GetById(int id);
        void Add(Questions ques);
        void Update(Questions ques);
        void Delete(Questions ques);
    }
}
