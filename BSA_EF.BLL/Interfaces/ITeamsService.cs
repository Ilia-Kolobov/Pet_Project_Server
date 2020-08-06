using BSA_EF.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.BLL.Interfaces
{
    public interface ITeamsService
    {
        public Task<List<Team>> GetTeams();

        public Task<Team> GetTeam(int id);

        public Task PostTeam(Team team);

        public Task PutTeam(Team team);

        public Task DeleteTeam(int id);
    }
}
