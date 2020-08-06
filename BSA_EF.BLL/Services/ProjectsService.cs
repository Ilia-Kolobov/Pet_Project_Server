using BSA_EF.DAL.Interfaces;
using BSA_EF.DAL.Models;
using BSA_EF.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Services
{
    public class ProjectsService : IProjectsService
    {
        private IUnitOfWork unitOfWork;

        public ProjectsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Project>> GetProjects()
        {
            var result = await unitOfWork.Projects.GetAsync();
            return result.ToList();
        }

        public  Task<Project> GetProject(int id)
        {
           return unitOfWork.Projects.GetAsync(id);
        }

        public async Task PostProject(Project project) 
        {
            await unitOfWork.Projects.CreateAsync(project);
            await unitOfWork.Commit();
        }

        public async Task PutProject(Project project)
        {
            await unitOfWork.Projects.UpdateAsync(project);
            await unitOfWork.Commit();
        }

        public async Task DeleteProject(int id) 
        {
            await  unitOfWork.Projects.DeleteAsync(id);
            await unitOfWork.Commit();
        }
    }
}
