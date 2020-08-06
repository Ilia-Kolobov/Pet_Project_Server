using BSA_EF.DAL.Models;
using Common.DTOs.LinqDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Interfaces
{
    public interface ILINQService
    {
        public Task<List<ProjectCountDTO>> GetCountOfUserTasksInProject(int id);
        public Task<List<ProjectTask>> GetTasksByUserIdWhereNameLess45(int id);
        public Task<List<TaskIdNameDTO>> GetFinishedTasksInThisYearByUserId(int id);
        public Task<List<TeamsUsersDTO>> GetTeamsMembersOverTenYearsOld();
        public Task<List<UsersTasksDTO>> GetSortedUsersWithSortedTasks();
        public Task<UserInfoDTO> GetUserInfoById(int id);
        public Task<List<ProjectInfoDTO>> GetProjectsInfo();
        public Task<UsersTasksDTO> GetUserUnfinishedTasksById(int id);
    }
}
