using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp_Kurs.ViewModel;

namespace WpfApp_Kurs.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditReserveWindow.xaml
    /// </summary>
    public partial class AddEditReserveWindow : Window
    {
        public AddEditReserveWindow()
        {
            InitializeComponent();
            DataContext = new ManageVM();
        }
    }
}
