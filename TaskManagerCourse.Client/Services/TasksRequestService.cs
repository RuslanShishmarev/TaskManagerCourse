using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services
{
    public class TasksRequestService : CommonRequestService
    {
        private string _tasksControllerUrl = HOST + "tasks";

        public List<TaskModel> GetAllTasks(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _tasksControllerUrl + "/user", token);
            List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(response);
            return tasks;
        }

        public TaskModel GetTaskById(AuthToken token, int taskId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _tasksControllerUrl + $"/{taskId}", token);
            TaskModel task = JsonConvert.DeserializeObject<TaskModel>(response);
            return task;
        }

        public List<TaskModel> GetTasksByDesk(AuthToken token, int deskId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("deskId", deskId.ToString());
            string response = GetDataByUrl(HttpMethod.Get, _tasksControllerUrl, token, null, null, parameters);
            List<TaskModel> tasks = JsonConvert.DeserializeObject<List<TaskModel>>(response);
            return tasks;
        }

        public HttpStatusCode CreateTask(AuthToken token, TaskModel task)
        {
            string taskJson = JsonConvert.SerializeObject(task);
            var result = SendDataByUrl(HttpMethod.Post, _tasksControllerUrl, token, taskJson);
            return result;
        }

        public HttpStatusCode UpdateTask(AuthToken token, TaskModel task)
        {
            string taskJson = JsonConvert.SerializeObject(task);
            var result = SendDataByUrl(HttpMethod.Patch, _tasksControllerUrl + $"/{task.Id}", token, taskJson);
            return result;
        }

        public HttpStatusCode DeleteTaskById(AuthToken token, int taskId)
        {
            var result = DeleteDataByUrl(_tasksControllerUrl + $"/{taskId}", token);
            return result;
        }
    }
}
