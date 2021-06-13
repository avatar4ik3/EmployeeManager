using EmployeeManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class SortCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; }

        public SortCommand(EmployeesViewModel viewModel )
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
            var comparison = parameter as Comparison<Employee>; 
            ViewModel.LoadData(comparison);
        }
    }
}
