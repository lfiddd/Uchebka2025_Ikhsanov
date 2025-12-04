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

public partial class ExamList : UserControl
{
    private List<Exam> _allExams = new();
    private List<Discipline> _disciplines = new();
    public ExamList()
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
        var exams = await App.DbContext.Exams
            .Include(e => e.StudentRegNavigation)
            .Include(e => e.DisciplineCodeNavigation)
            .Include(e => e.ExaminerTabNavigation)
            .ToListAsync();

        _allExams = exams;
        ExamsDataGrid.ItemsSource = _allExams;

        _disciplines = await App.DbContext.Disciplines.ToListAsync();
        DisciplineFilter.Items.Clear();
        DisciplineFilter.Items.Add(new ComboBoxItem { Content = "Все дисциплины", Tag = "" });
        foreach (var disc in _disciplines)
        {
            DisciplineFilter.Items.Add(new ComboBoxItem 
            { 
                Content = disc.NameDisc, 
                Tag = disc.IdDiscipline 
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

    private void ExamsList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ExamList>();
    }

    private void DeleteExam_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authuser.IdEmpNavigation != null &&
            (int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 2 ||
             int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 1))
        {
            Console.WriteLine("Deleting!");

            var button = sender as Button;
            var selectedProduct = button?.DataContext as Exam;


            Console.WriteLine((selectedProduct == null) ? "Item not found" : "Item founded");

            if (selectedProduct == null) return;

            VariableData.selectExam = selectedProduct;

            App.DbContext.Exams.Remove(selectedProduct);
            App.DbContext.SaveChanges();
        }

        LoadAllData();
    }

    private async void AddExam_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectExam = null;
        
        var parent = this.VisualRoot as Window;
        var addnewTeacher = new CreateAndChangeExams();
        
        await addnewTeacher.ShowDialog(parent);
        
        LoadAllData();
    }

    private async void InputElement_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        // Находим сам DataGrid (по имени)
        var dataGrid = this.FindControl<DataGrid>("ExamsDataGrid");
        var exam = dataGrid?.SelectedItem as Exam;

        if (exam == null) return;

        // Дальше твоя логика с ролями
        if (VariableData.authuser.IdEmpNavigation != null &&
            int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 3)
        {
            VariableData.selectExam = exam;
            var parent = this.VisualRoot as Window;
            var window = new ChangeGradeInExam();
            await window.ShowDialog(parent);
        }
        else if (VariableData.authuser.IdEmpNavigation != null &&
                 (int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 2 ||
                  int.Parse(VariableData.authuser.IdEmpNavigation.PositionEmp) == 1))
        {
            VariableData.selectExam = exam;
            var parent = this.VisualRoot as Window;
            var window = new CreateAndChangeExams();
            await window.ShowDialog(parent);
        }

        LoadAllData();
    }

    private void ApplyFilter()
    {
        var search = SearchBox.Text?.ToLower() ?? "";
        var selectedDisc = DisciplineFilter.SelectedItem as ComboBoxItem;
        var discId = selectedDisc?.Tag as int?;

        var filtered = _allExams.Where(e =>
        {
            bool matchesSearch = string.IsNullOrEmpty(search) ||
                                 (e.StudentRegNavigation?.Fullname?.ToLower().Contains(search) == true) ||
                                 (e.ExaminerTabNavigation?.Fullname?.ToLower().Contains(search) == true) ||
                                 (e.DisciplineCodeNavigation?.NameDisc?.ToLower().Contains(search) == true);
            bool matchesDisc = !discId.HasValue || e.DisciplineCode == discId.Value;
            return matchesSearch && matchesDisc;
        }).ToList();

        ExamsDataGrid.ItemsSource = filtered;
    }

    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void DisciplineFilter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyFilter();
    }
}