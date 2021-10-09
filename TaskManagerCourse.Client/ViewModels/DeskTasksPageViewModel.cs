using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Services;
using TaskManagerCourse.Client.Views.Components;
using TaskManagerCourse.Client.Views.Pages;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.ViewModels
{
    public class DeskTasksPageViewModel : BindableBase
    {
        private AuthToken _token;
        private DeskModel _desk;
        private UsersRequestService _usersRequestService;
        private TasksRequestService _tasksRequestService;
        private CommonViewService _viewService;

        private DeskTasksPage _page;

        public DeskTasksPageViewModel(AuthToken token, DeskModel desk, DeskTasksPage page)
        {
            _token = token;
            _desk = desk;
            _page = page;

            _viewService = new CommonViewService();
            _usersRequestService = new UsersRequestService();
            _tasksRequestService = new TasksRequestService();

            TasksByColumns = GetTasksByColumns(_desk.Id);
            _page.TasksGrid.Children.Add(CreateTasksGrid());

        }

        #region PROPERTIES
        private Dictionary<string, List<TaskClient>> _tasksByColumns = new Dictionary<string, List<TaskClient>>();

        public Dictionary<string, List<TaskClient>> TasksByColumns
        {
            get => _tasksByColumns; 
            set 
            { 
                _tasksByColumns = value;
                RaisePropertyChanged(nameof(TasksByColumns));
            }
        }

        #endregion

        #region METHODS

        private Dictionary<string, List<TaskClient>> GetTasksByColumns(int deskId)
        {
            var tasksByColumns = new Dictionary<string, List<TaskClient>>();
            var allTasks = _tasksRequestService.GetTasksByDesk(_token, deskId);
            foreach(string column in _desk.Columns)
            {
                tasksByColumns.Add(column, allTasks
                    .Where(t => t.Column == column)
                    .Select(t => new TaskClient(t)).ToList());
            }
            return tasksByColumns;
        }

        private Grid CreateTasksGrid()
        {
            ResourceDictionary resource = new ResourceDictionary();
            resource.Source = new Uri("./Resources/Styles/MainStyle.xaml", UriKind.Relative);

            Grid grid = new Grid();
            var row0 = new RowDefinition();
            row0.Height = new GridLength(30);

            var row1 = new RowDefinition();

            grid.RowDefinitions.Add(row0);
            grid.RowDefinitions.Add(row1);

            int culumnCount = 0;
            foreach(var column in TasksByColumns)
            {
                var col = new ColumnDefinition();
                grid.ColumnDefinitions.Add(col);

                //header
                TextBlock header = new TextBlock();
                header.Text = column.Key;
                header.Style = resource["headerTBlock"] as Style;

                Grid.SetRow(header, 0);
                Grid.SetColumn(header, culumnCount);

                grid.Children.Add(header);

                //column
                ItemsControl columnControl = new ItemsControl();
                Grid.SetRow(columnControl, 1);
                Grid.SetColumn(columnControl, culumnCount);

                var tasksViews = new List<TaskControl>();

                foreach (var task in column.Value)
                {
                    var taskView = new TaskControl(task);
                    tasksViews.Add(taskView);
                }
                columnControl.ItemsSource = tasksViews;
                grid.Children.Add(columnControl);

                culumnCount++;
            }

            return grid;

        }
        #endregion

    }
}
