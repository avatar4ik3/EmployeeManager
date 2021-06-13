using EmployeeManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class SelectChiefCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; private set; }
        public SelectChiefCommand(EmployeesViewModel employeesViewModel)
        {
            this.ViewModel = employeesViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return ViewModel.Selected.Chief != null;
        }

        public void Execute(object parameter)
        {
            ViewModel.Selected = ViewModel.Selected.Chief;
        }
    }
}
