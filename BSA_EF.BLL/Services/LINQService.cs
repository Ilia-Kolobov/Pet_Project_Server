using System;
using System.Linq;
using System.Collections.Generic;
using Common.DTOs.LinqDTOs;
using BSA_EF.DAL.Models;
using BSA_EF.DAL.Interfaces;
using AutoMapper;
using Common.DTOs;
using BSA_EF.BLL.Interfaces;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Services
{
    public class LINQService : ILINQService
    {
        private IUnitOfWork unitOfWork;
        private Mapper mapperUser;
        private Mapper mapperTeam;
        private Mapper mapperProject;
        private Mapper mapperProjectTask;

        public LINQService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapperTeam = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Team,TeamDTO>()));
            this.mapperProject = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()));
            this.mapperProjectTask = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProjectTask, ProjectTaskDTO>()));
            this.mapperUser = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()));
        }
        public async Task<List<ProjectCountDTO>> GetCountOfUserTasksInProject(int id)
        {
            var projects = await unitOfWork.Projects.GetAsync();
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();

            if (projects.Any(p => p.AuthorId == id))
            {
                return projects.Where(p => p.AuthorId == id).Select(p => new ProjectCountDTO
                {
                    Key = mapperProject.Map<ProjectDTO>(p),
                    Value = projectTasks.Where(t => t.ProjectId == p.Id).Count()
                }
                ).ToList();
            }
            throw new ArgumentException("Not found projects whis same AuthorId");
        }
        public async Task<List<ProjectTask>> GetTasksByUserIdWhereNameLess45(int id)
        {
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            if (users.Any(u => u.Id == id))
            {
                if (projectTasks.Any(t => t.PerformerId == id))
                    return projectTasks.Where(t => t.PerformerId == id && t.Name.Length < 45).ToList();
                throw new ArgumentException("Not found any tasks for user");
            }
            throw new ArgumentException("Not found user");
        }
        public async Task<List<TaskIdNameDTO>> GetFinishedTasksInThisYearByUserId(int id)
        {
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            if (users.Any(u => u.Id == id))
            {
                if (projectTasks.Any(t => t.PerformerId == id))
                {
                    return projectTasks
                    .Where(t => t.PerformerId == id
                        && t.State == (int)TaskStates.Finished
                        && t.FinishedAt.Year == DateTime.Now.Year)
                    .Select(t => new TaskIdNameDTO { Id = t.Id, Name = t.Name }).ToList();
                }
                throw new ArgumentException("Not found any tasks for user");
            }
            throw new ArgumentException("Not found user");
        }
        public async Task<List<TeamsUsersDTO>> GetTeamsMembersOverTenYearsOld()
        {
            var teams = await unitOfWork.Teams.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            return teams
                .GroupJoin(users
                .Where(x => DateTime.Now.Year - x.Birthday.Year > 10)
                .OrderByDescending(x => x.RegisteredAt),
                 t => t.Id,
                 x => x.TeamId,
                 (team, user) => new TeamsUsersDTO { Team = mapperTeam.Map<TeamDTO>(team), Users = mapperUser.Map<List<UserDTO>>(user.Select(u => u).ToList())})
                .Where(x => x.Users.Any()).ToList();
        }
        public async Task<List<UsersTasksDTO>> GetSortedUsersWithSortedTasks()
        {
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            return users.GroupJoin(
                projectTasks
                .OrderByDescending(x => x.Name.Length),
                u => u.Id,
                t => t.PerformerId,
                (user, tasks) => new UsersTasksDTO { User = mapperUser.Map<UserDTO>(user), Tasks = mapperProjectTask.Map<IEnumerable<ProjectTaskDTO>>(tasks) })
                .OrderBy(x => x.User.FirstName).ToList();
        }
        public async Task<UserInfoDTO> GetUserInfoById(int id)
        {
            var projects = await unitOfWork.Projects.GetAsync();
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            if (users.Any(u => u.Id == id))
            {
                if (projects.Any(p => p.AuthorId == id)
                    && projectTasks.Any(p => p.PerformerId == id)
                    && users.First(u=> u.Id ==id).TeamId != null
                )
                {
                    return users.Select(user => new UserInfoDTO
                    {
                        User = mapperUser.Map<UserDTO>(user),

                        LastProject = mapperProject.Map<ProjectDTO>(projects
                        .Where(p => p.AuthorId == user.Id)
                        .FirstOrDefault(x => x.CreatedAt == projects
                            .Where(p => p.AuthorId == user.Id)
                            .Max(x => x.CreatedAt))),

                        TasksCount = projectTasks.Where(x => x.ProjectId == projects
                        .Where(p => p.AuthorId == user.Id)
                        .FirstOrDefault(x => x.CreatedAt == projects
                            .Where(p => p.AuthorId == user.Id)
                            .Max(x => x.CreatedAt))?.Id).Count(),

                        UnfinishedAndCanceledTasksCount = projectTasks.Where(x => x.PerformerId == user.Id
                        && (x.State == (int)TaskStates.Started || x.State == (int)TaskStates.Created || x.State == (int)TaskStates.Canceled)).ToList().Count(),
                        LongestTask = mapperProjectTask.Map<ProjectTaskDTO>(projectTasks.Where(x => x.PerformerId == user.Id)
                           .First(x => x.FinishedAt - x.CreatedAt ==
                           projectTasks
                           .Where(x => x.PerformerId == user.Id).Max(x => x.FinishedAt - x.CreatedAt)))

                    }).First(x => x.User.Id == id);
                }
                throw new ArgumentException("Not found projects and tasks");
            }
            throw new ArgumentException("Not found user");
        }
        public async Task<List<ProjectInfoDTO>> GetProjectsInfo()
        {
            var projects = await unitOfWork.Projects.GetAsync();
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            return projects.Where(x => projectTasks.Where(t => t.ProjectId == x.Id).Any()).Select(proj => new ProjectInfoDTO
            {
                Project = mapperProject.Map<ProjectDTO>( proj),

                LongestTaskByDicription = mapperProjectTask.Map<ProjectTaskDTO>(projectTasks.Where(x => x.ProjectId == proj.Id)
                          .First(x => x.Description.Length == projectTasks.Where(x => x.ProjectId == proj.Id)
                          .Max(x => x.Description.Length))),

                LessTaskByName = mapperProjectTask.Map<ProjectTaskDTO>(projectTasks.Where(x => x.ProjectId == proj.Id)
                          .First(x => x.Name.Length == projectTasks.Where(x => x.ProjectId == proj.Id)
                          .Min(x => x.Name.Length))),

                UserCounWhereDescriptionMore20orTaskCounLess3 = users
                .Where(u => u.TeamId == projects.Where(x => x.Description.Length > 20 ||
                projectTasks
                .Where(t => t.ProjectId == x.Id).Count() < 3).FirstOrDefault(x => x.Id == proj.Id).Id).Count()


            }).ToList();


        }
        public async Task<UsersTasksDTO> GetUserUnfinishedTasksById(int id)
        {
            var projectTasks = await unitOfWork.ProjectTasks.GetAsync();
            var users = await unitOfWork.Users.GetAsync();

            if (users.Any(u => u.Id == id))
            {
                if (projectTasks.Any(t => t.PerformerId == id))
                {
                    return new UsersTasksDTO
                    {
                        User = mapperUser.Map<UserDTO>(users.First(u => u.Id == id)),
                        Tasks = mapperProjectTask.Map<List<ProjectTaskDTO>>(projectTasks
                        .Where(x => x.PerformerId == id && x.State != (int)TaskStates.Finished).ToList())
                    };
                }
                throw new ArgumentException("Not found any tasks for user");
            }
            throw new ArgumentException("Not found user");
        }
    }
}
