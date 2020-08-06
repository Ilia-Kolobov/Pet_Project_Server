using BSA_EF.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Interfaces
{
    public interface IProjectTasksService
    {
        public Task<List<ProjectTask>> GetProjectsTasks();
        public Task<ProjectTask> GetProjectTask(int id);
        public Task PostProjectTask(ProjectTask task);
        public Task PutProjectTask(ProjectTask task);
        public Task DeleteProjectTask(int id);
    }
}
