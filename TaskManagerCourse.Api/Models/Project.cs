using System.Collections.Generic;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models
{
    public class Project : CommonObject
    {
        public int Id { get; set; }
        public int? AdminId { get; set; }
        public ProjectAdmin Admin { get; set; }
        public List<User> AllUsers { get; set; } = new List<User>();
        public List<Desk> AllDesks { get; set; } = new List<Desk>();
        public ProjectStatus Status { get; set; }
        public Project() { }
        public Project(ProjectModel projectModel) : base(projectModel)
        {
            Id = projectModel.Id;
            AdminId = projectModel.AdminId;
            Status = projectModel.Status;
        }
        public ProjectModel ToDto()
        {
            return new ProjectModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreationDate = this.CreationDate,
                Photo = this.Photo,
                AdminId = this.AdminId,
                Status = this.Status,
            };
        }
    }
}
