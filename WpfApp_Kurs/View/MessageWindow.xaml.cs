using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp_Kurs.ViewModel;


namespace WpfApp_Kurs.View
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string text)
        {
            InitializeComponent();
            MessageText.Text = text;
            //DataContext = new ManageVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
