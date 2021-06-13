using EmployeeManager.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace EmployeeManager.Views
{
    public sealed partial class HistoryDetailControl : UserControl
    {
        public History ListDetailsMenuItem
        {
            get { return GetValue(ListDetailsMenuItemProperty) as History; }
            set { SetValue(ListDetailsMenuItemProperty, value); }
        }

        public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(History), typeof(HistoryDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

        public HistoryDetailControl()
        {
            InitializeComponent();
        }

        private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as HistoryDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
