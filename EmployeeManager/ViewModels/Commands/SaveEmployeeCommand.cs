using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    public class SaveEmployeeCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; }
        public SaveEmployeeCommand(EmployeesViewModel viewModel)
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
            if (ViewModel.Selected != null)
            {
                await ViewModel._sampleDataService.InsertAsync(ViewModel.Selected);
            }
        }
    }
}
