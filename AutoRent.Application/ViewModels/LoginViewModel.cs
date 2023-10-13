using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using System;
using AutoRent.Application.Stores;

namespace AutoRent.Application.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IAccountService accountService;

        public LoginViewModel(
            IRegionManager regionManager,
            IAccountService accountService)
        {
            this.regionManager = regionManager;
            this.accountService = accountService;

            Username = string.Empty;
            UsernameErrorMessage = string.Empty;
            Password = string.Empty;
            PasswordErrorMessage = string.Empty;

            LoginCommand = new DelegateCommand(Login);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _usernameErrorMessage;
        public string UsernameErrorMessage
        {
            get { return _usernameErrorMessage; }
            set { SetProperty(ref _usernameErrorMessage, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _passwordErrorMessage;
        public string PasswordErrorMessage
        {
            get { return _passwordErrorMessage; }
            set { SetProperty(ref _passwordErrorMessage, value); }
        }

        public DelegateCommand LoginCommand { get; private set; }
        private void Login()
        {

            try
            {
                UsernameErrorMessage = string.Empty;
                PasswordErrorMessage = string.Empty;
                if (string.IsNullOrEmpty(Username))
                {
                    UsernameErrorMessage = "Username is required. Please enter username.";
                }
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordErrorMessage = "Password is required. Please enter password.";
                }
                AccountStore.User = accountService.Login(new Services.DTOs.AccountLoginDto
                {
                    Username = Username,
                    Password = Password,
                });
                regionManager.RequestNavigate("ContentRegion", "HomeView");
                Username = string.Empty;
                Password = string.Empty;
            }
            catch (ValidationException e)
            { 

            }
            catch (InvalidCredentialsException e)
            {
                UsernameErrorMessage = "There is no account with this name. Please create an account.";
            }
            catch (Exception e)
            {

            }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
