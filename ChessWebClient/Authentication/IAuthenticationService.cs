using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Authentication
{
    public interface IAuthenticationService
    {
        Task<SignUpResultDTO> SignUp(SignUpDTO userForSignUp);
        Task<SignInResultDTO> SignIn(SignInDTO userForSignIn);
        Task SignOut();
    }
}
