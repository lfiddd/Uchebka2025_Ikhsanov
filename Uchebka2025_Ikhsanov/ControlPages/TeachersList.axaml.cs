using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Shop;
using Uchebka2025_Ikhsanov.Data;
using Uchebka2025_Ikhsanov.Views;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class TeachersList : UserControl
{
    private List<Teacher> _allTeachers = new();
    public TeachersList()
    {
        InitializeComponent();
        if (VariableData.authuser.IdEmpNavigation == null || VariableData.authuser.IdStudentNavigation != null)
        {
            StudBtn.IsVisible = false;
            TeachBtn.IsVisible = false;
            HeadBtn.IsVisible = false;
        }
        
        else if (VariableData.authuser.IdEmpNavigation != null &&
                 int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 3)
        {
            TeachBtn.IsVisible = false;
            HeadBtn.IsVisible = false;
        }
        else if (VariableData.authuser.IdEmpNavigation != null &&
                 int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 2)
        {
            HeadBtn.IsVisible = false;
        }

        LoadAllData();
    }

    private async void LoadAllData()
    {
        var teachers = await App.DbContext.Teachers.ToListAsync();
        _allTeachers = teachers;
        TeachersDataGrid.ItemsSource = _allTeachers;

        // Собираем уникальные звания
        var ranks = _allTeachers
            .Where(t => !string.IsNullOrEmpty(t.Rank))
            .Select(t => t.Rank)
            .Distinct()
            .OrderBy(r => r)
            .ToList();

        RankFilter.Items.Clear();
        RankFilter.Items.Add(new ComboBoxItem { Content = "Все звания", Tag = "" });
        foreach (var rank in ranks)
        {
            RankFilter.Items.Add(new ComboBoxItem { Content = rank, Tag = rank });
        }

        ApplyFilter();
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

    private void TeachList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<TeachersList>();
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;
        
        var parent = this.VisualRoot as Window;
        var addnewTeacher = new CreateAndChangeTeachers();
        
        await addnewTeacher.ShowDialog(parent);
    }

    private void ApplyFilter()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedRank = RankFilter.SelectedItem as ComboBoxItem;
        var rankFilter = selectedRank?.Tag as string;

        var filtered = _allTeachers.Where(t =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 (t.TabNumberNavigation.Fullname?.ToLower().Contains(search) == true);
            bool matchesRank = string.IsNullOrEmpty(rankFilter) || t.Rank == rankFilter;
            return matchesSearch && matchesRank;
        }).ToList();

        TeachersDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void RankFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilter();
    }
}