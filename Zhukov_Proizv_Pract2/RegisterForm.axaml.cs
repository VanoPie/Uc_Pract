using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace Zhukov_Proizv_Pract2;

public partial class RegisterForm : Window
{
    public RegisterForm()
    {
        InitializeComponent();
    }
    
    private MySqlConnection conn;
    private string connStr = "server=10.10.1.24;database=abd1_11;port=3306;User Id=user_01;password=user01pro";
    
    private void Regist(object? sender, RoutedEventArgs e)
    {
        conn = new MySqlConnection(connStr);
        conn.Open();
        string regist = "INSERT INTO Клиенты(ФИО, Адрес, Телефон, Электронная_почта_клиента, Пароль_клиента) VALUES ('" + FIO.Text + "', '" + Adres.Text + "', '" + Telephone.Text + "', '" + Email.Text + "', '" + Password.Text + "');";
        MySqlCommand cmd = new MySqlCommand(regist, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        MainWindow auth = new MainWindow();
        this.Close();
        auth.Show();
    }
}