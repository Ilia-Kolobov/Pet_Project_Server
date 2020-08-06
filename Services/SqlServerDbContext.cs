using Microsoft.EntityFrameworkCore;
using BSA_EF.DAL.Models;
using BSA_EF.DAL;
using System.Security.Cryptography.X509Certificates;

namespace BSA_EF.DAL
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<ProjectTask>().HasOne<Project>().WithMany().HasForeignKey(k => k.ProjectId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Project>().HasOne<Team>().WithMany().HasForeignKey(k => k.TeamId);
            modelBuilder.Entity<Project>().HasOne<User>().WithMany().HasForeignKey(k => k.AuthorId);
            modelBuilder.Entity<ProjectTask>().HasOne<User>().WithMany().HasForeignKey(k => k.PerformerId);
            Seeder.Seed(modelBuilder);
        }

    }
}
