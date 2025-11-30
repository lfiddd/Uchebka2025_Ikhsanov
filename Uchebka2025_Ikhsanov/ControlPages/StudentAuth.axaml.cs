using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class StudentAuth : UserControl
{
    private MainWindow _mainWindow;
    public StudentAuth()
    {
        InitializeComponent();
        _mainWindow = (MainWindow)VisualRoot;
    }

    private async void LoginBtn_Click(object? sender, RoutedEventArgs e)
    {
        string login = LoginText.Text;
        string password = PasswordText.Text;
        var d = App.DbContext.LoginStuds.FirstOrDefault(us => us.Login == login && us.LPassword == password);
        if (d != null)
        {
            VariableData.authStudent = d;
            await MessageBoxManager.GetMessageBoxStandard("Успех", "Добро пожаловать").ShowAsync();
            NavigationService.NavigateTo<MainPageStud>();

        }
        else
        {
            await MessageBoxManager.GetMessageBoxStandard("Провал",
                "Такого пользователя не существует. Перепроверьте данные или зарегайтесь!").ShowAsync();
        }
    }
    
    private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            var selectedItem = e.AddedItems[0];

            if (selectedItem is ComboBoxItem cbi)
            {
                string role = cbi.Name; 

                if (role == "studentSelection")
                {
                    NavigationService.NavigateTo<StudentAuth>();
                }
                else if (role == "employeeSelection")
                {
                    NavigationService.NavigateTo<Authorization>();
                }
            }
            
        }
    }
}