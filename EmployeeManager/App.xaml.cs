using CommunityToolkit.Mvvm.DependencyInjection;

using EmployeeManager.Activation;
using EmployeeManager.Contracts.Services;
using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess.TransferObjects;
using EmployeeManager.Core.Models;
using EmployeeManager.Core.Services;
using EmployeeManager.Helpers;
using EmployeeManager.Services;
using EmployeeManager.ViewModels;
using EmployeeManager.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

// To learn more about WinUI3, see: https://docs.microsoft.com/windows/apps/winui/winui3/.
namespace EmployeeManager
{
    public partial class App : Application
    {
        public static Window MainWindow { get; set; } = new Window() { Title = "AppDisplayName".GetLocalized() };

        public App()
        {
            InitializeComponent();
            UnhandledException += App_UnhandledException;
            Ioc.Default.ConfigureServices(ConfigureServices());
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.unhandledexceptioneventargs
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            var activationService = Ioc.Default.GetService<IActivationService>();
            await activationService.ActivateAsync(args);
        }

        private System.IServiceProvider ConfigureServices()
        {
            // TODO WTS: Register your services, viewmodels and pages here
            var services = new ServiceCollection();

            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IDataService<Employee, EmployeeDB>, EmployeeDataService>();
            services.AddSingleton<IDataService<Department, DepartmentDB>, DepartmentDataService>();
            services.AddSingleton<IDataService<History, HistoryDB>, HistoryDataService>();

            // Views and ViewModels
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();


            services.AddTransient<EmployeesViewModel>();
            services.AddTransient<EmployeesPage>();
            services.AddTransient<DepartmentsViewModel>();
            services.AddTransient<DepartmentsPage>();
            services.AddTransient<HistoryViewModel>();
            services.AddTransient<HistoryPage>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            return services.BuildServiceProvider();
        }
    }
}
