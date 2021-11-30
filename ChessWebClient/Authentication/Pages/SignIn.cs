using ChessWeb.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Authentication.Pages
{
    public partial class SignIn
    {

        private SignInDTO _userToSignIn = new SignInDTO();

        [Inject]
        public IAuthenticationService AuthService { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        public bool ShowErrorMessage { get; set; }

        public string ErrorMessage { get; set; }

        public async Task SignInUser()
        {
            ShowErrorMessage = false;

            var result = await AuthService.SignIn(_userToSignIn);
            
            if (result.IsAuthSuccessful)
            {
                NavManager.NavigateTo("/");
            }
            else
            {
                _userToSignIn = new SignInDTO();
                ErrorMessage = result.ErrorMessage;
                ShowErrorMessage = true;
            }
          
        }
    }
}
