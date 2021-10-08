using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Api.Models.Abstractions;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models.Services
{
    public class ProjectsService : AbstractionService, ICommonService<ProjectModel>
    {
        private readonly ApplicationContext _db;

        public ProjectsService(ApplicationContext db)
        {
            _db = db;
        }
        public bool Create(ProjectModel model)
        {
            bool result = DoAction(delegate () 
            {                
                Project newProject = new Project(model);
                _db.Projects.Add(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
                _db.Projects.Remove(newProject);
                _db.SaveChanges();
            });
            return result;
        }


        public bool Update(int id, ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project project = _db.Projects.FirstOrDefault(p => p.Id == id);
                project.Name = model.Name;
                project.Description = model.Description;
                project.Photo = model.Photo;
                project.Status = model.Status;
                //project.AdminId = model.AdminId;
                _db.Projects.Update(project);
                _db.SaveChanges();
            });
            return result;
        }

        public ProjectModel Get(int id)
        {
            Project project = _db.Projects.Include(p => p.AllUsers).Include(p => p.AllDesks).FirstOrDefault(p => p.Id == id);
            
            var projectModel = project?.ToDto();
            if (projectModel != null)
            {
                projectModel.AllUsersIds = project.AllUsers.Select(u => u.Id).ToList();
                projectModel.AllDesksIds = project.AllDesks.Select(u => u.Id).ToList();
            }

            return projectModel;
        }

        public async Task<IEnumerable<ProjectModel>> GetByUserId(int userId)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == userId);
            if(admin != null)
            {
                var projectsForAdmin = await _db.Projects.Where(p => p.AdminId == admin.Id).Select(p => p.ToDto()).ToListAsync();
                result.AddRange(projectsForAdmin);
            }
            var projectsForUser = await _db.Projects.Include(p => p.AllUsers).Where(p => p.AllUsers.Any(u => u.Id == userId)).Select(p => p.ToDto()).ToListAsync();
            result.AddRange(projectsForUser);
            return result;
        }
        public IQueryable<CommonModel> GetAll()
        {
            return _db.Projects.Select(p => p.ToDto() as CommonModel);
        }

        public void AddUsersToProject(int id, List<int> userIds)
        {
            Project project = _db.Projects.FirstOrDefault(p => p.Id == id);
            foreach(int userId in userIds)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if(project.AllUsers.Contains(user) == false)
                    project.AllUsers.Add(user);
            }
            _db.SaveChanges();
        }

        public void RemoveUsersFromProject(int id, List<int> userIds)
        {
            Project project = _db.Projects.Include(p => p.AllUsers).FirstOrDefault(p => p.Id == id);
            foreach (int userId in userIds)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if(project.AllUsers.Contains(user))
                    project.AllUsers.Remove(user);
            }
            _db.SaveChanges();
        }
    }
}
