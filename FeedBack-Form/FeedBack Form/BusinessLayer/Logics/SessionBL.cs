using DataAccessLayer.Access;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logics
{
    public class SessionBL
    {

        public SessionDAL _sessiondal;

        public SessionBL(SessionDAL sessionDAL)
        {
            this._sessiondal = sessionDAL;
        }

        public void AddSession(Session session)
        {
            _sessiondal.AddSession(session);
        }

        public async Task<IEnumerable<Session>> GetAllSessions()
        {
            return await _sessiondal.GetAllSession();
        }

        public void AddSession(DataAccessLayer.ViewModel.Session session)
        {
            throw new NotImplementedException();
        }
    }
}
