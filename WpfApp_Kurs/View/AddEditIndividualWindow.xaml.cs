using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp_Kurs.ViewModel;

namespace WpfApp_Kurs.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditIndividualWindow.xaml
    /// </summary>
    public partial class AddEditIndividualWindow : Window
    {
        public AddEditIndividualWindow()
        {
            InitializeComponent();
            DataContext = new ManageVM();
        }
    }
}
