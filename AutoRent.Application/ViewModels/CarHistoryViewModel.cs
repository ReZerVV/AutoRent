using AutoRent.Services.DTOs;
using AutoRent.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace AutoRent.Application.ViewModels
{
    public class CarHistoryViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IRentService rentService;
        private IRegionNavigationJournal journal;

        public CarHistoryViewModel(
            IRegionManager regionManager,
            IRentService rentService)
        {
            this.regionManager = regionManager;
            this.rentService = rentService;

            GoBackCommand = new DelegateCommand(GoBack);
            ReloadOrdersCommand = new DelegateCommand(ReloadOrders);

            ErrorMessage = string.Empty;
        }

        private int _autoId;

        private ObservableCollection<AutoRentOrderDto> _orders;
        public ObservableCollection<AutoRentOrderDto> Orders
        {
            get { return _orders; }
            set { SetProperty(ref _orders, value); }
        }

        public DelegateCommand ReloadOrdersCommand { get; private set; }
        private void ReloadOrders()
        {
            Orders = new ObservableCollection<AutoRentOrderDto>(
                rentService.GetOrdersByAutoId(_autoId));
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
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
                ReloadOrders();
            }
        }

        public DelegateCommand GoBackCommand { get; set; }
        private void GoBack()
        {
            journal.GoBack();
        }
    }
}
