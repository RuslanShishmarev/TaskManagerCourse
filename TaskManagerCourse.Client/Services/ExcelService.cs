using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services
{
    public class ExcelService
    {
        public List<UserModel> GetAllUsersFromExcel(string path)
        {
            List<UserModel> userModels = new List<UserModel>();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                var firstSheet = package.Workbook.Worksheets["Лист1"];
                bool isEmpty = false;
                int itemIndex = 1;
                while (!isEmpty)
                {
                    string name = firstSheet.Cells[$"A{itemIndex}"].Text;
                    if (!string.IsNullOrEmpty(name))
                    {
                        string surname = firstSheet.Cells[$"B{itemIndex}"].Text;
                        string email = firstSheet.Cells[$"C{itemIndex}"].Text;
                        string phone = firstSheet.Cells[$"D{itemIndex}"].Text;
                        string password = firstSheet.Cells[$"E{itemIndex}"].Text;

                        UserStatus status = UserStatus.User;
                        UserModel userModel = new UserModel(name, surname, email, password, status, phone);
                        userModels.Add(userModel);
                        itemIndex++;
                    }
                    else
                    {
                        isEmpty = true;
                    }
                }
            }
            return userModels;
        }
    }
}
