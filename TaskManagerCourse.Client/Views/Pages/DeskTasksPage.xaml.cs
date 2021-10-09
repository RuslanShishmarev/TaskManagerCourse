using System.Windows.Controls;

namespace TaskManagerCourse.Client.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeskTasksPage.xaml
    /// </summary>
    public partial class DeskTasksPage : Page
    {
        public Grid TasksGrid { get; private set; }
        public DeskTasksPage()
        {
            InitializeComponent();
            TasksGrid = tasksGrid;
        }
    }
}
