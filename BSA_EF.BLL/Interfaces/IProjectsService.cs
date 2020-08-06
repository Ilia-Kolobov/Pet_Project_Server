using BSA_EF.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Interfaces
{
    public interface IProjectsService
    {
        public Task<List<Project>> GetProjects();
        public Task<Project> GetProject(int id);
        public Task PostProject(Project project);
        public Task PutProject(Project project);
        public Task DeleteProject(int id);
    }
}
