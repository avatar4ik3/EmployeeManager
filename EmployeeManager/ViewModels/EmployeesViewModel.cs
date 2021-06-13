using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using EmployeeManager.Contracts.ViewModels;
using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess.TransferObjects;
using EmployeeManager.Core.Models;
using EmployeeManager.Core.Services;
using EmployeeManager.ViewModels.Commands;
using Microsoft.UI.Xaml.Controls;

namespace EmployeeManager.ViewModels
{
    public class EmployeesViewModel : ObservableRecipient, INavigationAware
    {
        // за это мне гореть в аду но не успеваю переделать шаблоны  winui 3
        public readonly IDataService<Employee, EmployeeDB> _sampleDataService;
        private Employee _selected;
        //Commands
        public ICommand SelectChiefCommand { get; }
        public ICommand AddEmployeeCommand { get; }
        public ICommand RemoveEmployeeCommand { get; }
        public ICommand SelectionChangedCommand { get; }
        public ICommand SaveEmployeeCommand { get; }
        public ICommand SortCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand DateSelectedCommand { get; }

        //Comparisions
        public Comparison<Employee> ByName => (el1, el2) =>
        {
            return el1.Name.CompareTo(el2.Name);
        };

        // Очень лениво да (
        public Comparison<Employee> ByPosition => (el1, el2) =>
        {
            return el1.Position.CompareTo(el2.Position);
        };

        public Comparison<Employee> BySalary => (el1, el2) =>
        {
            return el1.Salary.CompareTo(el2.Salary);
        };

        //переменная отвечающая за то выбрана ли дата для внесения в изменения описания работника
        //и за это прошу простить
        //почему текущее непонятно
        public DateTime SelectedDate { get; set; } = DateTime.Now;

        public Employee Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<Employee> SampleItems { get; private set; } = new ObservableCollection<Employee>();

        public EmployeesViewModel(IDataService<Employee, EmployeeDB> sampleDataService)
        {
            SelectChiefCommand = new SelectChiefCommand(this);
            AddEmployeeCommand = new AddEmployeeCommand(this);
            RemoveEmployeeCommand = new RemoveEmployeeCommand(this);
            SelectionChangedCommand = new SelectionChangedCommand(this);
            SaveEmployeeCommand = new SaveEmployeeCommand(this);
            SortCommand = new SortCommand(this);
            EditEmployeeCommand = new EditEmployeeCommand(this);
            DateSelectedCommand = new DateSelectedCommand(this);
            _sampleDataService = sampleDataService;

        }

        public void SelectionChanged(object sender,SelectionChangedEventArgs args)
        {

            //попытка привязаться к аргментам эвента . неудачная ((
            /*var selected = e.AddedItems[0];
            selected = await _sampleDataService.GetDataByDepthAsync((selected as Employee).Id);*/
            /*if (SampleItems.Count != 0 && Selected != null)
            {
                Selected.Chief = null;
                Selected.Department = null;
                if (!string.IsNullOrEmpty(Selected.ChiefId))
                {
                    Selected.Chief = await _sampleDataService.GetDataByDepthAsync(Selected.ChiefId, 0, 0);
                }
                if (!string.IsNullOrEmpty(Selected.DepartmentId))
                {
                    Selected.Department = await new DepartmentDataService().GetDataByDepthAsync(Selected.DepartmentId, 0, 0);
                }
                //обновление объекта иксд
                Selected = Selected;
            }*/
            if (args.AddedItems.First() != null)
            {
                Selected = args.AddedItems[0] as Employee;
                if (!string.IsNullOrEmpty(Selected.ChiefId))
                {
                    Selected.Chief = SampleItems.FirstOrDefault((el) => el.Id == Selected.ChiefId);
                }
            }
        }
        public void SetSelectedToChief()
        {
            Selected = Selected.Chief;
        }

        public async void LoadData(Comparison<Employee> comparison = null)
        {
            SampleItems.Clear();
            IEnumerable<Employee> data;
            if(comparison != null)
            {
                data = await _sampleDataService.GetListDetailsDataAsync(comparison);
            }
            else
            {
                data = await _sampleDataService.GetListDetailsDataAsync();
            }
            
            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            Selected = SampleItems.First();
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
