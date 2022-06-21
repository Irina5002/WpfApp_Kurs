using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp_Kurs.ViewModel;

namespace WpfApp_Kurs.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditAnimalWindow.xaml
    /// </summary>
    public partial class AddEditAnimalWindow : Window
    {
        public AddEditAnimalWindow()
        {
            InitializeComponent();
            DataContext = new ManageVM();
        }
    }
}
