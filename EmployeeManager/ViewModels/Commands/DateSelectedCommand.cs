using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class DateSelectedCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; private set; }
        public DateSelectedCommand(EmployeesViewModel employeesViewModel)
        {
            this.ViewModel = employeesViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //ViewModel.DateSelected = true;
        }
    }
}
