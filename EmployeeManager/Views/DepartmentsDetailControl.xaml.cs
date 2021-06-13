using EmployeeManager.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace EmployeeManager.Views
{
    public sealed partial class DepartmentsDetailControl : UserControl
    {
        public Department ListDetailsMenuItem
        {
            get { return GetValue(ListDetailsMenuItemProperty) as Department; }
            set { SetValue(ListDetailsMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Department), typeof(DepartmentsDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

        public DepartmentsDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DepartmentsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
