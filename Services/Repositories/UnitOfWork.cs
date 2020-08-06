using BSA_EF.DAL.Interfaces;
using BSA_EF.DAL.Models;
using BSA_EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BSA_EF.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        private IRepository<Project> _projectsAsync;
        private IRepository<ProjectTask> _projectTasksAsync;
        private IRepository<Team> _teamsAsync;
        private IRepository<User> _usersAsync;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }


        IRepository<Project> IUnitOfWork.Projects
        {
            get
            {
                return _projectsAsync ??
                    (_projectsAsync = new Repository<Project>(_context));
            }
        }
        IRepository<ProjectTask> IUnitOfWork.ProjectTasks
        {
            get
            {
                return _projectTasksAsync ??
                    (_projectTasksAsync = new Repository<ProjectTask>(_context));
            }
        }
        IRepository<Team> IUnitOfWork.Teams
        {
            get
            {
                return _teamsAsync ??
                    (_teamsAsync = new Repository<Team>(_context));
            }
        }
        IRepository<User> IUnitOfWork.Users
        {
            get
            {
                return _usersAsync ??
                    (_usersAsync = new Repository<User>(_context));
            }
        }

        async Task IUnitOfWork.Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}