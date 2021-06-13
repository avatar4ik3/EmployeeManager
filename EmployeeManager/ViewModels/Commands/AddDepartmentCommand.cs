using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class AddDepartmentCommand : ICommand
    {
        private DepartmentsViewModel ViewModel;
        public AddDepartmentCommand(DepartmentsViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.SampleItems.Add(new Core.Models.Department());
            ViewModel.Selected = ViewModel.SampleItems.Last();
        }
    }
}
