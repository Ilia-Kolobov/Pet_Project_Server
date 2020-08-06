using BSA_EF.DAL.Interfaces;
using BSA_EF.DAL.Models;
using BSA_EF.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Services
{
    public class TeamsService : ITeamsService
    {
        private IUnitOfWork unitOfWork;

        public TeamsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Team>> GetTeams()
        {
            var result =  await unitOfWork.Teams.GetAsync();
            return result.ToList();
        }

        public Task<Team> GetTeam(int id)
        {
            return unitOfWork.Teams.GetAsync(id);
        }

        public async Task PostTeam(Team team)
        {
            await unitOfWork.Teams.CreateAsync(team);
            await unitOfWork.Commit();
        }

        public async Task PutTeam(Team team)
        {
            await unitOfWork.Teams.UpdateAsync(team);
            await unitOfWork.Commit();
        }

        public async Task DeleteTeam(int id)
        {
            await unitOfWork.Teams.DeleteAsync(id);
            await unitOfWork.Commit();
        }
    }
}
