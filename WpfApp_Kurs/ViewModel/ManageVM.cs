using System;
using System.Collections.Generic;
using WpfApp_Kurs.Model;
using WpfApp_Kurs.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp_Kurs.ViewModel
{
    public class ManageVM : INotifyPropertyChanged
    {
        //Заповедники
        private List<Reserve> allReserves = DateAdd.GetAllReserves();
        public List<Reserve> AllReserves
        {
            get { return allReserves; }
            set
            {
                allReserves = value;
                NotifyPropertyChanged("AllReserves");
            }
        }

        //Виды
        private List<Animal> allAnimals = DateAdd.GetAllAnimals();
        public List<Animal> AllAnimals
        {
            get
            {
                return allAnimals;
            }
            private set
            {
                allAnimals = value;
                NotifyPropertyChanged("AllAnimals");
            }
        }
        //Особи
        private List<Individual> allIndividuals = DateAdd.GetAllIndividuals();
        public List<Individual> AllIndividuals
        {
            get
            {
                return allIndividuals;
            }
            private set
            {
                allIndividuals = value;
                NotifyPropertyChanged("AllIndividuals");
            }
        }

        //CreateReserve
        public string ReserveName { get; set; }
        public int ReserveFounded { get; set; }

        //CreateAnimal
        public string AnimalName { get; set; }

        //CreateIndividual
        public string IndividualName { get; set; }
        public string IndividualSex { get; set; }
        public int IndividualAge { get; set; }
        public Animal IndividualAnimal { get; set; }
        public Reserve IndividualReserve { get; set; }

        //Выделенные элементы
        public TabItem SelectedTabItem { get; set; }
        public Individual SelectedIndividual { get; set; }
        public Animal SelectedAnimal { get; set; }
        public Reserve SelectedReserve { get; set; }

        //Комманды для добавления новых 
        private RelayCommand addReserve;
        public RelayCommand AddReserve
        {
            get
            {
                return addReserve ?? new RelayCommand(obj =>
                {
                    Window wind = obj as Window;
                    string res_str = "";
                    if (ReserveName == null || ReserveName.Replace(" "," ").Length == 0)
                    { 

                    }
                    else
                    {
                        res_str = DateAdd.CreateReserve(ReserveName, ReserveFounded);
                        ShowMessage(res_str); //Если поменять местами эту строчку с нижней, то обновление таблицы будет не после закрытия окна сообщения, а сразу после сохранения.
                        UpdateData();
                        SetNullValuesToProperties();
                        wind.Close();
                    }
                    
                }
                );
            }
        }

        private RelayCommand addAnimal;
        public RelayCommand AddAnimal
        {
            get
            {
                return addAnimal ?? new RelayCommand(obj =>
                {
                    Window wind = obj as Window;
                    string res_str = "";
                    if (AnimalName == null || AnimalName.Replace(" ", " ").Length == 0)
                    {

                    }
                    else
                    {
                        res_str = DateAdd.CreateAnimal(AnimalName);
                        ShowMessage(res_str);
                        UpdateData();
                        SetNullValuesToProperties();
                        wind.Close();
                    }

                }
                );
            }
        }

        private RelayCommand addIndividual;
        public RelayCommand AddIndividual
        {
            get
            {
                return addIndividual ?? new RelayCommand(obj =>
                {
                    Window wind = obj as Window;
                    string res_str = "";
                    if (IndividualName == null || IndividualName.Replace(" ", " ").Length == 0)
                    {

                    }
                    else
                    {
                        res_str = DateAdd.CreateIndividual(IndividualName, IndividualSex, IndividualAge, IndividualAnimal, IndividualReserve);
                        ShowMessage(res_str);
                        UpdateData();
                        SetNullValuesToProperties();
                        wind.Close();
                    }

                }
                );
            }
        }

        //Комманды для удаления
        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string res_str = "Ничего не выбрано";
                    //Особь
                    if (SelectedTabItem.Name == "IndividualTab" && SelectedIndividual != null)
                    {
                        res_str = DateAdd.DeleteIndividual(SelectedIndividual);
                        UpdateData();
                    }
                    //Вид
                    if (SelectedTabItem.Name == "AnimalTab" && SelectedAnimal != null)
                    {
                        res_str = DateAdd.DeleteAnimal(SelectedAnimal);
                        UpdateData();
                    }
                    //Заповедник
                    if (SelectedTabItem.Name == "ReserveTab" && SelectedReserve != null)
                    {
                        res_str = DateAdd.DeleteReserve(SelectedReserve);
                        UpdateData();
                    }
                    //Обновление
                    SetNullValuesToProperties();
                    ShowMessage(res_str);
                }
                );
            }
        }

        //Комманды для открытия окон
        private RelayCommand openAddReserveWindow;
        public RelayCommand OpenAddReserveWindow //Пожалуйста, перестань путать private и public
        {
            get
            {
                return openAddReserveWindow ?? new RelayCommand(obj =>
                {
                    OpenAddReserveWindowMTD();
                }
                );
            }
        }

        private RelayCommand openAddAnimalWindow;
        public RelayCommand OpenAddAnimalWindow
        {
            get
            {
                return openAddAnimalWindow ?? new RelayCommand(obj =>
                {
                    OpenAddAnimalWindowMTD();
                }
                );
            }
        }

        private RelayCommand openAddIndividualWindow;
        public RelayCommand OpenAddIndividualWindow
        {
            get
            {
                return openAddIndividualWindow ?? new RelayCommand(obj =>
                {
                    OpenAddIndividualWindowMTD();
                }
                );
            }
        }

        //Обновление данных, которые временно не работают. Надо либо больше окон, либо больше ума :(
        private RelayCommand editIndividual;
        public RelayCommand EditIndividual
        {
            get
            {
                return editIndividual ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана особь";
                    string noAnimalReserve_str = "Не выбран заповедник или вид";
                    if (SelectedIndividual != null)
                    {
                        if ((IndividualAnimal != null) || (IndividualReserve != null))
                        {
                            resultStr = DateAdd.EditIndividual(SelectedIndividual, IndividualName, IndividualSex, IndividualAge, IndividualAnimal, IndividualReserve);

                            UpdateData();
                            SetNullValuesToProperties();
                            ShowMessage(resultStr);
                            window.Close();
                        }
                        else ShowMessage(noAnimalReserve_str);
                    }
                    else ShowMessage(resultStr);

                }
                );
            }
        }
        private RelayCommand editAnimal;
        public RelayCommand EditAnimal
        {
            get
            {
                return editAnimal ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result_str = "Не выбрана позиция";
                    if (SelectedAnimal != null)
                    {
                        result_str = DateAdd.EditAnimal(SelectedAnimal, AnimalName);

                        UpdateData();
                        SetNullValuesToProperties();
                        ShowMessage(result_str);
                        window.Close();
                    }
                    else ShowMessage(result_str);

                }
                );
            }
        }

        private RelayCommand editReserve;
        public RelayCommand EditReserve
        {
            get
            {
                return editReserve ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string result_str = "Не выбран отдел";
                    if (SelectedReserve != null)
                    {
                        result_str = DateAdd.EditReserve(SelectedReserve, ReserveName, ReserveFounded);

                        UpdateData();
                        SetNullValuesToProperties();
                        ShowMessage(result_str);
                        window.Close();
                    }
                    else ShowMessage(result_str);

                }
                );
            }
        }

        private RelayCommand editItem;
        public RelayCommand EditItem
        {
            get
            {
                return editItem ?? new RelayCommand(obj =>
                {
                    string res_str = "Ничего не выбрано";
                    //Особь
                    if (SelectedTabItem.Name == "IndividualTab" && SelectedIndividual != null)
                    {
                        EditAddIndividualWindowMTD();
                    }
                    //Вид
                    if (SelectedTabItem.Name == "AnimalTab" && SelectedAnimal != null)
                    {
                        EditAddAnimalWindowMTD();
                    }
                    //Заповедник
                    if (SelectedTabItem.Name == "ReserveTab" && SelectedReserve != null)
                    {
                        EditAddReserveWindowMTD();
                    }
                }
                );
            }
        }

        //Открытие окон создания
        private void OpenAddReserveWindowMTD()
        {
            AddEditReserveWindow newReserveWindow = new AddEditReserveWindow();
            SetCenterPosition(newReserveWindow);
        }
        private void OpenAddAnimalWindowMTD()
        {
            AddEditAnimalWindow newAnimalWindow = new AddEditAnimalWindow();
            SetCenterPosition(newAnimalWindow);
        }
        private void OpenAddIndividualWindowMTD()
        {
            AddEditIndividualWindow newIndividualWindow = new AddEditIndividualWindow();
            SetCenterPosition(newIndividualWindow);
        }

        //Открытие окон редактирвоания
        private void EditAddReserveWindowMTD()
        {
            AddEditReserveWindow editReserveWindow = new AddEditReserveWindow();
            SetCenterPosition(editReserveWindow);
        }
        private void EditAddAnimalWindowMTD()
        {
            AddEditAnimalWindow editAnimalWindow = new AddEditAnimalWindow();
            SetCenterPosition(editAnimalWindow);
        }
        private void EditAddIndividualWindowMTD()
        {
            AddEditIndividualWindow editIndividualWindow = new AddEditIndividualWindow();
            SetCenterPosition(editIndividualWindow);
        }
        //Чтобы по красоте открывались окна
        private void SetCenterPosition(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        //Обнуление всех свойств
        private void SetNullValuesToProperties()
        {
            //Особь
            IndividualName = null;
            IndividualAge = 0;
            IndividualSex = null;
            IndividualAnimal = null;
            IndividualReserve = null;
            //Вид
            AnimalName = null;
            //Заповедник
            ReserveName = null;
            ReserveFounded = 0;
        }

        //Обновление количества при добавлении нового
        private void UpdateData()
        {
            UpdateReservesView();
            UpdateAnimalsView();
            UpdateIndividualsView();
        }
        //Обновление информации в таблице во время работы программы
        private void UpdateReservesView()
        {
            AllReserves = DateAdd.GetAllReserves();
            MainWindow.AllReservesView.ItemsSource = null;
            MainWindow.AllReservesView.Items.Clear();
            MainWindow.AllReservesView.ItemsSource = AllReserves;
            MainWindow.AllReservesView.Items.Refresh();
        }
        private void UpdateAnimalsView()
        {
            AllAnimals = DateAdd.GetAllAnimals();
            MainWindow.AllAnimalsView.ItemsSource = null;
            MainWindow.AllAnimalsView.Items.Clear();
            MainWindow.AllAnimalsView.ItemsSource = AllAnimals;
            MainWindow.AllAnimalsView.Items.Refresh();
        }
        private void UpdateIndividualsView()
        {
            AllIndividuals = DateAdd.GetAllIndividuals();
            MainWindow.AllIndividualsView.ItemsSource = null;
            MainWindow.AllIndividualsView.Items.Clear();
            MainWindow.AllIndividualsView.ItemsSource = AllIndividuals;
            MainWindow.AllIndividualsView.Items.Refresh();
        }
        //Штука для покраски блоков текстбокса при проверке
        //private void BlockControll(Window wind, string blockName)
        //{
        //    Control block = wind.FindName(blockName) as Control;
        //}

        private void ShowMessage(string message)
        {
            MessageWindow messageViev = new MessageWindow(message);
            SetCenterPosition(messageViev);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
