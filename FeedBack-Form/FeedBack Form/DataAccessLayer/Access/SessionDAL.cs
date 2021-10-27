using Dapper;
using DataAccessLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Access
{
    public class SessionDAL
    {
        private readonly IConfiguration _configuration;

        public SessionDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void AddSession(Session session)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_CreateSession";

            DynamicParameters parameter = new();

            parameter.Add("Name", session.SessionName);
            parameter.Add("Duration", session.Duration);
            parameter.Add("Date", session.Date);
            parameter.Add("ConductorId", session.ConductorId);
            parameter.Add("SpeakerId", session.SpeakerId);

            SqlMapper.Execute(dbConnection, sp, commandType: CommandType.StoredProcedure, param: parameter);
        }

        //Get todays sesions
        public async Task<IEnumerable<Session>> GetAllSession()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllSessions";

            var listOfSessions = await Task.FromResult(dbConnection.Query<Session>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfSessions;
        }


    }
}
