using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.Windows;


namespace Zhukov_Proizv_Pract2;

public partial class Records : Window
{
    private List<Record> _rec;

    public Records()
    {
        InitializeComponent();
    }

    private MySqlConnection conn;
    string connStr = "server=10.10.1.24;database=abd1_11;port=3306;User Id=user_01;password=user01pro";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string add = "INSERT INTO Записи (Электронный_адрес_клиента, Дата_приема, Описание_проблемы) VALUES ('" + Name.Text + "', '" + Date_Pos.Text + "', '" + Problem.Text + "');";
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Uslugi back = new Uslugi();
        this.Close();
        back.Show();
    }
}