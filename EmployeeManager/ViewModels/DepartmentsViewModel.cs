using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using EmployeeManager.Contracts.ViewModels;
using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess.TransferObjects;
using EmployeeManager.Core.Models;
using EmployeeManager.ViewModels.Commands;

namespace EmployeeManager.ViewModels
{
    public class DepartmentsViewModel : ObservableRecipient, INavigationAware
    {
        public readonly IDataService<Department,DepartmentDB> _sampleDataService;
        private Department _selected;

        //commands
        public ICommand SaveDepartmentCommand { get; }
        public ICommand AddDepartmentCommand { get; }
        public ICommand RemoveDepartmentCommand { get; }
        public ICommand EditDepartmentCommand { get; }


        public Department Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<Department> SampleItems { get; private set; } = new ObservableCollection<Department>();

        public DepartmentsViewModel(IDataService<Department, DepartmentDB> sampleDataService)
        {
            SaveDepartmentCommand = new SaveDepartmentCommand(this);
            AddDepartmentCommand = new AddDepartmentCommand(this);
            RemoveDepartmentCommand = new RemoveDepartmentCommand(this);
            EditDepartmentCommand = new EditDepartmentCommand(this);

            _sampleDataService = sampleDataService;
        }

        public async void LoadData(Comparison<Department> comparision = null)
        {
            SampleItems.Clear();
            IEnumerable<Department> data;
            if(comparision != null)
            {
                data = await _sampleDataService.GetListDetailsDataAsync(comparision);

            }
            else
            {
                data = await _sampleDataService.GetListDetailsDataAsync();

            }
            foreach (var item in data)
            {
                SampleItems.Add(item);
            }
        }

        public void OnNavigatedTo(object parameter)
        {
            LoadData();
        }

        public void OnNavigatedFrom()
        {
        }

        public void EnsureItemSelected()
        {
            /*if (Selected == null)
            {
                Selected = SampleItems.First();
            }*/
        }
    }
}
