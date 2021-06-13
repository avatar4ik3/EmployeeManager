using System;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using EmployeeManager.Contracts.ViewModels;
using EmployeeManager.Core.Contracts.Services;
using EmployeeManager.Core.DBAccess.TransferObjects;
using EmployeeManager.Core.Models;

namespace EmployeeManager.ViewModels
{
    public class HistoryViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IDataService<History,HistoryDB> _sampleDataService;
        private History _selected;

        public History Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ObservableCollection<History> SampleItems { get; private set; } = new ObservableCollection<History>();

        public HistoryViewModel(IDataService<History, HistoryDB> sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            SampleItems.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetListDetailsDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }
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
