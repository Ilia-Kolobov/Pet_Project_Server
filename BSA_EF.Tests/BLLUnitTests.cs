using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSA_EF.BLL.Services;
using BSA_EF.DAL;
using Microsoft.EntityFrameworkCore;
using BSA_EF.WebAPI;
using BSA_EF.DAL.Repositories;
using System.Linq;
using BSA_EF.DAL.Interfaces;
using FakeItEasy;
using BSA_EF.DAL.Models;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Tests
{
    public class BLLUnitTests
    {
        private readonly LINQService _service;

        private readonly FakeSqlServerDbContext _context;

        public BLLUnitTests()
        {
            _context = new FakeSqlServerDbContext(new DbContextOptionsBuilder<FakeSqlServerDbContext>()
            .UseInMemoryDatabase("LINQTestDatabase")
            .Options);
            _context.Database.EnsureCreated();
            this._service = new LINQService(new FakeUnitOfWork(_context));
        }

        // GetCountOfUserTasksInProject

        [Fact]
        public async Task GetCountOfUserTasksInProject_WhenUserExistById7_ThenProjectId4AndCountTask4()
        {
            var result = await _service.GetCountOfUserTasksInProject(7);
            Assert.Single(result);
            Assert.Equal(4, result[0].Key.Id);
            Assert.Equal(4, result[0].Value);
        }

        [Fact]
        public async Task GetCountOfUserTasksInProject_WhenUserNotExistById11_ThenThrowArgumentException()
        {
           await  Assert.ThrowsAsync<ArgumentException>(() =>  _service.GetCountOfUserTasksInProject(11));
        }

        [Fact]
        public async Task GetCountOfUserTasksInProject_WhenUserHasNotTasksById2_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetCountOfUserTasksInProject(2));
        }

        // GetTasksByUserIdWhereNameLess45

        [Fact]
        public async Task GetTasksByUserIdWhereNameLess45_WhenUserExistAndHasTasksById10_ThenGet2Tasks()
        {
            var result = await _service.GetTasksByUserIdWhereNameLess45(10);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(13, result[1].Id);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetTasksByUserIdWhereNameLess45_WhenUserNotExistById11_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>  _service.GetTasksByUserIdWhereNameLess45(11));
        }

        [Fact]
        public async Task GetTasksByUserIdWhereNameLess45_WhenUserHasNotTasksById2_ThenThrowArgumentException()
        {
           await Assert.ThrowsAsync<ArgumentException>(() => _service.GetTasksByUserIdWhereNameLess45(2));
        }

        // GetFinishedTasksInThisYearByUserId

        [Fact]
        public async Task GetFinishedTasksInThisYearByUserId_WhenUserExistAndHasTasksById1_ThenGet3Tasks()
        {
            var result = await _service.GetFinishedTasksInThisYearByUserId(1);
            Assert.Equal(3, result.Count);
            Assert.Equal(3, result[0].Id);
            Assert.Equal(9, result[1].Id);
            Assert.Equal(11, result[2].Id);
        }

        [Fact]
        public async Task GetFinishedTasksInThisYearByUserId_WhenUserNotExistById11_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetFinishedTasksInThisYearByUserId(11));
        }

        [Fact]
        public async Task GetFinishedTasksInThisYearByUserId_WhenUserHasNotTasksById2_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetFinishedTasksInThisYearByUserId(2));
        }

        // GetTeamsMembersOverTenYearsOld

        [Fact]
        public async Task GetTeamsMembersOverTenYearsOld_WhenUsersExist_ThenGet4Teams()
        {
            var result = await _service.GetTeamsMembersOverTenYearsOld();
            Assert.Equal(4, result.Count);
            Assert.Equal(10, result[3].Users[1].Id);
        }

        // GetSortedUsersWithSortedTasks

        [Fact]
        public async Task GetSortedUsersWithSortedTasks_WhenUsersExistAndHaveTasks_ThenGetSortedUsersAndTasks()
        {
            var result = await _service.GetSortedUsersWithSortedTasks();
            Assert.Equal(10, result.Count);

            var firstUser = result[0].User;
            var firstUserTasks = result[0].Tasks.ToList();

            var lastUser = result[9].User;
            var lastUserTasks = result[9].Tasks.ToList();

            Assert.Equal(10, firstUser.Id);

            Assert.Equal(14, firstUserTasks[0].Id);
            Assert.Equal(1, firstUserTasks[1].Id);
            Assert.Equal(13, firstUserTasks[2].Id);

            Assert.Equal(4, lastUser.Id);

            Assert.Equal(4, lastUserTasks[0].Id);
        }

        // GetUserInfoById

        [Fact]
        public async Task GetUserInfoById_WhenUserExistAndHasTasksById1_ThenGetInfo()
        {
            var result = await _service.GetUserInfoById(1);
            Assert.Equal(1, result.User.Id);
            Assert.Equal(7, result.LastProject.Id);
            Assert.Equal(1, result.TasksCount);
            Assert.Equal(0, result.UnfinishedAndCanceledTasksCount);
            Assert.Equal(11, result.LongestTask.Id);
        }

        [Fact]
        public async Task GetUserInfoById_WhenUserNotExistById11_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetFinishedTasksInThisYearByUserId(11));
        }

        [Fact]
        public async Task GetUserInfoById_WhenUserHasNotTasksAndProjectsById2_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetFinishedTasksInThisYearByUserId(2));
        }

        // GetProjectsInfo 

        [Fact]
        public async Task GetProjectsInfo_WhenProjectsExist_ThenGetInfo()
        {
            var result = await _service.GetProjectsInfo();


            Assert.Equal(6, result.Count());
            Assert.Equal(4, result[3].Project.Id);
            Assert.Equal(6, result[3].LongestTaskByDicription.Id);
            Assert.Equal(13, result[3].LessTaskByName.Id);
            Assert.Equal(2, result[3].UserCounWhereDescriptionMore20orTaskCounLess3);
        }

        // AddUser

        [Fact]
        public async Task AddUser_WhenUserNotExists_ThenCallCreateMethod()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var newUser = A.Fake<User>();
            var service = new UsersService(unitOfWork);
            service.PostUser(newUser);
            A.CallTo(() => unitOfWork.Users.CreateAsync(newUser)).MustHaveHappened();
        }

        [Fact]
        public async Task AddUser_WhenUserWithSameIdExists_ThenThrowArgumentException()
        {
            var unitOfWork = new UnitOfWork(_context);
            var newUser = new User { Id = 1 };
            var service = new UsersService(unitOfWork);
            await Assert.ThrowsAsync<ArgumentException>(() => service.PostUser(newUser));
        }

        // СhangeStateOfTask

        [Fact]
        public async Task СhangeStateOfTask_WhenTaskExists_ThenCallUpdateMethod()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var newTask = A.Fake<ProjectTask>();
            var service = new ProjectTasksService(unitOfWork);
            await service.PutProjectTask(newTask);
            A.CallTo(() => unitOfWork.ProjectTasks.UpdateAsync(newTask)).MustHaveHappened();
        }

        [Fact]
        public async Task СhangeStateOfTask_WhenTaskNotExists_ThenDbUpdateConcurrencyException()
        {
            var unitOfWork = new UnitOfWork(_context);
            var newTask = new ProjectTask { Id = 100, Name = "wewad" };
            var service = new ProjectTasksService(unitOfWork);
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => service.PutProjectTask(newTask));
        }

        // AddUserToTeam

        [Fact]
        public async Task AddUserToTeam_WhenUserExists_ThenCallUpdateMethod()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var newUser = A.Fake<User>();
            var service = new UsersService(unitOfWork);
            service.PutUser(newUser);
            A.CallTo(() => unitOfWork.Users.UpdateAsync(newUser)).MustHaveHappened();
        }

        [Fact]
        public async Task AddUserToTeam_WhenUserNotExists_ThenDbUpdateConcurrencyException()
        {
            var unitOfWork = new UnitOfWork(_context);
            var newUser = new User { Id = 100 };
            var service = new UsersService(unitOfWork);
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => service.PutUser(newUser));
        }

        // GetUserUnfinishedTasksById

        [Fact]
        public async Task GetUserUnfinishedTasksById_WhenUserExistAndHasTasksById8_ThenGetUserUnfinishedTasksById()
        {
            var result = await _service.GetUserUnfinishedTasksById(8);
            var userTasks = result.Tasks.ToList();

            Assert.Equal(8, result.User.Id);

            Assert.Equal(2, userTasks.Count());
            Assert.Equal(8, userTasks[0].Id);
            Assert.Equal(12, userTasks[1].Id);
        }

        [Fact]
        public async Task GetUserUnfinishedTasksById_WhenUserNotExistAndHasNotTasksById2_ThenThrowArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetUserUnfinishedTasksById(2));
        }
    }
}
