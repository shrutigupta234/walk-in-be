using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace walk_in_api.Model
{
    public class SignUpRequest
    {
       

        [Required]
        public string? username {get; set;}

        [Required]
        public string? password {get; set;}

        [Required]
        public string? confirmPassword {get; set;}

        [Required]
        public string? fname {get; set;}

        [Required]
        public string? lname {get; set;}

        [Required]
        [EmailAddress]
        public string? email {get; set;}

        [Required]
        public string? phone {get; set;}

        public string? portfolio_url {get; set;}

        [Required]
        public bool? is_email_update {get; set;}

        [Required]
        public decimal? aggregate {get; set;}

        [Required]
        public int? year_of_passing {get; set;}

        [Required]
        public string? qualification {get; set;}

        [Required]
        public string? stream {get; set;}

        [Required]
        public string? college {get; set;}

        [Required]
        public string? college_city {get; set;}

        [Required]
        public string? applicant_type {get; set;}

        [Required]
        public int? yoe {get; set;}

        [Required]
        public int? current_ctc {get; set;}

        [Required]
        public int? expected_ctc {get; set;}

        [Required]
        public bool? is_notice_period {get; set;}

        [Required]
        public DateTime? end_notice_date {get; set;}

        [Required]
        public int? notice_duration {get; set;}

        [Required]
        public bool? is_prev_test {get; set;}

        public string? prev_role_applied {get; set;}




    }

    public class SignUpResponse 
    {
        public bool IsSuccess {get; set;}
        public string? Message{get; set;}


    }
}




       
        //  @fname,@lname,@email,@phone,@portfolio,@is_email_update,@aggregate,@year_of_passing,@qualification,@stream ,@college ,@college_city,@ applicant_type ,@yoe,@current_ctc,@expected_ctc,@is_notice_period,@ end_notice_date ,@notice_duration,@is_prev_test ,@prev_role_applied 