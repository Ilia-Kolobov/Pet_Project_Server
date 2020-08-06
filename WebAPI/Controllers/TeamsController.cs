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
    public class TeamsController : ControllerBase
    {
        private ITeamsService service;
        public TeamsController(ITeamsService service)
        {
            this.service = service;
        }

        // GET api/Teams
        [HttpGet]
        public async Task<ActionResult> GetTeams()
        {
            return Ok( await service.GetTeams());
        }

        // GET api/Teams/id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeam(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                var response = await service.GetTeam(id);
                if (response == null)
                    throw new ArgumentException("Team whis same id not found");
                return Ok(response);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }

        }


        // POST api/Teams
        [HttpPost]
        public async Task<ActionResult> Post(Team team)
        {
            if(team == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PostTeam(team);
                return Created("api/Teams", team);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
                //return NotFound(e.Message);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Entity with the same Id already exists. Please try again with another Id");
            }
        }

        // DELETE api/Teams/{id}   
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                await service.DeleteTeam(id);
                return NoContent();
            }
            catch(ArgumentException e)
            {
                return NotFound("Entity with this Id hasn't already exists. Please try again with another Id");
            }
        }

        // PUT api/Teams
        [HttpPut()]
        public async Task<ActionResult> Put(Team team)
        {
            if (team == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PutTeam(team);
                return Ok(team);
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
