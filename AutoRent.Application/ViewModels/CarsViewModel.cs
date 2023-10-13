using AutoRent.Application.Stores;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace AutoRent.Application.ViewModels
{
    public class CarsViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IAutoService autoService;
        private readonly IRentService rentService;
        private IRegionNavigationJournal journal;

        public CarsViewModel(
            IRegionManager regionManager,
            IAutoService autoService,
            IRentService rentService)
        {
            this.regionManager = regionManager;
            this.autoService = autoService;
            this.rentService = rentService;
            NavigateMenuCommand = new DelegateCommand<string>(NavigateMenu);
            DeleteCarCommand = new DelegateCommand<AutoDto>(DeleteCar);
            ReloadCarsCommand = new DelegateCommand(ReloadCars);
            EditCarNavigateMenuCommand = new DelegateCommand<AutoDto>(EditCarNavigateMenu);
            SetLocationCarCommand = new DelegateCommand<AutoDto>(SetLocationCar);
            RentCarCommand = new DelegateCommand<AutoDto>(RentCar);
            CarOrdersNavigateMenuCommand = new DelegateCommand<AutoDto>(CarOrdersNavigateMenu);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);

            ReloadCars();
            IsAdministrator = AccountStore.User.Role == Domain.AccountRole.Administrator;
            ErrorMessage = string.Empty;
            Message = string.Empty;
        }

        private bool _isAdministrator;
        public bool IsAdministrator
        {
            get { return _isAdministrator; }
            set { SetProperty(ref _isAdministrator, value); }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        public DelegateCommand<AutoDto> SetLocationCarCommand { get; private set; }
        private void SetLocationCar(AutoDto autoDto)
        {
            try
            {
                ErrorMessage = string.Empty;
                autoService.SetLocation(new AutoSetLocationDto
                {
                    Id = autoDto.Id,
                    Location = Location,
                });
                ReloadCars();
                Location = string.Empty;
            }
            catch (ValidationException e)
            {
                ErrorMessage = e.Message;
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    ErrorMessage = string.Empty;
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private ObservableCollection<AutoDto> _cars;
        public ObservableCollection<AutoDto> Cars 
        {
            get { return _cars; }
            set { SetProperty(ref _cars, value); }
        }

        public DelegateCommand<string> NavigateMenuCommand { get; private set; }
        private void NavigateMenu(string navigatePath)
        {
            if (navigatePath != null)
                regionManager.RequestNavigate("SidebarMenuContentRegion", navigatePath);
        }

        public DelegateCommand<AutoDto> DeleteCarCommand { get; private set; }
        private void DeleteCar(AutoDto autoDto)
        {
            if (autoDto == null)
                return;
            autoService.Delete(autoDto.Id);
            Cars.Remove(autoDto);
        }

        public DelegateCommand ReloadCarsCommand { get; private set; }
        private void ReloadCars()
        {
            Cars = new ObservableCollection<AutoDto>(
                autoService.GetAllAutos()
            );
        }

        public DelegateCommand<AutoDto> CarOrdersNavigateMenuCommand { get; private set; }
        public void CarOrdersNavigateMenu(AutoDto autoDto)
        {
            if (IsAdministrator)
                return;
            if (autoDto == null)
                return;
            var parameters = new NavigationParameters();
            parameters.Add("auto", autoDto);
            regionManager.RequestNavigate("SidebarMenuContentRegion", "CarOrdersView", parameters);
        }

        public DelegateCommand<AutoDto> EditCarNavigateMenuCommand { get; private set; }
        public void EditCarNavigateMenu(AutoDto autoDto)
        {
            if (autoDto == null)
                return;
            var parameters = new NavigationParameters();
            parameters.Add("auto", autoDto);
            regionManager.RequestNavigate("SidebarMenuContentRegion", "EditCarView", parameters);
        }

        public DelegateCommand<AutoDto> RentCarCommand { get; private set; }
        public void RentCar(AutoDto autoDto)
        {
            try
            {
                ErrorMessage = string.Empty;
                rentService.Rent(autoDto.Id);
                Message = "The car is rented. The machine is in the active list.";
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    Message = string.Empty;
                });
            }
            catch (ValidationException e)
            {
                ErrorMessage = e.Message;
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    ErrorMessage = string.Empty;
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            journal = navigationContext.NavigationService.Journal;
            IsAdministrator = AccountStore.User.Role == Domain.AccountRole.Administrator;
            ReloadCars();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public DelegateCommand GoForwardCommand { get; set; }
        private void GoForward()
        {
            journal.GoForward();
        }
        private bool CanGoForward()
        {
            return journal != null && journal.CanGoForward;
        }
    }
}
