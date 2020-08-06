using BSA_EF.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSA_EF.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Project> Projects { get; }
        IRepository<ProjectTask> ProjectTasks { get; }
        IRepository<Team> Teams { get; }
        IRepository<User> Users { get; }
        Task Commit();
    }
}
