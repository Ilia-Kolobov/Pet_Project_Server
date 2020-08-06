using Common.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UserInterface.Services
{
    public class QueriesService
    {
        private HttpClient client;
        private string path;
        private Random random = new Random();
        public QueriesService(HttpClient client, string path)
        {
            this.client = client;
            this.path = path;
        }
        public Task<int> MarkRandomTaskWithDelay(int seconds)
        {
            TaskCompletionSource<int> tcs1 = new TaskCompletionSource<int>();
            Task<int> t1 = tcs1.Task;

            Timer timer = new Timer(seconds)
            {
                AutoReset = false
            };
            timer.Elapsed += async (object source, ElapsedEventArgs e) =>
            {
                var allTasks = await (await client.GetAsync(path + "ProjectTasks")).
                   EnsureSuccessStatusCode().Content.ReadAsAsync<List<ProjectTaskDTO>>();

                    var unfinishedTasks = allTasks.Where(x => x.State != (int)TaskStatesDTO.Finished).ToList();
                    int randomIndex = random.Next(0, unfinishedTasks.Count() - 1);

                    ProjectTaskDTO projectTask = unfinishedTasks[randomIndex];
                    projectTask.State = (int)TaskStatesDTO.Finished;
                    // Serializing 
                    var json = JsonConvert.SerializeObject(projectTask);
                    // Creating content for Put request
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    // Put request
                    var response = await client.PutAsync(path + "ProjectTasks", content);
                    if (response.IsSuccessStatusCode)
                    {
                        tcs1.SetResult(projectTask.Id);
                    }
                    else
                    {
                        tcs1.SetException(new InvalidOperationException("Немає не завершених тасків"));
                    }
            };
            timer.Start();
            return t1;
        }
    }
}