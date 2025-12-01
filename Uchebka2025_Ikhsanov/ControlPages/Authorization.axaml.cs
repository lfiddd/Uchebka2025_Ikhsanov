using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class Authorization : UserControl
{
    private MainWindow _mainWindow;
    public Authorization()
    {
        InitializeComponent();
        _mainWindow = (MainWindow)VisualRoot;
    }

    private async void LoginBtn_Click(object? sender, RoutedEventArgs e)
    {
        string login = LoginText.Text;
        string password = PasswordText.Text;
        var d = App.DbContext.Logins.FirstOrDefault(us => us.Login1 == login && us.PasswordL == password);
        if (d != null)
        {
            VariableData.authuser = d;
            await MessageBoxManager.GetMessageBoxStandard("Успех", "Добро пожаловать").ShowAsync();
            NavigationService.NavigateTo<MainPageEmp>();

        }
        else
        {
            await MessageBoxManager.GetMessageBoxStandard("Провал",
                "Такого пользователя не существует. Перепроверьте данные или зарегайтесь!").ShowAsync();
        }
    }
    
    
}