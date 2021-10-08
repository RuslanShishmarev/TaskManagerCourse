using System.Collections.Generic;

namespace TaskManagerCourse.Common.Models
{
    public class DeskModel : CommonModel
    {
        public bool IsPrivate { get; set; }
        public string[] Columns { get; set; }
        public int ProjectId { get; set; }
        public int AdminId { get; set; }
        public List<int> TasksIds { get; set; }

        public DeskModel() { }
        public DeskModel(string name, string description, bool isPrivate, string[] columns) 
        {
            Name = name;
            Description = description;
            IsPrivate = isPrivate;
            Columns = columns;
        }
    }
}
