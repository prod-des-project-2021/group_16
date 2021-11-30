using ChessWeb.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Authentication.Pages
{
    public partial class SignUp
    {
        private readonly SignUpDTO _userForSignUp = new SignUpDTO();
        
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public async Task SignUpUser()
        {
            ShowRegistrationErrors = false;
            var result = await AuthenticationService.SignUp(_userForSignUp);
            if (result.IsSuccessfulyRegistered)
            {
                NavigationManager.NavigateTo("/signin");
            }
            else
            {
                _userForSignUp.Username = String.Empty;
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
        }
    }
}

