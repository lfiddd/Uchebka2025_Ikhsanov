using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class TeachersList : UserControl
{
    public TeachersList()
    {
        InitializeComponent();
    }

    private void MainPageEmployees(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPageEmp>();
    }

    private void DisciplineList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DisciplineList>();
    }

    private void ExamsList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ExamList>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
       NavigationService.NavigateTo<Authorization>();
    }

    private void StudList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<StudentsList>();
    }

    private void DepartHeadLists(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DepartHeadList>();
    }
}