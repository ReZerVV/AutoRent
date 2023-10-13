using AutoRent.Services;
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
    public class ActiveCarsViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IAutoService autoService;
        private readonly IRentService rentService;
        private IRegionNavigationJournal journal;

        public ActiveCarsViewModel(
            IRegionManager regionManager,
            IAutoService autoService,
            IRentService rentService)
        {
            this.regionManager = regionManager;
            this.autoService = autoService;
            this.rentService = rentService;

            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            ReturnCarCommand = new DelegateCommand<AutoDto>(ReturnCar);
            ReloadCarsCommand = new DelegateCommand(ReloadCars);

            ErrorMessage = string.Empty;
            Message = string.Empty;
            ReloadCars();
        }

        private ObservableCollection<AutoDto> _cars;
        public ObservableCollection<AutoDto> Cars
        {
            get { return _cars; }
            set { SetProperty(ref _cars, value); }
        }
        
        public DelegateCommand ReloadCarsCommand { get; private set; }
        private void ReloadCars()
        {
            Cars = new ObservableCollection<AutoDto>(
                autoService.GetRentalAutos()
            );
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
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

        public DelegateCommand<AutoDto> ReturnCarCommand { get; private set; }
        private void ReturnCar(AutoDto autoDto)
        {
            try
            {
                ErrorMessage = string.Empty;
                Message = $"Total payable: {rentService.Return(autoDto.Id)}";
                Cars.Remove(autoDto); 
                Task.Run(() =>
                {
                    Thread.Sleep(10000);
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
