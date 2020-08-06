using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using BSA_EF.BLL.Interfaces;
using System.Threading.Tasks;

namespace BSA_EF.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LINQController : ControllerBase
    {
        ILINQService services; 
        public LINQController(ILINQService services)
        {
            this.services = services;
        }
        // GET api/LINQ/CountOfUserTasksInProject/{id:int}
        [HttpGet("CountOfUserTasksInProject/{id:int}")]
        public async Task<ActionResult> GetTask1(int id)
        {
            try
            {
                var result = await services.GetCountOfUserTasksInProject(id);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/LINQ/TasksByUserIdWhereNameLess45/{id:int}
        [HttpGet("TasksByUserIdWhereNameLess45/{id:int}")]
        public async Task<ActionResult> GetTask2(int id)
        {
            try
            {
                var result = await services.GetTasksByUserIdWhereNameLess45(id);
                if (result.Any())
                    return Ok(result);
                else return NotFound("Not found any tasks where name less 45");
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/LINQ/FinishedTasksInThisYearByUserId/{id:int}
        [HttpGet("FinishedTasksInThisYearByUserId/{id:int}")]
        public async Task<ActionResult> GetTask3(int id)
        {
            try
            {
                var result = await services.GetFinishedTasksInThisYearByUserId(id);
                if (result.Any())
                    return Ok(result);
                else return NotFound("Not found any finished tasks in this year");
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/LINQ/TeamsMembersOverTenYearsOld
        [HttpGet("TeamsMembersOverTenYearsOld")]
        public async Task<ActionResult> GetTask4()
        {
            var result = await services.GetTeamsMembersOverTenYearsOld();
            return Ok(result);
        }

        // GET api/LINQ/SortedUsersWithSortedTasks
        [HttpGet("SortedUsersWithSortedTasks")]
        public async Task<ActionResult> GetTask5()
        {
            var result = await services.GetSortedUsersWithSortedTasks();
            return Ok(result);
        }

        // GET api/LINQ/UserInfoById/{id:int}
        [HttpGet("UserInfoById/{id:int}")]
        public async Task<ActionResult> GetTask6(int id)
        {
            try
            {
                var result = await services.GetUserInfoById(id);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET api/LINQ/ProjectsInfo
        [HttpGet("ProjectsInfo")]
        public async Task<ActionResult> GetTask7()
        {
            var result = await services.GetProjectsInfo();
            return Ok(result);
        }


        // GET api/LINQ/GetUserUnfinishedTasksById
        [HttpGet("GetUserUnfinishedTasks/{id:int}")]
        public async Task<ActionResult> GetTask8(int id)
        {
            try
            {
                var result = await services.GetUserUnfinishedTasksById(id);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
