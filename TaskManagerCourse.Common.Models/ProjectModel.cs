using System.Collections.Generic;

namespace TaskManagerCourse.Common.Models
{
    public class ProjectModel : CommonModel
    {
        public ProjectStatus Status { get; set; }
        public int? AdminId { get; set; }
        public List<int> AllUsersIds { get; set; }
        public List<int> AllDesksIds { get; set; }
        
        public ProjectModel() { }
        public ProjectModel(string name, string description, ProjectStatus status) 
        {
            Name = name;
            Description = description;
            Status = status;
        }
    }
}
