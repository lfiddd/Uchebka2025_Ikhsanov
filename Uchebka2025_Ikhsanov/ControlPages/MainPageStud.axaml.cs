using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class MainPageStud : UserControl
{
    public MainPageStud()
    {
        InitializeComponent();
    }

    private void MainPageStudents(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPageStud>();
    }

    private void DisciplineList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DisciplineListForStud>();
    }

    private void ExamsList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ExamListForStud>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<StudentAuth>();
    }

    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}