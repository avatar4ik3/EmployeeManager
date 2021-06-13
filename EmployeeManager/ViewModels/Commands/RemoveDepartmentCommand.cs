using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.ViewModels.Commands
{
    class RemoveDepartmentCommand : ICommand
    {
        private DepartmentsViewModel ViewModel;
        public RemoveDepartmentCommand(DepartmentsViewModel viewModel)
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
            if (ViewModel.Selected != null)
            {
                var id = ViewModel.Selected.Id;
                ViewModel.SampleItems.Remove(ViewModel.SampleItems.First(el => el.Id == id));
                try
                {
                    ViewModel._sampleDataService.Delete(id);

                }
                catch { };
            }
            ViewModel.LoadData();
        }
        
    }
}
