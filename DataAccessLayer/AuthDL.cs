using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walk_in_api.Model;
using MySql.Data.MySqlClient;
using System.Data.Common;

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

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
             SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";


            try
            {
                
                if(_mySqlConnection.State != System.Data.ConnectionState.Open){
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * FROM walk.user WHERE username=@username and password=@password";

                using(MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@username",request.username);
                    sqlCommand.Parameters.AddWithValue("@password",request.password);
                    
                    using(DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync()){
                        if(dataReader.HasRows){
                            response.Message = "Login successful";
                        }
                        else{
                            response.IsSuccess = false;
                            response.Message = "Login failed";
                        }
                    }

                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                
            }
            finally {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "Successful";


            try
            {
                if(!request.password.Equals(request.confirmPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password and confirm password do not match";
                    return response;

                }
                if(_mySqlConnection.State != System.Data.ConnectionState.Open){
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO walk.user (username, password) values(@username, @password);";

                using(MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@username",request.username);
                    sqlCommand.Parameters.AddWithValue("@password",request.password);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();

                    if(Status <= 0){
                        response.IsSuccess = false;
                        response.Message = "Something went wrong";
                        return response;
                    }



                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                
            }
            finally {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

       
    }
}