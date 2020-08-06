using Microsoft.AspNetCore.Mvc;
using BSA_EF.DAL.Models;
using System;
using Microsoft.EntityFrameworkCore;
using BSA_EF.BLL.Interfaces;
using System.Threading.Tasks;

namespace BSA_EF.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTasksController : ControllerBase
    {
        private IProjectTasksService service;
        public ProjectTasksController(IProjectTasksService service)
        {
            this.service = service;
        }

        // GET api/ProjectTasks
        [HttpGet]
        public async Task<ActionResult> GetProjectTasks()
        {
            return Ok(await service.GetProjectsTasks());
        }

        // GET api/ProjectTasks/id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProjectTask(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                var response = await service.GetProjectTask(id);
                if (response == null)
                    throw new ArgumentException("Task whis same id not found");
                return Ok(response);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

        }


        // POST api/ProjectTasks
        [HttpPost]
        public async Task<ActionResult> Post(ProjectTask task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PostProjectTask(task);
                return Created("api/ProjectTasks", task);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Entity with the same Id already exists. Please try again with another Id");
            }
        }

        // DELETE api/ProjectTasks/{id}   
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                await service.DeleteProjectTask(id);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound("Entity with this Id hasn't already exists. Please try again with another Id");
            }
        }

        // PUT api/ProjectTasks
        [HttpPut()]
        public async Task<ActionResult> Put(ProjectTask task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PutProjectTask(task);
                return Ok(task);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Entity with this Id hasn't already exists. Please try again with another Id");
            }
        }

    }
}
