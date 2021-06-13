using CommunityToolkit.Mvvm.ComponentModel;
using EmployeeManager.Core.Models;
using EmployeeManager.ViewModels;
using EmployeeManager.ViewModels.Commands;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Windows.Input;

namespace EmployeeManager.Views
{
    public sealed partial class EmployeesDetailControl : UserControl
    {
        //попытка привязать viewModel к этой страничке, но наверное лучше использовать навигацию чтобы переходить по ссылкам
        /*public EmployeesViewModel ViewModel
        {
            get { return GetValue(ViewModelProperty) as EmployeesViewModel; }
            set { SetValue(ViewModelProperty, value); }
        }*/

        public bool HasViewModelValue { get; set; }

        public Employee ListDetailsMenuItem
        {
            get { return GetValue(ListDetailsMenuItemProperty) as Employee; }
            set { SetValue(ListDetailsMenuItemProperty, value); }
        }

        public DateTime CurrentDate
        {
            get
            {
                if(ListDetailsMenuItem == null)
                {
                    return new DateTime();
                }
                return ListDetailsMenuItem.Birth;
            }
            set
            {
                ListDetailsMenuItem.Birth = value;
            }
        }
        public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Employee), typeof(EmployeesDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));
        //public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(EmployeesViewModel), typeof(EmployeesDetailControl), new PropertyMetadata(null, new PropertyChangedCallback(OnViewModelChanged)));

        /*private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as EmployeesDetailControl;
            var model = e.NewValue as EmployeesViewModel;
            if (model == null)
            {
                control.HasViewModelValue = false;   
            } else
            {
                control.HasViewModelValue = true;
            }
            control.ForegroundElement.ChangeView(0, 0, 1);
        }*/

        

        public EmployeesDetailControl()
        {
            
            InitializeComponent();
        }

        private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as EmployeesDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

    }
}
