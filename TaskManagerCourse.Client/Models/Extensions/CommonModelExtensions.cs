using System.IO;
using System.Windows.Media.Imaging;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Models.Extensions
{
    public static class CommonModelExtensions
    {
        public static BitmapImage LoadImage(this CommonModel model)
        {
            if (model?.Photo == null || model.Photo.Length == 0)
                return null;

            var image = new BitmapImage();
            using (var memSm = new MemoryStream(model.Photo))
            {
                memSm.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = memSm;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
