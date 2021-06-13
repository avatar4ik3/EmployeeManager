using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI.UI.Controls;

using EmployeeManager.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace EmployeeManager.Views
{
    public sealed partial class EmployeesPage : Page
    {
        public EmployeesViewModel ViewModel { get; }

        public EmployeesPage()
        {
            ViewModel = Ioc.Default.GetService<EmployeesViewModel>();
            InitializeComponent();
        }

        private void OnViewStateChanged(object sender, ListDetailsViewState e)
        {
            if (e == ListDetailsViewState.Both)
            {
                ViewModel.EnsureItemSelected();
            }
        }

        private void HyperlinkButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.Selected = ViewModel.Selected.Chief;
        }
    }
}
