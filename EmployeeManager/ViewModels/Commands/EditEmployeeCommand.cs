using EmployeeManager.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class EditEmployeeCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; private set; }
        public EditEmployeeCommand(EmployeesViewModel employeesViewModel)
        {
            this.ViewModel = employeesViewModel;
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
                var date = parameter as DateTime?;
                var history = HistoryDataService.ConverFromEmployee(ViewModel.Selected, date);
                var service = new HistoryDataService();
                //todo МНОГОПОТОЧНОСТЬ
                await service.InsertAsync(history);
                await ViewModel._sampleDataService.UpdateInfoAsync(ViewModel.Selected);
            }
        }
    }
}
