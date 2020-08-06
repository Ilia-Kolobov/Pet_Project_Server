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
    public class UsersController : ControllerBase
    {
        private IUsersService service;
        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        // GET api/Users
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await service.GetUsers());
        }

        // GET api/Users/id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsers(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try 
            {
                var response = await service.GetUser(id);
                if (response == null)
                    throw new ArgumentException("User whis same id not found");
                return Ok(response); 
            }
            catch(ArgumentException e) 
            { 
                return NotFound(e.Message); 
            }
            
        }

        // POST api/Users
        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PostUser(user);
                return Created("api/User", user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
                //return NotFound(e.Message);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Entity with the same Id already exists. Please try again with another Id");
            }
        }

        // DELETE api/Users/{id}   
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            try
            {
                await service.DeleteUser(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound("Entity with this Id hasn't already exists. Please try again with another Id");
            }
        }

        // PUT api/Users
        [HttpPut()]
        public async Task<ActionResult> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            try
            {
                await service.PutUser(user);
                return Ok(user);
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
