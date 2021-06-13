using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI.UI.Controls;

using EmployeeManager.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EmployeeManager.Views
{
    public sealed partial class DepartmentsPage : Page
    {
        public DepartmentsViewModel ViewModel { get; }

        public DepartmentsPage()
        {
            ViewModel = Ioc.Default.GetService<DepartmentsViewModel>();
            InitializeComponent();
        }

        private void OnViewStateChanged(object sender, ListDetailsViewState e)
        {
            if (e == ListDetailsViewState.Both)
            {
                ViewModel.EnsureItemSelected();
            }
        }
    }
}
