using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class ExamList : UserControl
{
    public ExamList()
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

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<Authorization>();
    }

    private void TeachList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<TeachersList>();
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