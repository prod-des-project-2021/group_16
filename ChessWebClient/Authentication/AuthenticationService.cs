using Blazored.LocalStorage;
using ChessWeb.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChessWebClient.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient,
            AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<SignInResultDTO> SignIn(SignInDTO userForSignIn)
        {

            var signInResult = await _httpClient.PostAsJsonAsync("/signin", userForSignIn);
            var signInContent = await signInResult.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<SignInResultDTO>(signInContent, _options);

            if (signInResult.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("authToken", result.Token);
                ((TokenAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            }

            return result;
        }

        public async Task SignOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((TokenAuthStateProvider)_authStateProvider).NotifyUserSignOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResultDTO> SignUp(SignUpDTO userForSignUp)
        {

            var signUpResult = await _httpClient.PostAsJsonAsync("/signup", userForSignUp);
            var signUpContent = await signUpResult.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SignUpResultDTO>(signUpContent, _options);
        }
    }
}
