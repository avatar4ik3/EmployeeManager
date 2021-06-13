using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class RemoveEmployeeCommand : ICommand
    {
        public EmployeesViewModel ViewModel { get; private set; }
        public RemoveEmployeeCommand(EmployeesViewModel employeesViewModel)
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
            string id = parameter as string;
            try
            {
                // ИЗВИНИТЕ!!!!!!!!!!
                ViewModel._sampleDataService.Delete(id);
            }
            catch { }
            ViewModel.SampleItems.Remove(ViewModel.SampleItems.First(el => el.Id == id));
            ViewModel.Selected = ViewModel.SampleItems.First();
        }
    }
}
