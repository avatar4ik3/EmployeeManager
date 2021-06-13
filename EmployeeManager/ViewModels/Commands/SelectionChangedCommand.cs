using EmployeeManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class SelectionChangedCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; }
        public SelectionChangedCommand(EmployeesViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if(!string.IsNullOrEmpty(ViewModel.Selected.ChiefId))
            {
                ViewModel.Selected.Chief = ViewModel.SampleItems.First(el => el.Id == ViewModel.Selected.ChiefId);
            }
            //ViewModel.Selected = await new EmployeeDataService().GetDataByDepthAsync(ViewModel.Selected.Id);
        }
    }
}
