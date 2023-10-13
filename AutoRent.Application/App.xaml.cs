using AutoRent.Application.Stores;
using AutoRent.Application.ViewModels;
using AutoRent.Application.Views;
using AutoRent.Data;
using AutoRent.Services;
using AutoRent.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace AutoRent.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            var window = Container.Resolve<MainWindow>();
            window.Loaded += (sender, args) =>
            {
                var manager = Container.Resolve<IRegionManager>();
                manager.RequestNavigate("ContentRegion", "LoginView");
            };
            return window;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<AppDataContext>(() => new AppDataContext(
                new DbContextOptionsBuilder<AppDataContext>()
                    .UseInMemoryDatabase(databaseName: "AutoRent_Db")
                    .Options));
            containerRegistry.Register<IAccountService, AccountService>();
            containerRegistry.Register<IAutoService, AutoService>();
            containerRegistry.Register<IRentService, RentService>();

            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<RegisterView>();
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<CarsView>();
            containerRegistry.RegisterForNavigation<CreateNewCarView>();
            containerRegistry.RegisterForNavigation<EditCarView>();
            containerRegistry.RegisterForNavigation<SearchCarsView>();
            containerRegistry.RegisterForNavigation<DetailsCarView>();
            containerRegistry.RegisterForNavigation<ActiveCarsView>();
            containerRegistry.RegisterForNavigation<CarOrdersView>();
        }
    }
}
