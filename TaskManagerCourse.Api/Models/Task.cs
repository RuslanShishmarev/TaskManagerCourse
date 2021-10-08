using System;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models
{
    public class Task : CommonObject
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] File { get; set; }
        public int DeskId { get; set; }
        public Desk Desk { get; set; }
        public string Column { get; set; }
        public int? CreatorId { get; set; }
        public User Creator { get; set; }
        public int? ExecutorId { get; set; }

        public Task() { }
        public Task(TaskModel taskModel) : base(taskModel)
        {
            Id = taskModel.Id;
            StartDate = taskModel.CreationDate;
            EndDate = taskModel.EndDate;
            File = taskModel.File;
            DeskId = taskModel.DeskId;
            Column = taskModel.Column;
            CreatorId = taskModel.CreatorId;
            ExecutorId = taskModel.ExecutorId;
        }

        public TaskModel ToDto()
        {
            return new TaskModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreationDate = this.CreationDate,
                Photo = this.Photo,
                StartDate = this.CreationDate,
                EndDate = this.EndDate,
                File = this.File,
                DeskId = this.DeskId,
                Column = this.Column,
                CreatorId = this.CreatorId,
                ExecutorId = this.ExecutorId
            };
        }

        public TaskModel ToShortDto()
        {
            return new TaskModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreationDate = this.CreationDate,
                StartDate = this.CreationDate,
                EndDate = this.EndDate,
                DeskId = this.DeskId,
                Column = this.Column,
                CreatorId = this.CreatorId,
                ExecutorId = this.ExecutorId
            };
        }
    }
}
