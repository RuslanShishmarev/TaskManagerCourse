using TaskManagerCourse.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using TaskManagerCourse.Common.Models;
using TaskManagerCourse.Client.Models;

namespace TaskManagerCourse.Client.Services.Tests
{
    [TestClass()]
    public class UsersRequestServiceTests
    {

        private AuthToken _token;
        private UsersRequestService _service;

        public UsersRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("admin", "qwerty123");
            _service = new UsersRequestService();
        }

        [TestMethod()]
        public void GetTokenTest()
        {
            var token = new UsersRequestService().GetToken("admin", "qwerty123");
            Console.WriteLine(token.access_token);
            Assert.IsNotNull(token);
        }

        [TestMethod()]
        public void CreateUserTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "qwerty123");

            UserModel userTest = new UserModel("Mary", "Gray", "MaryGray@mail.ru", "qwerty", UserStatus.User, "789456123");

            var result = service.CreateUser(token, userTest);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void GetAllUsersTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "qwerty123");

            var result = service.GetAllUsers(token);

            Console.WriteLine(result.Count);

            Assert.AreNotEqual(Array.Empty<UserModel>(), result.ToArray());
        }

        [TestMethod()]
        public void DeleteUserTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "qwerty123");

            var result = service.DeleteUser(token, 5);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void CreateMultipleUsersTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "qwerty123");

            UserModel userTest1 = new UserModel("Vlad", "Dmitriev", "VladDmitriev@mail.ru", "qwerty", UserStatus.User, "789456123");
            UserModel userTest2 = new UserModel("Alex", "Menshova", "AlexMenshova@mail.ru", "qwerty", UserStatus.Editor, "789456123");
            UserModel userTest3 = new UserModel("Artem", "Volodecky", "ArtemVolodecky@mail.ru", "qwerty", UserStatus.User, "789456123");

            List<UserModel> users = new List<UserModel>() { userTest1, userTest2, userTest3 };

            var result = service.CreateMultipleUsers(token, users);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            var service = new UsersRequestService();

            var token = service.GetToken("admin", "qwerty123");

            UserModel userTest = new UserModel("Artem", "Volodecky", "VolodeckyArtem@mail.ru", "qwerty", UserStatus.User, "+7989456123");
            userTest.Id = 9;

            var result = service.UpdateUser(token, userTest);

            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void GetProjectUserAdminTest()
        {
            var id = _service.GetProjectUserAdmin(_token, 4);
            Assert.AreEqual(null, id);
        }
    }
}