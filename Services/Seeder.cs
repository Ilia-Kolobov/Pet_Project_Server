using Newtonsoft.Json;
using BSA_EF.DAL.Models;
using BSA_EF.DAL;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BSA_EF.DAL
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(JsonConvert.DeserializeObject<List<Project>>(File.ReadAllText("StartData\\Projects.json")));
            modelBuilder.Entity<ProjectTask>().HasData(JsonConvert.DeserializeObject<List<ProjectTask>>(File.ReadAllText("StartData\\Tasks.json")));
            modelBuilder.Entity<Team>().HasData(JsonConvert.DeserializeObject<List<Team>>(File.ReadAllText("StartData\\Teams.json")));
            modelBuilder.Entity<User>().HasData(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("StartData\\Users.json")));
        }

    }
}
