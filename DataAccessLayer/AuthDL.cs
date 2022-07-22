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

        public async Task<ReadAllWalkInResponse> ReadAllWalkIn()
        {
            ReadAllWalkInResponse response = new ReadAllWalkInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if(_mySqlConnection.State != System.Data.ConnectionState.Open){
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT walk_in.start_date, walk_in.end_date, walk_in.expires_by, walk_in.title AS walk_in_title,
       walk.job_roles.title AS job_role_title, 
       time_slots.time_start, time_slots.time_end,
       location.city   
       FROM walk_in
INNER JOIN walk_in_has_job_roles
  ON walk_in.id = walk_in_has_job_roles.walk_in_id
INNER JOIN job_roles
  ON job_roles.id = walk_in_has_job_roles.job_role_id
INNER JOIN walk_in_has_time_slots
  ON walk_in.id = walk_in_has_time_slots.walk_in_id
INNER JOIN time_slots
  ON time_slots.id = walk_in_has_time_slots.time_slots_id
INNER JOIN location
  ON  walk_in.location_id= location.id ;";

                using(MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    
                    int count = 0;
                    using(DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync()){
                        if(dataReader.HasRows){

                             response.readAllWalkIns = new List<GetReadAllWalkIn>();
                            
                            while(await dataReader.ReadAsync()){

                                GetReadAllWalkIn getdata = new GetReadAllWalkIn();
                                getdata.startDate = dataReader["start_date"] != DBNull.Value ? Convert.ToString(dataReader["start_date"]) : Convert.ToString("null");
                                getdata.endDate = dataReader["end_date"] != DBNull.Value ? Convert.ToString(dataReader["end_date"]) : Convert.ToString("null");
                                getdata.expiresBy = dataReader["expires_by"] != DBNull.Value ? Convert.ToString(dataReader["expires_by"]) : Convert.ToString("null");
                                getdata.timeStart = dataReader["time_start"] != DBNull.Value ? Convert.ToString(dataReader["time_start"].ToString()) : Convert.ToString("00:00:00");
                                getdata.timeEnd = dataReader["time_end"] != DBNull.Value ? Convert.ToString(dataReader["time_end"].ToString()) : Convert.ToString("00:00:00");
                                getdata.city = dataReader["city"] != DBNull.Value ? Convert.ToString(dataReader["city"]) : "null";
                                getdata.jobRoleTitle = dataReader["job_role_title"] != DBNull.Value ? Convert.ToString(dataReader["job_role_title"]) : "null";
                                getdata.walkInTitle = dataReader["walk_in_title"] != DBNull.Value ? Convert.ToString(dataReader["walk_in_title"]) : "null";
                                
                               response.readAllWalkIns.Add(getdata);
                            }
                            
                        }
                        else{
                            response.IsSuccess = true;
                            response.Message = "Record not found";
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

        public async Task<ReadWalkInByIdResponse> ReadWalkInById(ReadWalkInByIdRequest request)
        {
             ReadWalkInByIdResponse response = new ReadWalkInByIdResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if(_mySqlConnection.State != System.Data.ConnectionState.Open){
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT walk_in.start_date, walk_in.end_date,walk_in.expires_by, walk_in.title AS walk_in_title,
       job_roles.title AS job_role_title, job_roles.description, job_roles.compensation, job_roles.requirements,
       time_slots.time_start, time_slots.time_end,
   
       location.city, location.line1, location.line2, location.line3, location.postal_code   
       FROM walk_in
       
INNER JOIN walk_in_has_job_roles
  ON walk_in.id = walk_in_has_job_roles.walk_in_id
INNER JOIN job_roles
  ON job_roles.id = walk_in_has_job_roles.job_role_id
INNER JOIN walk_in_has_time_slots
  ON walk_in.id = walk_in_has_time_slots.walk_in_id
INNER JOIN time_slots
  ON time_slots.id = walk_in_has_time_slots.time_slots_id
INNER JOIN location
  ON  walk_in.location_id= location.id
INNER JOIN pre_requisites_and_application_process
  ON  walk_in.pre_requisites_and_application_process_id= pre_requisites_and_application_process.id 
WHERE walk_in.id= @id";

                using(MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                     sqlCommand.Parameters.AddWithValue("@id",request.WalkInID);
                   
                    int count = 0;
                    using(DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync()){
                        if(dataReader.HasRows){

                             response.readWalkInByIds = new List<GetReadWalkInById>();
                            
                            while(await dataReader.ReadAsync()){

                                GetReadWalkInById getdata = new GetReadWalkInById();
                                getdata.startDate = dataReader["start_date"] != DBNull.Value ? Convert.ToString(dataReader["start_date"]) : Convert.ToString("null");
                                getdata.endDate = dataReader["end_date"] != DBNull.Value ? Convert.ToString(dataReader["end_date"]) : Convert.ToString("null");
                                getdata.expiresBy = dataReader["expires_by"] != DBNull.Value ? Convert.ToString(dataReader["expires_by"]) : Convert.ToString("null");
                                getdata.timeStart = dataReader["time_start"] != DBNull.Value ? Convert.ToString(dataReader["time_start"].ToString()) : Convert.ToString("00:00:00");
                                getdata.timeEnd = dataReader["time_end"] != DBNull.Value ? Convert.ToString(dataReader["time_end"].ToString()) : Convert.ToString("00:00:00");
                                getdata.city = dataReader["city"] != DBNull.Value ? Convert.ToString(dataReader["city"]) : "null";
                                getdata.jobRoleTitle = dataReader["job_role_title"] != DBNull.Value ? Convert.ToString(dataReader["job_role_title"]) : "null";
                                getdata.walkInTitle = dataReader["walk_in_title"] != DBNull.Value ? Convert.ToString(dataReader["walk_in_title"]) : "null";
                                getdata.description = dataReader["description"] != DBNull.Value ? Convert.ToString(dataReader["description"]) : "null";
                                getdata.requirements = dataReader["requirements"] != DBNull.Value ? Convert.ToString(dataReader["requirements"]) : "null";
                                getdata.compensation = dataReader["compensation"] != DBNull.Value ? Convert.ToInt32(dataReader["compensation"]) : 0;
                                
                               response.readWalkInByIds.Add(getdata);
                            }
                            
                        }
                        else{
                            response.IsSuccess = true;
                            response.Message = "Record not found";
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