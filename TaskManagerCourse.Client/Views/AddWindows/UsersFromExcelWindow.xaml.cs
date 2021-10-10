using System.Windows;
using System.Windows.Controls;
using TaskManagerCourse.Client.ViewModels;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для UsersFromExcelWindow.xaml
    /// </summary>
    public partial class UsersFromExcelWindow : Window
    {
        public UsersFromExcelWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = DataContext as UsersPageViewModel;


            foreach (var item in e.RemovedItems)
            {
                if (item.GetType() == typeof(UserModel))
                {
                    var user = (UserModel)item;
                    if (context.SelectedUsersFromExcel.Contains(user))
                        context.SelectedUsersFromExcel.Remove(user);
                }
            }
            foreach (var item in e.AddedItems)
            {
                if (item.GetType() == typeof(UserModel))
                {
                    var user = (UserModel)item;
                    context.SelectedUsersFromExcel.Add(user);
                }
            }

        }
    }
}
