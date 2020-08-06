
using BSA_EF.DAL.Models;
using Common.DTOs;
using Common.DTOs.LinqDTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BSA_EF.WebAPI.Tests
{
    public class WebAPIIntegrationTests:IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly string path = "https://localhost:44339";
        public WebAPIIntegrationTests(CustomWebApplicationFactory<Startup> factory) 
        {
            _client = factory.CreateClient();
        }

        // CreateProject

        [Fact]
        public async Task CreateProject_WhenHasNotSameId_ThenResponeStatusCode201()
        {
            var project = new ProjectDTO { Id = 11, AuthorId = 3, Description = "hjkhkjh", CreatedAt = DateTime.Now, Name = "BIba", TeamId = 3, Deadline = new DateTime(2020, 09, 23)};
            string json = JsonConvert.SerializeObject(project);
            var httpResponse = await _client.PostAsync(path+"/api/Projects", new StringContent(json, Encoding.UTF8, "application/json"));

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var createdProjects = JsonConvert.DeserializeObject<ProjectDTO>(stringResponse);

            await _client.DeleteAsync(path+"/api/Projects/11");

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.Equal(project.Id,createdProjects.Id);
        }

        [Fact]
        public async Task CreateProject_WhenProjectWhisSameIdExists_ThenResponseStatusCode400()
        {
            var project = new ProjectDTO { Id = 1, AuthorId = 3, Description = "hjkhkjh", CreatedAt = DateTime.Now, Name = "BIba", TeamId = 3, Deadline = new DateTime(2020, 09, 23) };
            string json = JsonConvert.SerializeObject(project);
            var httpResponse = await _client.PostAsync(path+"/api/Projects", new StringContent(json, Encoding.UTF8, "application/json"));


            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("{\"Id\" = -1, \"Name\" = \"TestName\", \"Description\" = \"TestDescription\", \"CreatedAt\" = \"2020.07.20\", \"Deadline\" = \"2020.09.12\", \"AuthorId\" = 3, \"TeamId\" = 3}")]
        [InlineData("{\"Id\" = 100, \"Name\" = \"TestName\", \"Description\" = \"TestDescription\", \"CreatedAt\" = \"2020.07.20\", \"Deadline\" = \"2020.09.12\", \"AuthorId\" = 3}")]
        [InlineData("{\"Id\" = 100, \"Name\" = \"TestName\", \"Description\" = \"TestDescription\", \"CreatedAt\" = \"2020.07.20\", \"Deadline\" = \"2020.09.12\", \"TeamId\" = 3}")]
        [InlineData("{\"Id\" = 100, \"Name\" = \"TestName\", \"Description\" = \"TestDescription\", \"Deadline\" = \"2020.09.12\", \"AuthorId\" = 3, \"TeamId\" = 3}")]
        [InlineData("{\"Id\" = 100, \"Description\" = \"TestDescription\", \"CreatedAt\" = \"2020.07.20\", \"Deadline\" = \"2020.09.12\", \"AuthorId\" = 3, \"TeamId\" = 3}")]
        [InlineData("{\"Name\" = \"TestName\", \"Description\" = \"TestDescription\", \"CreatedAt\" = \"2020.07.20\", \"Deadline\" = \"2020.09.12\", \"AuthorId\" = 3, \"TeamId\" = 3}")]
        [InlineData("")]
        public async Task CreateProject_WhenWrongRequestBody_ThenResponseCode400(string jsonInstring)
        {
            var httpResponse = await _client.PostAsync(path+"/api/Projects", new StringContent(jsonInstring, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        // CreateTeam

        [Fact]
        public async Task CreateTeam_WhenHasNotSameId_ThenResponseStatusCode201()
        {
            var team = new TeamDTO { Id = 6, Name = "TestName", CreatedAt = DateTime.Now };
            string json = JsonConvert.SerializeObject(team);
            var httpResponse = await _client.PostAsync(path+"/api/Teams", new StringContent(json, Encoding.UTF8, "application/json"));

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var createdTeam = JsonConvert.DeserializeObject<TeamDTO>(stringResponse);

            await _client.DeleteAsync(path+"/api/Projects/11");

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.Equal(team.Id, createdTeam.Id);
        }

        [Fact]
        public async Task CreateTeam_WhenTeamWithSameIdExists_ThenResponseCode400()
        {
            var team = new TeamDTO { Id = 1, Name = "TestName", CreatedAt = DateTime.Now };
            string json = JsonConvert.SerializeObject(team);
            var httpResponse = await _client.PostAsync(path+"/api/Teams", new StringContent(json, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Theory]
        [InlineData("{\"Id\" = -1, \"Name\" = \"TestName\", \"CreatedAt\" = \"2020.07.20\", \"Description\" = \"TestDescription\"}")]
        [InlineData("{\"Id\" = -1, \"Name\" = \"TestName\", \"Description\" = \"TestDescription\"}")]
        [InlineData("{\"Id\" = -1, \"CreatedAt\" = \"2020.07.20\", \"Description\" = \"TestDescription\"}")]
        [InlineData("{\"Name\" = \"TestName\", \"CreatedAt\" = \"2020.07.20\", \"Description\" = \"TestDescription\"}")]
        [InlineData("")]
        public async Task CreateTeam_WhenWrongRequestBody_ThenResponseCode400(string jsonInstring)
        {
            var httpResponse = await _client.PostAsync(path+"/api/Teams", new StringContent(jsonInstring, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        // DeleteUser

        [Fact]
        public async Task DeleteUser_WhenUserExistsById_ThenResponseStatusCode204()
        {
            var httpGetResponseBeforeDelete = await _client.GetAsync(path+"/api/Users/1");
            var httpDeleteResponse = await _client.DeleteAsync(path+"/api/Users/1");
            var httpGetResponseAfterDelete = await _client.GetAsync(path+"/api/Users/1");
           
            Assert.Equal(HttpStatusCode.OK, httpGetResponseBeforeDelete.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, httpDeleteResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpGetResponseAfterDelete.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_WhenUserNotExistsById_ThenResponseStatusCode404()
        {
            var httpGetResponse = await _client.GetAsync(path+"/api/Users/100");
            var httpDeleteResponse = await _client.DeleteAsync(path+"/api/Users/100");

            Assert.Equal(HttpStatusCode.NotFound, httpGetResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpDeleteResponse.StatusCode);
        }

        // DeleteTask

        [Fact]
        public async Task DeleteTask_WhenUserExistsById_ThenResponseStatusCode204()
        {
            var httpGetResponseBeforeDelete = await _client.GetAsync(path+"/api/ProjectTasks/1");
            var httpDeleteResponse = await _client.DeleteAsync(path+"/api/ProjectTasks/1");
            var httpGetResponseAfterDelete = await _client.GetAsync(path+"/api/ProjectTasks/1");

            Assert.Equal(HttpStatusCode.OK, httpGetResponseBeforeDelete.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, httpDeleteResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpGetResponseAfterDelete.StatusCode);
        }
        [Fact]
        public async Task DeleteTask_WhenUserNotExistsById_ThenResponseStatusCode404()
        {
            var httpGetResponse = await _client.GetAsync(path+"/api/ProjectTasks/100");
            var httpDeleteResponse = await _client.DeleteAsync(path+"/api/ProjectTasks/100");

            Assert.Equal(HttpStatusCode.NotFound, httpGetResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, httpDeleteResponse.StatusCode);
        }

        // LINQ 

        // GetCountOfUserTasksInProject

        [Fact]
        public async Task GetCountOfUserTasksInProject_WhenUserExistById7_ThenResponseStatusCode200()
        {
            var httpGetResponse = await _client.GetAsync(path+"/api/LINQ/CountOfUserTasksInProject/7");

            var stringResponse = await httpGetResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ProjectCountDTO>>(stringResponse);
            
            Assert.Equal(HttpStatusCode.OK, httpGetResponse.StatusCode);
 
            Assert.Single(result);
            Assert.Equal(4, result[0].Key.Id);
            Assert.Equal(4, result[0].Value);
        }

        [Fact]
        public async Task GetCountOfUserTasksInProject_WhenUserNotExistById11_ThenResponseStatuscode404()
        {
            var httpGetResponse = await _client.GetAsync(path+"/api/LINQ/CountOfUserTasksInProject/11");

            Assert.Equal(HttpStatusCode.NotFound, httpGetResponse.StatusCode);
        }

        // GetUserUnfinishedTasksById

        [Fact]
        public async Task GetUserUnfinishedTasksById_WhenUserExistById8_ThenResponseStatusCode200()
        {
            var httpGetResponse = await _client.GetAsync(path+"/api/LINQ/GetUserUnfinishedTasks/8");

            var stringResponse = await httpGetResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UsersTasksDTO>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpGetResponse.StatusCode);

            var userTasks = result.Tasks.ToList();

            Assert.Equal(8, result.User.Id);

            Assert.Equal(2, userTasks.Count());
            Assert.Equal(8, userTasks[0].Id);
            Assert.Equal(12, userTasks[1].Id);
        }

        [Fact]
        public async Task GetUserUnfinishedTasksById_WhenUserNotExistById11_ThenResponseStatusCode404()
        {
            var httpGetResponse = await _client.GetAsync(path+"/api/LINQ/GetUserUnfinishedTasks/11");

            Assert.Equal(HttpStatusCode.NotFound, httpGetResponse.StatusCode);
        }

    }
}
