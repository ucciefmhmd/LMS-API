using LMS.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BL.Interface
{
    public interface IChooseQuestionRep
    {
        void Add(ChooseQuestion chooseQues);
        void update(ChooseQuestion chooseQues);
        void Delete(ChooseQuestion chooseQues);
    }
}
