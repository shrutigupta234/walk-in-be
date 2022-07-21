using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walk_in_api.Model;
using MySql.Data.MySqlClient;

namespace walk_in_api.DataAccessLayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnectionString"]);
        }

        public Task<SignInResponse> SignIn(SignInRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            throw new NotImplementedException();
        }

       
    }
}