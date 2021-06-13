using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class EditDepartmentCommand : ICommand
    {
        private DepartmentsViewModel ViewModel;
        public EditDepartmentCommand(DepartmentsViewModel viewModel)
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
            if(ViewModel.Selected != null)
            {
                ViewModel._sampleDataService.UpdateInfoAsync(ViewModel.Selected);
            }
            ViewModel.LoadData();
        }
    }
}
