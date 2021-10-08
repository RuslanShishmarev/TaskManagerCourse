using TaskManagerCourse.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services.Tests
{
    [TestClass()]
    public class TasksRequestServiceTests
    {
        private AuthToken _token;
        private TasksRequestService _service;

        public TasksRequestServiceTests()
        {
            _token = new UsersRequestService().GetToken("admin", "qwerty123");
            _service = new TasksRequestService();
        }

        [TestMethod()]
        public void GetAllTasksTest()
        {
            var tasks = _service.GetAllTasks(_token);

            Console.WriteLine(tasks.Count);

            Assert.AreNotEqual(0, tasks.Count);
        }

        [TestMethod()]
        public void GetTaskByIdTest()
        {
            var task = _service.GetTaskById(_token, 1);
            Assert.AreNotEqual(null, task);
        }

        [TestMethod()]
        public void GetTasksByDeskTest()
        {
            var tasks = _service.GetTasksByDesk(_token, 1);
            Assert.AreNotEqual(0, tasks.Count);
        }

        [TestMethod()]
        public void CreateTaskTest()
        {
            var task = new TaskModel("Задача 1", "Задача из тестов", DateTime.Now, DateTime.Now, "New");
            task.DeskId = 3;
            task.ExecutorId = 1;
            var result = _service.CreateTask(_token, task);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void UpdateTaskTest()
        {
            var task = new TaskModel("Задача 1", "Задача из тестов", DateTime.Now, DateTime.Now, "In Progress");
            task.Id = 3;
            task.ExecutorId = 2;
            var result = _service.UpdateTask(_token, task);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

        [TestMethod()]
        public void DeleteTaskByIdTest()
        {
            var result = _service.DeleteTaskById(_token, 3);
            Assert.AreEqual(HttpStatusCode.OK, result);
        }
    }
}