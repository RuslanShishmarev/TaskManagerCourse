using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TaskManagerCourse.Client.Models.Extensions;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Models
{
    public class ModelClient<T> where T : CommonModel
    {
        public T Model { get; private set; }
        public ModelClient(T model)
        {
            Model = model;
        }
        public BitmapImage Image
        {
            get => Model?.LoadImage();
        }
    }
}
