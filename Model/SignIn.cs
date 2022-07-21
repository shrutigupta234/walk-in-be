using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace walk_in_api.Model
{
    public class SignInRequest
    {
       

        [Required]
        public string? username {get; set;}

        [Required]
        public string? password {get; set;}

        


    }

    public class SignInResponse 
    {
        public bool IsSuccess {get; set;}
        public string? Message{get; set;}


    }
}