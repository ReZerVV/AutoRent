using AutoRent.Application.Stores;
using AutoRent.Domain;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoRent.Application.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IAccountService accountService;

        public RegisterViewModel(
            IRegionManager regionManager,
            IAccountService accountService)
        {
            this.regionManager = regionManager;
            this.accountService = accountService;

            Username = string.Empty;
            UsernameErrorMessage = string.Empty;
            Password = string.Empty;
            PasswordErrorMessage = string.Empty;
            ConfirmPassword = string.Empty;

            RegisterCommand = new DelegateCommand(Register);
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        public IEnumerable<string> RolesValues
        {
            get { return Enum.GetNames(typeof(AccountRole)); }
        }
        private string _role;
        public string Role
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }

        public DelegateCommand RegisterCommand { get; private set; }
        private void Register()
        {

            try
            {
                UsernameErrorMessage = string.Empty;
                PasswordErrorMessage = string.Empty;
                if (string.IsNullOrEmpty(Username))
                {
                    UsernameErrorMessage = "Username is required. Please enter username.";
                }
                if (Username.Length <= 3 ||
                    !Regex.IsMatch(Username, "^[a-zA-Z0-9\\s]*$"))
                {
                    UsernameErrorMessage = "Username invalid format. Please re-enter username.";
                }
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordErrorMessage = "Password is required. Please enter password.";
                }
                if (Password.Length < 8 ||
                    !Regex.IsMatch(Password, "^[a-zA-Z0-9\\s]*$"))
                {
                    PasswordErrorMessage = "Password invalid format. Please re-enter password.";
                }
                if (!string.Equals(Password, ConfirmPassword))
                {
                    PasswordErrorMessage = "Password mismatch. Please re-enter password.";
                }
                if (string.IsNullOrEmpty(Role))
                {
                    return;
                }
                if (string.IsNullOrEmpty(UsernameErrorMessage) &&
                    string.IsNullOrEmpty(PasswordErrorMessage))
                {
                    AccountStore.User = accountService.Register(new AccountRegisterDto
                    {
                        Username = Username,
                        Password = Password,
                        Role = Enum.Parse<AccountRole>(Role),
                    });
                    regionManager.RequestNavigate("ContentRegion", "HomeView");
                    Username = string.Empty;
                    Password = string.Empty;
                    Role = string.Empty;
                }
            }
            catch (ValidationException e)
            {

            }
            catch (InvalidCredentialsException e)
            {
                UsernameErrorMessage = "A user with the same name already exists. Please re-enter username.";
            }
            catch (Exception e)
            {
                throw e;
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
