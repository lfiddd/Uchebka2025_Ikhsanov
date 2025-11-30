using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class ExamListForStud : UserControl
{
    public ExamListForStud()
    {
        InitializeComponent();
    }

    private void MainPageEmployees(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPageStud>();
    }

    private void DisciplineList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DisciplineListForStud>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<StudentAuth>();
    }
}