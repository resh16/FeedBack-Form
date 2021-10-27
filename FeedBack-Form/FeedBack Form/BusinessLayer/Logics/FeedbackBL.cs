using DataAccessLayer.Access;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logics
{
    public class FeedbackBL
    {

        public FeedbackDAL _feedbackdal;

        public FeedbackBL(FeedbackDAL feedbackDAL)
        {
            this._feedbackdal = feedbackDAL;
        }

        public void AddFeedback(Feedback feedback)
        {
            _feedbackdal.AddFeedback(feedback);
        }


    }
}
