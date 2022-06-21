using System;
using System.Windows.Controls;
using WpfApp_Kurs.ViewModel;
using System.Windows;


namespace WpfApp_Kurs.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Нужно для передачи значений
        public static ListView AllReservesView;
        public static ListView AllAnimalsView;
        public static ListView AllIndividualsView;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ManageVM();
            AllReservesView = ViewAllReserves;
            AllAnimalsView = ViewAllAnimals;
            AllIndividualsView = ViewAllIndividuals;
        }
    }
}
