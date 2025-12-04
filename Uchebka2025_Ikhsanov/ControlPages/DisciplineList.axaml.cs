using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Shop;
using Uchebka2025_Ikhsanov.Data;
using Uchebka2025_Ikhsanov.Views;

namespace Uchebka2025_Ikhsanov.ControlPages;

public partial class DisciplineList : UserControl
{
    private List<Discipline> _allDisciplines = new();
    private List<Department> _departments = new();
    public DisciplineList()
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
        var disciplines = await App.DbContext.Disciplines
            .Include(d => d.IdDepartNavigation)
            .ToListAsync();

        _allDisciplines = disciplines;
        DisciplinesDataGrid.ItemsSource = _allDisciplines;

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

    private void DiscipList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<DisciplineList>();
    }

    private async void InputElement_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        var dataGrid = this.FindControl<DataGrid>("DisciplinesDataGrid");
        var exam = dataGrid?.SelectedItem as Discipline;
        
        if (exam == null) return;
        
        if (VariableData.authuser.IdEmpNavigation != null &&
           (int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 2 ||
            int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 1))
        {
            VariableData.selectDiscipline = exam;
            var parent = this.VisualRoot as Window;
            var window = new CreateAndChangeDisciplines();
            await window.ShowDialog(parent);
        }

        LoadAllData();
    }

    private async void AddDiscipline_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectDiscipline = null;
        
        var parent = this.VisualRoot as Window;
        var addnewTeacher = new CreateAndChangeDisciplines();
        
        await addnewTeacher.ShowDialog(parent);
        
        LoadAllData();
    }

    private void DeleteDiscipline_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authuser.IdEmpNavigation != null &&
            (int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 2 ||
             int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 1))
        {
            Console.WriteLine("Deleting!");

            var button = sender as Button;
            var selectedProduct = button?.DataContext as Discipline;


            Console.WriteLine((selectedProduct == null) ? "Item not found" : "Item founded");

            if (selectedProduct == null) return;

            VariableData.selectDiscipline = selectedProduct;

            App.DbContext.Disciplines.Remove(selectedProduct);
            App.DbContext.SaveChanges();
        }

        LoadAllData();
    }

    private void ApplyFilter()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedDept = DepartmentFilter.SelectedItem as ComboBoxItem;
        var deptId = selectedDept?.Tag as int?;

        var filtered = _allDisciplines.Where(d =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 (d.NameDisc?.ToLower().Contains(search) == true);
            bool matchesDept = !deptId.HasValue || d.IdDepart == deptId.Value;
            return matchesSearch && matchesDept;
        }).ToList();

        DisciplinesDataGrid.ItemsSource = filtered;
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