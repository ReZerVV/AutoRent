using AutoRent.Services.DTOs;
using AutoRent.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace AutoRent.Application.ViewModels
{
    public class CreateNewCarViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IAutoService autoService;
        private IRegionNavigationJournal journal;

        public CreateNewCarViewModel(
            IRegionManager regionManager,
            IAutoService autoService)
        {
            this.regionManager = regionManager;
            this.autoService = autoService;

            CreateNewCarCommand = new DelegateCommand(CreateNewCar);

            NameErrorMessage = string.Empty;
            DescriptionErrorMessage = string.Empty;
            BrandErrorMessage = string.Empty;
            ClassErrorMessage = string.Empty;
            CostPerHourErrorMessage = string.Empty;

            GoBackCommand = new DelegateCommand(GoBack);
        }

        private string _name;
        public string Name 
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _nameErrorMessage;
        public string NameErrorMessage
        {
            get { return _nameErrorMessage; }
            set { SetProperty(ref _nameErrorMessage, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        
        private string _descriptionErrorMessage;
        public string DescriptionErrorMessage
        {
            get { return _descriptionErrorMessage; }
            set { SetProperty(ref _descriptionErrorMessage, value); }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set { SetProperty(ref _brand, value); }
        }

        private string _brandErrorMessage;
        public string BrandErrorMessage
        {
            get { return _brandErrorMessage; }
            set { SetProperty(ref _brandErrorMessage, value); }
        }

        private string _class;
        public string Class
        {
            get { return _class; }
            set { SetProperty(ref _class, value); }
        }

        private string _classErrorMessage;
        public string ClassErrorMessage
        {
            get { return _classErrorMessage; }
            set { SetProperty(ref _classErrorMessage, value); }
        }

        private decimal _costPerHour;
        public decimal CostPerHour
        {
            get { return _costPerHour; }
            set { SetProperty(ref _costPerHour, value); }
        }

        private string _costPerHourErrorMessage;
        public string CostPerHourErrorMessage
        {
            get { return _costPerHourErrorMessage; }
            set { SetProperty(ref _costPerHourErrorMessage, value); }
        }

        public DelegateCommand CreateNewCarCommand { get; private set; }
        private void CreateNewCar()
        {
            try
            {
                NameErrorMessage = string.Empty;
                DescriptionErrorMessage = string.Empty;
                BrandErrorMessage = string.Empty;
                ClassErrorMessage = string.Empty;
                CostPerHourErrorMessage = string.Empty;

                if (string.IsNullOrEmpty(Name)) 
                {
                    NameErrorMessage = "Name is required. Please enter name.";
                }
                if (string.IsNullOrEmpty(Description))
                {
                    DescriptionErrorMessage = "Description is required. Please enter description.";
                }
                if (string.IsNullOrEmpty(Brand))
                {
                    BrandErrorMessage = "Brand is required. Please enter brand.";
                }
                if (string.IsNullOrEmpty(Class))
                {
                    ClassErrorMessage = "Class is required. Please enter class.";
                }
                if (CostPerHour == 0)
                {
                    CostPerHourErrorMessage = "Cost is required. Please enter cost.";
                }
                if (string.IsNullOrEmpty(NameErrorMessage) &&
                    string.IsNullOrEmpty(DescriptionErrorMessage) &&
                    string.IsNullOrEmpty(BrandErrorMessage) &&
                    string.IsNullOrEmpty(ClassErrorMessage) &&
                    string.IsNullOrEmpty(CostPerHourErrorMessage))
                {
                    autoService.Create(new AutoCreateDto 
                    {
                        Name = Name,
                        Description = Description,
                        Brand = Brand,
                        Class = Class,
                        CostPerHour = CostPerHour,
                    });
                    Name = string.Empty;
                    Description = string.Empty;
                    Brand = string.Empty;
                    Class = string.Empty;
                    CostPerHour = 0;
                    regionManager.RequestNavigate("SidebarMenuContentRegion", "CarsView");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public DelegateCommand GoBackCommand { get; set; }
        private void GoBack()
        {
            journal.GoBack();
        }
    }
}
