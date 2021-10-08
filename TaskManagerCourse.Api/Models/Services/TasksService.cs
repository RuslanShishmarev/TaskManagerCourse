using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerCourse.Api.Models.Abstractions;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models.Services
{
    public class TasksService : AbstractionService, ICommonService<TaskModel>
    {
        private readonly ApplicationContext _db;

        public TasksService(ApplicationContext db)
        {
            _db = db;
        }
        public bool Create(TaskModel model)
        {
            bool result = DoAction(delegate ()
            {
                Task newTask = new Task(model);
                _db.Tasks.Add(newTask);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            });
            return result;
        }

        public TaskModel Get(int id)
        {
            Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            return task?.ToDto();
        }

        public IQueryable<TaskModel> GetTasksForUser(int userId)
        {
            return _db.Tasks.Where(t => t.CreatorId == userId || t.ExecutorId == userId).Select(t => t.ToShortDto());
        }

        public bool Update(int id, TaskModel model)
        {
            bool result = DoAction(delegate ()
            {
                Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);

                task.Name = model.Name;
                task.Description = model.Description;
                task.Photo = model.Photo;
                task.StartDate = model.CreationDate;
                task.EndDate = model.EndDate;
                task.File = model.File;
                task.Column = model.Column;
                task.ExecutorId = model.ExecutorId;

                _db.Tasks.Update(task);
                _db.SaveChanges();
            });
            return result;
        }

        public IQueryable<TaskModel> GetAll(int deskId)
        {
            return _db.Tasks.Where(t => t.DeskId == deskId).Select(t => t.ToShortDto());
        }
    }
}
