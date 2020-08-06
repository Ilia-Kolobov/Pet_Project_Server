using BSA_EF.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BSA_EF.WebAPI.Tests
{
    public class FakeSqlServerDbContext : DbContext
    {
        public FakeSqlServerDbContext(DbContextOptions<FakeSqlServerDbContext> options)
            : base(options)
        { }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>().HasOne<Project>().WithMany().HasForeignKey(k => k.ProjectId);
            modelBuilder.Entity<Project>().HasOne<Team>().WithMany().HasForeignKey(k => k.TeamId);
            modelBuilder.Entity<Project>().HasOne<User>().WithMany().HasForeignKey(k => k.AuthorId);
            modelBuilder.Entity<ProjectTask>().HasOne<User>().WithMany().HasForeignKey(k => k.PerformerId);
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Name = "Expedita amet quas id a.",
                    Description = "Ea ab omnis saepe rem vel et.\nIllo quaerat eos accusantium reiciendis dolores quibusdam ratione.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    Deadline = new DateTime(2021, 2, 2),
                    AuthorId = 5,
                    TeamId = 5
                },
                new Project
                {
                    Id = 2,
                    Name = "Totam autem hic atque suscipit.",
                    Description = "Aut quia id adipisci alias non mollitia.\nAlias et at quia soluta quisquam aspernatur nemo molestias.\nVel id suscipit vero ipsa repudiandae nesciunt.\nProvident veritatis maiores aut.\nIste et incidunt.",
                    CreatedAt = new DateTime(2020, 6, 30),
                    Deadline = new DateTime(2021, 4, 30),
                    AuthorId = 1,
                    TeamId = 1
                },
                new Project
                {
                    Id = 3,
                    Name = "Eos illum eum minima quibusdam.",
                    Description = "Quis dicta repudiandae consequatur et odio repudiandae occaecati.\nDolore fugit veniam dolorem aperiam consequatur cum sed officiis ut.\nExercitationem ea ducimus saepe id asperiores dignissimos molestiae repellat.",
                    CreatedAt = new DateTime(2020, 1, 1),
                    Deadline = new DateTime(2021, 11, 7),
                    AuthorId = 9,
                    TeamId = 4
                },
                new Project
                {
                    Id = 4,
                    Name = "Quia repellendus odit et eligendi.",
                    Description = "Voluptatem eaque accusamus maiores quo beatae quos doloremque.\nEos pariatur ea saepe atque.\nDelectus quidem voluptatem harum architecto repellat.\nCupiditate culpa consectetur illo occaecati et.\nCumque inventore voluptas tenetur.\nFacilis quaerat sed praesentium.",
                    CreatedAt = new DateTime(2020, 6, 30),
                    Deadline = new DateTime(2020, 9, 1),
                    AuthorId = 7,
                    TeamId = 2
                },
                new Project
                {
                    Id = 5,
                    Name = "Aspernatur vero quas et ipsum.",
                    Description = "Qui rem mollitia inventore nulla nam nam excepturi.\nQuibusdam distinctio iste quo dolor.\nBeatae consequatur qui est quo amet et quia.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    Deadline = new DateTime(2021, 5, 30),
                    AuthorId = 8,
                    TeamId = 3
                },
                new Project
                {
                    Id = 6,
                    Name = "Reprehenderit nam architecto sed aut.",
                    Description = "Molestiae incidunt praesentium dolor odit culpa voluptatibus maxime et nam.\nAut nam et laudantium omnis et sed.\nOdio perspiciatis iure exercitationem possimus dicta minima.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    Deadline = new DateTime(2020, 7, 30),
                    AuthorId = 9,
                    TeamId = 4
                },
                new Project
                {
                    Id = 7,
                    Name = "Quaerat omnis enim sint ex.",
                    Description = "Modi nisi quasi vero odio amet excepturi.\nOfficiis et a molestiae rerum.\nSuscipit ea aut autem ipsa itaque nihil.\nEum et nihil eveniet accusantium ea quod temporibus.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    Deadline = new DateTime(2021, 3, 30),
                    AuthorId = 1,
                    TeamId = 1
                }
                );

            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask
                {
                    Id = 1,
                    Name = "Quasi consectetur nesciunt doloribus.",
                    Description = "Sed voluptas quia dolores expedita eius laborum ut qui aspernatur.\nMolestias sapiente pariatur fuga architecto sed.\nAutem repellendus maxime magni qui exercitationem rerum.\nDolorem magnam aut commodi nemo aut quaerat.\nEos sit veniam qui molestiae facere voluptatem.\nFacilis eum atque enim dolor facilis ea ipsum tempora.",
                    CreatedAt = new DateTime(2020, 6, 30),
                    FinishedAt = new DateTime(2021, 12, 10),
                    State = 3,
                    ProjectId = 1,
                    PerformerId = 10,
                },
                new ProjectTask
                {
                    Id = 2,
                    Name = "Praesentium ut consequatur cumque eveniet suscipit amet officia.",
                    Description = "Praesentium autem consequatur magnam et doloribus exercitationem.\nAut animi fuga cupiditate debitis atque nisi consequatur consequatur.\nCupiditate necessitatibus quo eos sequi earum et quis accusamus.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2021, 12, 19),
                    State = 3,
                    ProjectId = 2,
                    PerformerId = 5
                },
                new ProjectTask
                {
                    Id = 3,
                    Name = "Error sit sunt.",
                    Description = "Unde dignissimos libero minima quas aliquam.\nConsequuntur aliquid non.\nEligendi quia quidem nihil sit veritatis.",
                    CreatedAt = new DateTime(2020, 6, 30),
                    FinishedAt = new DateTime(2020, 7, 25),
                    State = 2,
                    ProjectId = 2,
                    PerformerId = 1,
                },
                new ProjectTask
                {
                    Id = 4,
                    Name = "Repellendus itaque expedita est ut.",
                    Description = "Nisi esse accusamus dolorem blanditiis porro est dolores.\nExplicabo consequatur rem dignissimos odit praesentium.\nMolestiae facilis et tenetur.\nVoluptas quis sed et ab nulla omnis cupiditate.\nId sed et.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2020, 10, 19),
                    State = 2,
                    ProjectId = 3,
                    PerformerId = 4,
                },
                new ProjectTask
                {
                    Id = 5,
                    Name = "Voluptate neque vel molestiae dolor nulla voluptas voluptas optio.",
                    Description = "Rerum totam sit.\nVelit saepe iusto et repellat et consequuntur sit.\nVoluptate officiis pariatur ut ea.\nNeque ut sed voluptatem occaecati.\nDolor velit quaerat molestiae assumenda veritatis voluptatem.",
                    CreatedAt = new DateTime(2020, 6, 30),
                    FinishedAt = new DateTime(2021, 6, 20),
                    State = 3,
                    ProjectId = 3,
                    PerformerId = 9,
                },
                new ProjectTask
                {
                    Id = 6,
                    Name = "In qui labore omnis voluptas voluptates aspernatur consectetur pariatur.",
                    Description = "Sit tenetur nihil laborum qui quia assumenda ratione.\nTempora esse deleniti quia debitis incidunt odio consequatur unde.\nEt quos quam consectetur excepturi sint qui enim autem eaque.\nAdipisci praesentium officia non quod vel rerum nihil.\nVeniam qui incidunt dolorum.\nMolestiae ea officia qui explicabo nulla repellat.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2020, 10, 26),
                    State = 2,
                    ProjectId = 4,
                    PerformerId = 6,
                },
                new ProjectTask
                {
                    Id = 7,
                    Name = "Rerum eum qui ad repudiandae eum laborum ut saepe.",
                    Description = "Dolores esse quibusdam aut ut quidem nulla voluptatem.\nQuidem vitae sequi aut qui cumque adipisci quo quam.\nAlias quis voluptatibus.\nQui est aut.\nNihil quia occaecati occaecati totam laudantium.\nNobis cum quae saepe molestiae voluptas id reiciendis a consequuntur.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2021, 6, 16),
                    State = 1,
                    ProjectId = 4,
                    PerformerId = 7,
                    
                },
                new ProjectTask
                {
                    Id = 8,
                    Name = "Et ipsa voluptatem non eos et similique sunt.",
                    Description = "Nihil eos minima sed.\nEst et assumenda voluptatem voluptatem illum doloribus.\nVoluptatem enim voluptatem et ut.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2021, 12, 29),
                    State = 1,
                    ProjectId = 6,
                    PerformerId = 8,
                },
                new ProjectTask
                {
                    Id = 9,
                    Name = "Dolorem fugiat impedit nesciunt enim non.",
                    Description = "Ipsam quo soluta aut numquam aliquam sint.\nAliquam voluptas error fuga est et quae dolores.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2020, 11, 24),
                    State = 2,
                    ProjectId = 6,
                    PerformerId = 1,
                },
                new ProjectTask
                {
                    Id = 10,
                    Name = "Dolorem eos incidunt eum perspiciatis officia ratione est rerum.",
                    Description = "Quia quam eveniet quisquam rerum voluptatum laboriosam repudiandae.\nVoluptate fugit esse eveniet ducimus sunt veniam a.\nSed qui et consequatur similique eum velit ipsa voluptates ut.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2020, 10, 13),
                    State = 2,
                    ProjectId = 7,
                    PerformerId = 9,
                },
                new ProjectTask
                {
                    Id = 11,
                    Name = "Sit qui repellendus libero libero.",
                    Description = "Quia quam eveniet rerum voluptatum laboriosam repudiandae.\nVoluptate fugit esse eveniet ducimus sunt veniam a.\nSed qui et consequatur similique eum velit ipsa voluptates ut.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2020, 12, 13),
                    State = 2,
                    ProjectId = 2,
                    PerformerId = 1,
                },
                new ProjectTask
                {
                    Id = 12,
                    Name = "Quidem dolores ut dolores atque sapiente laborum sint.",
                    Description = "Quia quam eveniet quisquam rerum laboriosam repudiandae.\nVoluptate fugit esse eveniet ducimus sunt veniam a.\nSed qui et consequatur similique eum velit ipsa voluptates ut.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2021, 6, 17),
                    State = 0,
                    ProjectId = 4,
                    PerformerId = 8,
                },
                new ProjectTask
                {
                    Id = 13,
                    Name = "Minima vel eum atque.",
                    Description = "Quas et quae eveniet quisquam rerum voluptatum laboriosam repudiandae.\nVoluptate fugit esse eveniet ducimus sunt veniam a.\nSed qui et consequatur similique eum velit ipsa voluptates ut.",
                    CreatedAt = new DateTime(2020, 6, 30),
                    FinishedAt = new DateTime(2021, 2, 22),
                    State = 0,
                    ProjectId = 4,
                    PerformerId = 10,
                    
                },
                new ProjectTask
                {
                    Id = 14,
                    Name = "Placeat impedit ullam eveniet corporis quidem doloribus.",
                    Description = "Ipsum odio qui in dolorum aperiam ut.\nBlanditiis ratione sapiente quos est quia pariatur.\nSed qui et consequatur similique eum velit ipsa voluptates ut.",
                    CreatedAt = new DateTime(2020, 7, 1),
                    FinishedAt = new DateTime(2020, 10, 13),
                    State = 2,
                    ProjectId = 6,
                    PerformerId = 10,
                }
                );


            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "debitis",
                    CreatedAt = new DateTime(2020, 6, 30)
                },
                new Team
                {
                    Id = 2,
                    Name = "asperiores",
                    CreatedAt = new DateTime(2020, 6, 30)
                },
                new Team
                {
                    Id = 3,
                    Name = "cupiditate",
                    CreatedAt = new DateTime(2020, 7, 1)
                },
                new Team
                {
                    Id = 4,
                    Name = "ea",
                    CreatedAt = new DateTime(2020, 7, 1)
                },
                new Team
                {
                    Id = 5,
                    Name = "amet",
                    CreatedAt = new DateTime(2020, 6, 30)
                }
                );


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Jordane",
                    LastName = "Walker",
                    Email = "Jordane.Walker@gmail.com",
                    Birthday = new DateTime(2005, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 24),
                    TeamId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Kolby",
                    LastName = "Jones",
                    Email = "Kolby_Jones@yahoo.com",
                    Birthday = new DateTime(2012, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 1
                },
                new User
                {
                    Id = 3,
                    FirstName = "Mabelle",
                    LastName = "Miller",
                    Email = "Mabelle.Miller12@hotmail.com",
                    Birthday = new DateTime(2015, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 1
                },
                new User
                {
                    Id = 4,
                    FirstName = "Verla",
                    LastName = "Bechtelar",
                    Email = "Verla.Bechtelar62@gmail.com",
                    Birthday = new DateTime(2015, 10, 28),
                    RegisteredAt = new DateTime(2020, 7, 1),
                    TeamId = 4
                },
                new User
                {
                    Id = 5,
                    FirstName = "Retha",
                    LastName = "Will",
                    Email = "Retha67@yahoo.com",
                    Birthday = new DateTime(2007, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 5
                },
                new User
                {
                    Id = 6,
                    FirstName = "Roberto",
                    LastName = "Ruecker",
                    Email = "Roberto.Ruecker35@yahoo.com",
                    Birthday = new DateTime(2007, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 2
                },
                new User
                {
                    Id = 7,
                    FirstName = "Tracy",
                    LastName = "Schinner",
                    Email = "Tracy.Schinner65@gmail.com",
                    Birthday = new DateTime(2012, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 2
                },
                new User
                {
                    Id = 8,
                    FirstName = "Norma",
                    LastName = "Gislason",
                    Email = "Norma_Gislason@yahoo.com",
                    Birthday = new DateTime(2003, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 3
                },
                new User
                {
                    Id = 9,
                    FirstName = "Luisa",
                    LastName = "Lind",
                    Email = "Luisa92@yahoo.com",
                    Birthday = new DateTime(2017, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 4
                },
                new User
                {
                    Id = 10,
                    FirstName = "Edward",
                    LastName = "Kuvalis",
                    Email = "Edward_Kuvalis@yahoo.com",
                    Birthday = new DateTime(2007, 10, 28),
                    RegisteredAt = new DateTime(2020, 6, 28),
                    TeamId = 5
                }
                );
        }
    }
}
