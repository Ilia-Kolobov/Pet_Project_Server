using BSA_EF.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using BSA_EF.DAL.Models;
using BSA_EF.BLL.Interfaces;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Services
{
    public class ProjectTasksService : IProjectTasksService
    {
        private IUnitOfWork unitOfWork;

        public ProjectTasksService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<ProjectTask>> GetProjectsTasks()
        {
            var result = await unitOfWork.ProjectTasks.GetAsync();
            return result.ToList();
        }

        public Task<ProjectTask> GetProjectTask(int id)
        {
            return unitOfWork.ProjectTasks.GetAsync(id);
        }

        public async Task PostProjectTask(ProjectTask task)
        {
            await unitOfWork.ProjectTasks.CreateAsync(task);
            await unitOfWork.Commit();
        }

        public async Task PutProjectTask(ProjectTask task)
        {
            await unitOfWork.ProjectTasks.UpdateAsync(task);
            await unitOfWork.Commit();
        }

        public async Task DeleteProjectTask(int id)
        {
            await unitOfWork.ProjectTasks.DeleteAsync(id);
            await unitOfWork.Commit();
        }
    }
}
