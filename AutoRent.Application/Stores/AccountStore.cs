using AutoRent.Services.DTOs;

namespace AutoRent.Application.Stores
{
    public class AccountStore
    {
        public static AccountDto User { get; set; }

        public static void Logout()
        {
            User = null;
        }
    }
}
