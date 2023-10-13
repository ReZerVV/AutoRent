using AutoRent.Application.Stores;
using AutoRent.Services.DTOs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace AutoRent.Application.ViewModels
{
    public class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;

        public HomeViewModel(
            IRegionManager regionManager,
            AccountStore accountStore)
        {
            this.regionManager = regionManager;

            LogoutCommand = new DelegateCommand(Logout);
            NavigateMenuCommand = new DelegateCommand<string>(NavigateMenu);
            User = AccountStore.User;
        }

        private AccountDto _user;
        public AccountDto User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public DelegateCommand LogoutCommand { get; private set; }
        private void Logout()
        {
            AccountStore.Logout();
            regionManager.RequestNavigate("ContentRegion", "LoginView");
        }

        public DelegateCommand<string> NavigateMenuCommand { get; private set; }
        private void NavigateMenu(string navigatePath)
        {
            if (navigatePath != null)
                regionManager.RequestNavigate("SidebarMenuContentRegion", navigatePath);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            regionManager.RequestNavigate("SidebarMenuContentRegion", "SearchCarsView");
            User = AccountStore.User;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
