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



    }

    public class SignUpResponse 
    {
        public bool IsSuccess {get; set;}
        public string? Message{get; set;}


    }
}