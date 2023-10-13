using AutoRent.Services.DTOs;
using AutoRent.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoRent.Application.ViewModels
{
    public class SearchCarsViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IAutoService autoService;
        private IRegionNavigationJournal journal;

        public SearchCarsViewModel(
            IRegionManager regionManager,
            IAutoService autoService)
        {
            this.regionManager = regionManager;
            this.autoService = autoService;

            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            SearchCarsCommand = new DelegateCommand(SearchCars);
            ClearFilterCommand = new DelegateCommand(ClearFilter);
            DetailsCarNavigateMenuCommand = new DelegateCommand<AutoDto>(DetailsCarNavigateMenu);

            BrandsValues = autoService.GetAllAutoBrands();
            ClassesValues = autoService.GetAllAutoClasses();
            LoadCars();
        }

        private ObservableCollection<AutoDto> _cars;
        public ObservableCollection<AutoDto> Cars
        {
            get { return _cars; }
            set { SetProperty(ref _cars, value); }
        }
        
        private void LoadCars()
        {
            Cars = new ObservableCollection<AutoDto>(
                autoService.GetAllAutos()
            );
        }

        private string _search;
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        public IEnumerable<string> BrandsValues { get; private set; }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set { SetProperty(ref _brand, value); }
        }

        public IEnumerable<string> ClassesValues { get; private set; }

        private string _class;
        public string Class
        {
            get { return _class; }
            set { SetProperty(ref _class, value); }
        }

        private decimal _priceFrom;
        public decimal PriceFrom
        {
            get { return _priceFrom; }
            set { SetProperty(ref _priceFrom, value); }
        }

        private decimal _priceTo;
        public decimal PriceTo
        {
            get { return _priceTo; }
            set { SetProperty(ref _priceTo, value); }
        }

        public DelegateCommand<AutoDto> DetailsCarNavigateMenuCommand { get; private set; }
        private void DetailsCarNavigateMenu(AutoDto autoDto)
        {
            if (autoDto == null)
                return;
            var parameters = new NavigationParameters();
            parameters.Add("auto", autoDto);
            regionManager.RequestNavigate("SidebarMenuContentRegion", "DetailsCarView", parameters);
        }

        public DelegateCommand SearchCarsCommand { get; private set; }
        private void SearchCars()
        {
            try
            {
                Cars = new ObservableCollection<AutoDto>(
                    autoService.Search(new AutoSearchDto
                    {
                        KeyWords = Search,
                        Location = Location,
                        Class = Class,
                        Brand = Brand,
                        MinCost = PriceFrom,
                        MaxCost = PriceTo,
                    })
                );
            }
            catch 
            {

            }
        }

        public DelegateCommand ClearFilterCommand { get; private set; }
        private void ClearFilter()
        {
            Search = string.Empty;
            Location = string.Empty;
            Brand = string.Empty;
            Class = string.Empty;
            PriceFrom = 0;
            PriceTo = 0;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            journal = navigationContext.NavigationService.Journal;
            GoForwardCommand.RaiseCanExecuteChanged();
            BrandsValues = autoService.GetAllAutoBrands();
            ClassesValues = autoService.GetAllAutoClasses();
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
