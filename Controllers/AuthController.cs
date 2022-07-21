using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using walk_in_api.DataAccessLayer;
using walk_in_api.Model;

namespace walk_in_api.Controllers
{
    [Route("api/[controller]/[Action]")]

    [ApiController]
    public class AuthController : Controller
    {
        public readonly IAuthDL _authDL;
        
        // constructor
        public AuthController(IAuthDL authDL){
            //assign interface props
            _authDL = authDL;

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {

            SignUpResponse response = new SignUpResponse();
            try {

            }
            catch (Exception ex){
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);


        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {

            SignInResponse response = new SignInResponse();
            try {

            }
            catch (Exception ex){
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);


        }
        
    }
}