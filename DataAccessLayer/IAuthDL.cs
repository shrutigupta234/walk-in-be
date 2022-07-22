using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using walk_in_api.Model;
using walk_in_api.Controllers;

namespace walk_in_api.DataAccessLayer
{
    public interface IAuthDL
    {
       public Task<SignUpResponse> SignUp(SignUpRequest request);
        public Task<SignInResponse> SignIn(SignInRequest request);

        public Task<ReadAllWalkInResponse> ReadAllWalkIn();

        // public Task<W> WalkIn(SignInRequest request);
    }
}