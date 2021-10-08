using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Api.Models.Services;
using TaskManagerCourse.Common.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesksController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly DesksService _desksService;

        public DesksController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _desksService = new DesksService(db);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonModel>>> GetDesksForCurrentUser()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if(user != null)
            {
                var result = await _desksService.GetAll(user.Id).ToListAsync();
                return result == null ? NoContent() : Ok(result);
            }
            return Unauthorized();            
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var desk = _desksService.Get(id);
            return desk == null ? NotFound() : Ok(desk);
        }
        
        [HttpGet("project")]
        public async Task<ActionResult<IEnumerable<CommonModel>>> GetProjectDesks(int projectId)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                var result = await _desksService.GetProjectDesks(projectId, user.Id).ToListAsync();
                return result == null ? NoContent() : Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult Create([FromBody] DeskModel deskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if(user != null)
            {
                if(deskModel != null)
                {
                    deskModel.AdminId = user.Id;
                    bool result = _desksService.Create(deskModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest();
            }
            return Unauthorized();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] DeskModel deskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (deskModel != null)
                {
                    bool result = _desksService.Update(id, deskModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool result = _desksService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
