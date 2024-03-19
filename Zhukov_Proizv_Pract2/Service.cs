using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Utilities;
using System;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;

namespace Zhukov_Proizv_Pract2;

public class Service
{
    public int ID_услуги { get; set; }
    public string Название_услуги { get; set; }
    public double Цена_услуги { get; set; }
    public string Продолжительность_услуги { get; set; }
    public Bitmap? Изображение_услуги { get; set; }
    public string Изображение_услуги_путь { get; set; }
    public int Размер_скидки { get; set; }
    public double Цена_со_скидкой { get; set; }
}

public class Record
{
    public int ID_заказа { get; set; }
    public string Дата_заказа { get; set; }
    public string Срок_исполнения_заказа { get; set; }
    public int ID_автомобиля { get; set; }
    public int ID_клиента { get; set; }
}
