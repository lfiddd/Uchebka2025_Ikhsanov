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

public partial class DepartHeadList : UserControl
{
    private List<HeadOfDept> _allHeads = new();
    private List<Department> _departments = new();

    public DepartHeadList()
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
        var heads = await App.DbContext.HeadOfDepts
            .Include(h => h.TabNumberNavigation)
            .ThenInclude(e => e.IdDepartNavigation)
            .ToListAsync();

        _allHeads = heads;
        HeadDataGrid.ItemsSource = _allHeads;

        // Загружаем кафедры для фильтра
        _departments = await App.DbContext.Departments.ToListAsync();
        DepartmentFilter.Items.Clear();
        DepartmentFilter.Items.Add(new ComboBoxItem { Content = "Все кафедры", Tag = "" });
        foreach (var dept in _departments)
        {
            DepartmentFilter.Items.Add(new ComboBoxItem
            {
                Content = dept.NameDepart,
                Tag = dept.IdDepart
            });
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

    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private async void AddHeadButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectUser = null;

        var parent = this.VisualRoot as Window;
        var addnewTeacher = new CreateAndChangeDeparts();

        await addnewTeacher.ShowDialog(parent);
    }

    private void DeleteHeadButton_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ApplyFilter()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedDept = DepartmentFilter.SelectedItem as ComboBoxItem;
        var deptId = selectedDept?.Tag as int?;

        var filtered = _allHeads.Where(h =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 (h.TabNumberNavigation?.Fullname?.ToLower().Contains(search) == true);
            bool matchesDept = !deptId.HasValue ||
                               h.TabNumberNavigation?.IdDepart == deptId.Value;
            return matchesSearch && matchesDept;
        }).ToList();

        HeadDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void DepartmentFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilter();
    }
}