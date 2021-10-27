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
    public class FeedbackDAL
    {
        private readonly IConfiguration _configuration;

        public FeedbackDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public void AddFeedback(Feedback feedback)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_SubmitFeedback";

            DynamicParameters parameter = new();

            parameter.Add("Name", feedback.Name);
            parameter.Add("Email", feedback.Email);
            parameter.Add("Mobile", feedback.MobileNumber);
            parameter.Add("Comment", feedback.Comment);
            parameter.Add("IsInformative", feedback.IsInformative);
            parameter.Add("SpeakerRating", feedback.SpeakerRating);
            parameter.Add("OverrallRating", feedback.OverrallRating);


            SqlMapper.Execute(dbConnection, sp, commandType: CommandType.StoredProcedure, param: parameter);
        }



    }
}
