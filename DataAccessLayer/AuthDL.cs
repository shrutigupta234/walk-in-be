using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walk_in_api.Model;
using MySql.Data.MySqlClient;
using System.Data.Common;
using Scrypt;
using System.Diagnostics;

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
            ScryptEncoder encoder = new ScryptEncoder();
             SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";


            try
            {
                
                if(_mySqlConnection.State != System.Data.ConnectionState.Open){
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * FROM walk.user WHERE username=@username";

                using(MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@username",request.username);
                    // sqlCommand.Parameters.AddWithValue("@password",request.password);
                    int count = 0;
                    using(DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync()){
                        if(dataReader.HasRows){

                            // if(encoder.Compare(dataReader["password"].,encoder.Encode(request.password) )){

                            // }
                            // for (int i = 0; i < dataReader.FieldCount; i++){
                            //     Console.WriteLine(dataReader.GetName(i));
                            // }
                            
                            while(dataReader.Read()){
                                if(count == 0){
                                    String pwd = dataReader["password"].ToString();
                                    
                                    
                                    if(encoder.Compare(request.password, pwd)){
                                        response.Message = "Login successful";
                                    } else {
                                        response.IsSuccess = false;
                                        response.Message = "Login Failed";
                                    }
                                } else {
                                    break;
                                }
                                count++;
                            }
                            
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
            ScryptEncoder encoder = new ScryptEncoder();
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

                string SqlQuery = @"INSERT INTO walk.user (username, password, fname,lname,email,phone,portfolio_url,is_email_update,aggregate,year_of_passing,qualification,stream ,college ,college_city, applicant_type ,yoe,current_ctc,expected_ctc,is_notice_period, end_notice_date ,notice_duration,is_prev_test , prev_role_applied ) values(@username, @password,@fname,@lname,@email,@phone,@portfolio_url,@is_email_update,@aggregate,@year_of_passing,@qualification,@stream ,@college ,@college_city,@applicant_type ,@yoe,@current_ctc,@expected_ctc,@is_notice_period,@end_notice_date ,@notice_duration,@is_prev_test ,@prev_role_applied );";

                using(MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@username",request.username);
                    sqlCommand.Parameters.AddWithValue("@password",encoder.Encode(request.password));
                    sqlCommand.Parameters.AddWithValue("@fname",request.fname);
                    sqlCommand.Parameters.AddWithValue("@lname",request.lname);
                    sqlCommand.Parameters.AddWithValue("@email",request.email);
                    sqlCommand.Parameters.AddWithValue("@phone",request.phone);
                    sqlCommand.Parameters.AddWithValue("@portfolio_url",request.portfolio_url);
                    sqlCommand.Parameters.AddWithValue("@is_email_update",request.is_email_update);
                    sqlCommand.Parameters.AddWithValue("@aggregate",request.aggregate);
                    sqlCommand.Parameters.AddWithValue("@year_of_passing",request.year_of_passing);
                    sqlCommand.Parameters.AddWithValue("@qualification",request.qualification);
                    sqlCommand.Parameters.AddWithValue("@stream",request.stream);
                    sqlCommand.Parameters.AddWithValue("@college",request.college);
                    sqlCommand.Parameters.AddWithValue("@college_city",request.college_city);
                    sqlCommand.Parameters.AddWithValue("@applicant_type",request.applicant_type);
                    sqlCommand.Parameters.AddWithValue("@yoe",request.yoe);
                    sqlCommand.Parameters.AddWithValue("@current_ctc",request.current_ctc);
                    sqlCommand.Parameters.AddWithValue("@expected_ctc",request.expected_ctc);
                    sqlCommand.Parameters.AddWithValue("@is_notice_period",request.is_notice_period);
                    sqlCommand.Parameters.AddWithValue("@end_notice_date",request.end_notice_date);
                    sqlCommand.Parameters.AddWithValue("@notice_duration",request.notice_duration);
                    sqlCommand.Parameters.AddWithValue("@is_prev_test",request.is_prev_test);
                    sqlCommand.Parameters.AddWithValue("@prev_role_applied", request.prev_role_applied);
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