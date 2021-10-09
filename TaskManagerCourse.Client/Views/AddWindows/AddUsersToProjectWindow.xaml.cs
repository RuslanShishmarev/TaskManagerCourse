using System.Windows;
using System.Windows.Controls;
using TaskManagerCourse.Client.ViewModels;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddUsersToProjectWindow.xaml
    /// </summary>
    public partial class AddUsersToProjectWindow : Window
    {
        public AddUsersToProjectWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (ProjectsPageViewModel)DataContext;

            foreach (UserModel user in e.RemovedItems)
                viewModel.SelectedUsersForProject.Remove(user);

            foreach (UserModel user in e.AddedItems)
                viewModel.SelectedUsersForProject.Add(user);
        }
    }
}
