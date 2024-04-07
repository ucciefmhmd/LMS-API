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
    public class ChooseQuestionRep : IChooseQuestionRep
    {
        private readonly LMSContext db;

        public ChooseQuestionRep(LMSContext db)
        {
            this.db = db;
        }
        public void Add(ChooseQuestion chooseQues)
        {
            db.ChooseQuestion.Add(chooseQues);
            db.SaveChanges();
        }

        public void Delete(ChooseQuestion chooseQues)
        {
            db.ChooseQuestion.Remove(chooseQues);
            db.SaveChanges();
        }

        public void update(ChooseQuestion chooseQues)
        {
            db.ChooseQuestion.Update(chooseQues);
            db.SaveChanges();
        }
    }
}
