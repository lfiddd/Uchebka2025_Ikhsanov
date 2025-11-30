using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class DisciplineListForStud : UserControl
{
    public DisciplineListForStud()
    {
        InitializeComponent();
    }

    private void MainPageEmployees(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPageStud>();
    }


    private void ExamsList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ExamListForStud>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<StudentAuth>();
    }
}