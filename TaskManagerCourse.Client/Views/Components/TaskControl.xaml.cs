using System.Collections.Generic;
using System.Windows.Controls;
using TaskManagerCourse.Client.Models;

namespace TaskManagerCourse.Client.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для TaskControl.xaml
    /// </summary>
    public partial class TaskControl : UserControl
    {
        public TaskControl(TaskClient task)
        {
            InitializeComponent();
            DataContext = task;
        }
    }
}
