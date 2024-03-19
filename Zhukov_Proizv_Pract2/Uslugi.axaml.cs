using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using Avalonia.Media;
using Avalonia.Controls;
using System.IO;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Utilities;
using System;
using System.Globalization;
using Avalonia.Platform;

namespace Zhukov_Proizv_Pract2
{

    public partial class Uslugi : Window
    {
        private List<Service> _services;
        string fullTable = "SELECT * FROM Услуги";
        string connStr = "server=10.10.1.24;database=abd1_11;port=3306;User Id=user_01;password=user01pro";
        private MySqlConnection conn;

        public Uslugi()
        {
            InitializeComponent();
            ShowTable(fullTable);
            FillCmb();
        }

        public void ShowTable(string sql)
        {
            _services = new List<Service>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var Serv = new Service()
                {
                    ID_услуги = reader.GetInt32("ID_услуги"),
                    Название_услуги = reader.GetString("Название_услуги"),
                    Цена_услуги = reader.GetInt32("Цена_услуги"),
                    Продолжительность_услуги = reader.GetString("Продолжительность_услуги"),
                    Изображение_услуги = LoadImage("avares://Zhukov_Proizv_Pract2/Photos/" + reader.GetString("Изображение_услуги")),
                    Изображение_услуги_путь = reader.GetString("Изображение_услуги"),
                    Размер_скидки = reader.GetInt32("Размер_скидки"),
                    Цена_со_скидкой = reader.GetInt32("Цена_услуги") - (reader.GetInt32("Цена_услуги") * reader.GetInt32("Размер_скидки") / 100)
                };
                _services.Add(Serv);
            }
            
            conn.Close();
            DataGrid.ItemsSource = _services;
            DataGrid.LoadingRow += DataGrid_LoadingRow;
        }
        
        public Bitmap LoadImage(string Uri)
        {
            return new Bitmap(AssetLoader.Open(new Uri(Uri)));
        }
        private void SearchService(object? sender, TextChangedEventArgs e)
        {
            var srv = _services;
            srv = srv.Where(x => x.Название_услуги.Contains(ServSearch.Text)).ToList();
            DataGrid.ItemsSource = srv;
        }
        
        private void DiscountFilter(object? sender, SelectionChangedEventArgs e)
        {
            var discontComboBox = (ComboBox)sender;
            var selectedDiscount = discontComboBox.SelectedItem as string;
            
            int startDiscount = 0;
            int endDiscount = 0;
            
            if (selectedDiscount == "Все скидки")
            {
                DataGrid.ItemsSource = _services;
            }
            else if (selectedDiscount == "От 0% до 5%")
            {
                startDiscount = 1;
                endDiscount = 5;
            }
            else if (selectedDiscount == "От 5% до 15%")
            {
                startDiscount = 5;
                endDiscount = 15;
            }
            else if (selectedDiscount == "От 15% до 30%")
            {
                startDiscount = 15;
                endDiscount = 30;
            }
            else if (selectedDiscount == "От 30% до 70%")
            {
                startDiscount = 30;
                endDiscount = 71;
            }
            
            if (startDiscount != 0 && endDiscount != 0)
            {
                var filteredUsers = _services
                    .Where(x => x.Размер_скидки >= startDiscount && x.Размер_скидки < endDiscount)
                    .ToList();

                DataGrid.ItemsSource = filteredUsers;
            }
        }

        

        public void FillCmb()
        {
            _services = new List<Service>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand(fullTable, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var Serv = new Service()
                {
                    ID_услуги = reader.GetInt32("ID_услуги"),
                    Название_услуги = reader.GetString("Название_услуги"),
                    Цена_услуги = reader.GetInt32("Цена_услуги"),
                    Продолжительность_услуги = reader.GetString("Продолжительность_услуги"),
                    Изображение_услуги = LoadImage("avares://Zhukov_Proizv_Pract2/Photos/" + reader.GetString("Изображение_услуги")),
                    Изображение_услуги_путь = reader.GetString("Изображение_услуги"),
                    Размер_скидки = reader.GetInt32("Размер_скидки"),
                    Цена_со_скидкой = reader.GetInt32("Цена_услуги") - (reader.GetInt32("Цена_услуги") * reader.GetInt32("Размер_скидки") / 100)
                };
                    _services.Add(Serv);
            }
            conn.Close();
            
            var discontComboBox = this.Find<ComboBox>("DiscontComboBox");
            discontComboBox.ItemsSource = new List<string>
            {
                "Все скидки",
                "От 0% до 5%",
                "От 5% до 15%",
                "От 15% до 30%",
                "От 30% до 70%"
            };
        }
        
        private void SortAscending(object sender, RoutedEventArgs e)
        {
            var sortedItems = DataGrid.ItemsSource.Cast<Service>().OrderBy(s => s.Цена_услуги).ToList();
            DataGrid.ItemsSource = sortedItems;
        }

        private void SortDescending(object sender, RoutedEventArgs e)
        {
            var sortedItems = DataGrid.ItemsSource.Cast<Service>().OrderByDescending(s => s.Цена_услуги).ToList();
            DataGrid.ItemsSource = sortedItems;
        }
        
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Service _service = e.Row.DataContext as Service; 
            if (_service != null) 
            {
                if (0 <= _service.Размер_скидки && _service.Размер_скидки < 5) 
                {
                    e.Row.Background = Brushes.DarkGreen; 
                }
                else if (5 <= _service.Размер_скидки && _service.Размер_скидки < 15) 
                {
                    e.Row.Background = Brushes.Green;
                }
                else if (15 <= _service.Размер_скидки && _service.Размер_скидки < 30) 
                {
                    e.Row.Background = Brushes.LimeGreen;
                }
                else if (30 <= _service.Размер_скидки && _service.Размер_скидки <= 70) 
                {
                    e.Row.Background = Brushes.PaleGreen;
                }
                else
                {
                    e.Row.Background = Brushes.Transparent; 
                }
            }
        }
        
        private void AddData(object? sender, RoutedEventArgs e)
        {
            Service newServ = new Service();
            AddEditForm add = new AddEditForm(newServ, _services);
            add.Title = "Добавление данных";
            add.Zag.Text = "Добавление данных";
            add.Show();
            this.Close();
        }

        private void Edit(object? sender, RoutedEventArgs e)
        {
            Service currentServ = DataGrid.SelectedItem as Service;
            if (currentServ == null)
                return;
            AddEditForm edit = new AddEditForm(currentServ, _services);
            edit.Title = "Редактирование данных";
            edit.Zag.Text = "Редактирование данных";
            edit.Show();
            this.Close();
        }
        
        public void Exit_Program(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void LogOut(object? sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }
        
        private void Recd(object? sender, RoutedEventArgs e)
        {
            Records logout = new Records();
            logout.Show();
            this.Close();
        }
        
        private void DeleteData(object? sender, RoutedEventArgs e)
        {
            try
            {
                Service srv = DataGrid.SelectedItem as Service;
                if (srv == null)
                {
                    return;
                }
                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "DELETE FROM Услуги WHERE ID_услуги = " + srv.ID_услуги;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                _services.Remove(srv);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}