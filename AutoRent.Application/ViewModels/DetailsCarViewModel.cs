using AutoRent.Application.Stores;
using AutoRent.Services.DTOs;
using AutoRent.Services.Exceptions;
using AutoRent.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoRent.Application.ViewModels
{
    public class DetailsCarViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IRentService rentService;
        private IRegionNavigationJournal journal;

        public DetailsCarViewModel(
            IRegionManager regionManager,
            IRentService rentService)
        {
            this.regionManager = regionManager;
            this.rentService = rentService;

            GoBackCommand = new DelegateCommand(GoBack);
            BookingCarCommand = new DelegateCommand(BookingCar);

            ErrorMessage = string.Empty;
            RentalStartDate = DateTime.Now.AddMinutes(1);
            RentalEndDate = RentalStartDate.AddHours(1);
        }

        private int _autoId;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set { SetProperty(ref _brand, value); }
        }

        private string _class;
        public string Class
        {
            get { return _class; }
            set { SetProperty(ref _class, value); }
        }

        private decimal _costPerHour;
        public decimal CostPerHour
        {
            get { return _costPerHour; }
            set { SetProperty(ref _costPerHour, value); }
        }

        private decimal _calcualtePrice;
        public decimal CalcualtePrice
        {
            get { return _calcualtePrice; }
            set { SetProperty(ref _calcualtePrice, value); }
        }

        private DateTime _rentalStartDate;
        public DateTime RentalStartDate
        {
            get { return _rentalStartDate; }
            set 
            { 
                SetProperty(ref _rentalStartDate, value);
                CalcualtePrice = (RentalEndDate - RentalStartDate).Hours * CostPerHour;
            }
        }

        private DateTime _rentalEndDate;
        public DateTime RentalEndDate
        {
            get { return _rentalEndDate; }
            set
            {
                SetProperty(ref _rentalEndDate, value);
                CalcualtePrice = (RentalEndDate - RentalStartDate).Hours * CostPerHour;
            }
        }
        
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        public DelegateCommand BookingCarCommand { get; private set; }
        private void BookingCar()
        {
            try
            {
                ErrorMessage = string.Empty;
                rentService.Booking(new AutoRentOrderBookingDto
                {
                    RenterId = AccountStore.User.Id,
                    AutoId = _autoId,
                    RentalStartDate = RentalStartDate,
                    RentalEndDate = RentalEndDate,
                });
                regionManager.RequestNavigate("SidebarMenuContentRegion", "SearchCarsView");
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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            journal = navigationContext.NavigationService.Journal;
            var auto = navigationContext.Parameters["auto"] as AutoDto;
            if (auto != null)
            {
                _autoId = auto.Id;
                Name = auto.Name;
                Description = auto.Description;
                Brand = auto.Brand;
                Class = auto.Class;
                CostPerHour = auto.CostPerHour;
                RentalStartDate = DateTime.Now.AddMinutes(1);
                RentalEndDate = RentalStartDate.AddHours(1);
                CalcualtePrice = 0;
            }
        }

        public DelegateCommand GoBackCommand { get; set; }
        private void GoBack()
        {
            journal.GoBack();
            RentalStartDate = DateTime.Now.AddMinutes(1);
            RentalEndDate = RentalStartDate.AddHours(1);
            CalcualtePrice = 0;
        }
    }
}
