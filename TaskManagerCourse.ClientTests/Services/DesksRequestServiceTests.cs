using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagerCourse.Client.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerCourse.Common.Models;
using TaskManagerCourse.Client.Models;
using System.Net;

namespace TaskManagerCourse.Client.Services.Tests
{
    [TestClass()]
    public class DesksRequestServiceTests
    {
        private AuthToken _token;
        private DesksRequestService _service;

        public DesksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("admin", "qwerty123");
            _service = new DesksRequestService();
        }

        [TestMethod()]
        public void GetAllDesksTest()
        {
            var desks = _service.GetAllDesks(_token);

            Console.WriteLine(desks.Count);

            Assert.AreNotEqual(Array.Empty<ProjectModel>(), desks);
        }

        [TestMethod()]
        public void GetDeskByIdTest()
        {
            var desk = _service.GetDeskById(_token, 3);
            Assert.AreNotEqual(null, desk);
        }

        [TestMethod()]
        public void GetDesksByProjectTest()
        {
            var desks = _service.GetDesksByProject(_token, 5);
            Assert.AreEqual(2, desks.Count);
        }

        [TestMethod()]
        public void CreateDeskTest()
        {
            var desk = new DeskModel("Доска из тестов", "Обычная доска для тестирования сервисов", true, new string[] { "Новые", "Готовые" });
            desk.ProjectId = 3;
            desk.AdminId = 1;

            var result = _service.CreateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateDeskTest()
        {
            var desk = new DeskModel("Доска из тестов", "Обычная доска для тестирования сервисов", true, new string[] { "Новые", "На проверке", "Готовые" });
            desk.ProjectId = 3;
            desk.AdminId = 1;
            desk.Id = 5;

            var result = _service.UpdateDesk(_token, desk);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteDeskByIdTest()
        {
            var result = _service.DeleteDesk(_token, 5);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}