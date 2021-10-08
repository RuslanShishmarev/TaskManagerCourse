using System.Windows.Media.Imaging;
using TaskManagerCourse.Client.Models.Extensions;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Models
{
    public class TaskClient
    {
        public TaskModel Model { get; private set; }
        public TaskClient(TaskModel model)
        {
            Model = model;
        }
        public UserModel Creator { get; set; }
        public UserModel Executor { get; set; }

        public BitmapImage Image
        {
            get
            {
                return Model.LoadImage();
            }
        }

        public bool IsHaveFile
        {
            get => Model?.File != null;
        }
    }
}
