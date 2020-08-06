using System;
using Microsoft.AspNetCore.Mvc;
using BSA_EF.DAL.Models;
using Microsoft.EntityFrameworkCore;
using BSA_EF.BLL.Interfaces;
using System.Threading.Tasks;

namespace BSA_EF.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectsService service;

        public ProjectsController(IProjectsService service)
        {
            this.service = service;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult> GetProjects()
        {
            return Ok( await service.GetProjects());
        }


        //GET: api/Projects/id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                var response = await service.GetProject(id);
                if (response == null)
                    throw new ArgumentException("Project whis same id not found");
                return Ok(response);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

        }

        //POST: api/Projects
        [HttpPost("")]
        public async Task<ActionResult> PostProject(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PostProject(project);
                return Created("api/Projects", project);
            }
            catch (ArgumentException e)
            {
                // return NotFound(e.Message);
                return BadRequest(e.Message);
            }
            catch (DbUpdateException) 
            {
                
                return BadRequest("Entity with the same Id already exists. Please try again with another Id");
            }
        }

        //PUT: api/Projects
        [HttpPut("")]
        public async Task<ActionResult> PutProject(Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PutProject(project);
                return Ok(project);
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

        //DELETE: api/Projects
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                await service.DeleteProject(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound("Entity with this Id hasn't already exists. Please try again with another Id");
            }
        }
    }
}


