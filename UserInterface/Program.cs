using System;
using System.Drawing;
using Console = Colorful.Console;
using System.Net.Http;
using System.Collections.Generic;
using Common.DTOs;
using Common.DTOs.LinqDTOs;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using UserInterface.Menu;
using UserInterface.Services;

namespace UserInterface
{
    class Program
    {
        private static HttpClient client = new HttpClient();
        private static string path = "https://localhost:44339/api/";
        private static QueriesService queries = new QueriesService(client, path);
        public Program()
        {
            client = new HttpClient();
            path = "https://localhost:44339/api/";
            queries = new QueriesService(client, path);
        }
        public static void Main(string[] args)
        {
            Console.SetWindowSize(130, 30);
            // Title word
            string headerText = "       COOL LINQ      ";
            Console.Clear();
            // setup main menu
            ConsoleMenu mainMenu = new ConsoleMenu();
            // Submenu1
            ConsoleMenu subMenu = new ConsoleMenu();
            subMenu.SubTitle = "------------------------------------------------------ Меню запитiв Projects -----------------------------------------------------";
            subMenu.AddMenuItem(0, "1.Get all projects", () => Task.WaitAny(GetProjects()));
            subMenu.AddMenuItem(1, "2.Get project by id", () => Task.WaitAny(GetProjectById()));
            subMenu.AddMenuItem(2, "3.Post project", () => Task.WaitAny(PostProject()));
            subMenu.AddMenuItem(3, "4.Put project", () => Task.WaitAny(PutProject()));
            subMenu.AddMenuItem(4, "5.Delete project by id", () => Task.WaitAny(DeleteProjectById()));
            subMenu.AddMenuItem(5, "6.Вихiд", subMenu.HideMenu);
            subMenu.ParentMenu = mainMenu;
            // Submenu2
            ConsoleMenu subMenu2 = new ConsoleMenu();
            subMenu2.SubTitle = "-------------------------------------------------------- Меню запитiв Users ------------------------------------------------------";
            subMenu2.AddMenuItem(0, "1.Get all users", () => Task.WaitAny(GetUsers()));
            subMenu2.AddMenuItem(1, "2.Get user by id", () => Task.WaitAny(GetUserById()));
            subMenu2.AddMenuItem(2, "3.Post users", () => Task.WaitAny(PostUser()));
            subMenu2.AddMenuItem(3, "4.Put user", () => Task.WaitAny(PutUser()));
            subMenu2.AddMenuItem(4, "5.Delete user by id", () => Task.WaitAny(DeleteUserById()));
            subMenu2.AddMenuItem(5, "6.Вихiд", subMenu2.HideMenu);
            subMenu2.ParentMenu = mainMenu;
            // Submenu3
            ConsoleMenu subMenu3 = new ConsoleMenu();
            subMenu3.SubTitle = "-------------------------------------------------------- Меню запитiв Teams ------------------------------------------------------";
            subMenu3.AddMenuItem(0, "1.Get all teams", () => Task.WaitAny(GetTeams()));
            subMenu3.AddMenuItem(1, "2.Get team by id", () => Task.WaitAny(GetTeamById()));
            subMenu3.AddMenuItem(2, "3.Post team", () => Task.WaitAny(PostTeam()));
            subMenu3.AddMenuItem(3, "4.Put team", () => Task.WaitAny(PutTeam()));
            subMenu3.AddMenuItem(4, "5.Delete team by id", () => Task.WaitAny(DeleteTeamById()));
            subMenu3.AddMenuItem(5, "6.Вихiд", subMenu3.HideMenu);
            subMenu3.ParentMenu = mainMenu;
            // Submenu4
            ConsoleMenu subMenu4 = new ConsoleMenu();
            subMenu4.SubTitle = "-------------------------------------------------------- Меню запитiв Tasks ------------------------------------------------------";
            subMenu4.AddMenuItem(0, "1.Get all tasks", () => Task.WaitAny(GetTasks()));
            subMenu4.AddMenuItem(1, "2.Get task by id", () => Task.WaitAny(GetTaskById()));
            subMenu4.AddMenuItem(2, "3.Post task", () => Task.WaitAny(PostTask()));
            subMenu4.AddMenuItem(3, "4.Put task", () => Task.WaitAny(PutTask()));
            subMenu4.AddMenuItem(4, "5.Delete task by id", () => Task.WaitAny(DeleteTaskById()));
            subMenu4.AddMenuItem(5, "6.Вихiд", subMenu4.HideMenu);
            subMenu4.ParentMenu = mainMenu;
            // Submenu5
            ConsoleMenu subMenu5 = new ConsoleMenu();
            mainMenu.SubTitle = "-------------------------------------------------------------- Меню запитiв LINQ --------------------------------------------------------------";
            subMenu5.AddMenuItem(0, "1.Кiлькiсть таскiв у проектi конкретного користувача", () => Task.WaitAny(ShowLINQ1()));
            subMenu5.AddMenuItem(1, "2.Cписок таскiв конкретного користувача(name таска < 45 символiв)", () => Task.WaitAny(ShowLINQ2()));
            subMenu5.AddMenuItem(2, "3.Список таскiв, якi виконанi в поточному роцi для конкретного користувача", () => Task.WaitAny(ShowLINQ3()));
            subMenu5.AddMenuItem(3, "4.Cписок команд, учасники яких старшi 10 рокiв, вiдсортованих за датою їх реєстрацiї за спаданням,згрупованих по командах", () => Task.WaitAny(ShowLINQ4()));
            subMenu5.AddMenuItem(4, "5.Список користувачiв за алфавiтом first_name (по зростанню) з вiдсортованими tasks по довжинi name ", () => Task.WaitAny(ShowLINQ5()));
            subMenu5.AddMenuItem(5, "6.Структура User", () => Task.WaitAny(ShowLINQ6()));
            subMenu5.AddMenuItem(6, "7.Структура Project", () => Task.WaitAny(ShowLINQ7()));
            subMenu5.AddMenuItem(7, "8.Вихiд", subMenu5.HideMenu);
            subMenu5.ParentMenu = mainMenu;
            // Main menu
            mainMenu.SubTitle = "-------------------------------------------------------------- Menu --------------------------------------------------------------";
            mainMenu.AddMenuItem(0, "1.Меню запитiв Projects", subMenu.ShowMenu);
            mainMenu.AddMenuItem(1, "2.Меню запитiв Users", subMenu2.ShowMenu);
            mainMenu.AddMenuItem(2, "3.Меню запитiв Teams", subMenu3.ShowMenu);
            mainMenu.AddMenuItem(3, "4.Меню запитiв Tasks", subMenu4.ShowMenu);
            mainMenu.AddMenuItem(4, "5.Меню запитiв LINQ", subMenu5.ShowMenu);
            mainMenu.AddMenuItem(5, "6.Вiдкладене завдання",async() => await ShowMarkRandomTaskWithDelay());
            mainMenu.AddMenuItem(6, "7.Вихiд", Exit);
            // Cool Parking title for all menues
            mainMenu.Header = headerText;
            subMenu.Header = headerText;
            subMenu2.Header = headerText;
            subMenu3.Header = headerText;
            subMenu4.Header = headerText;
            subMenu5.Header = headerText;
            // Display the menu
            mainMenu.ShowMenu();
        }

        private static async Task ShowMarkRandomTaskWithDelay()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            try
            { 
                var markedTaskId = await queries.MarkRandomTaskWithDelay(1000);
                Console.WriteLine($"Id таску, стан якого було змiнений на \"завершений\" = {markedTaskId}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            Console.ReadKey();
        }

        // Task methods for subMenu4
        private static async Task PostTask()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть назву:");
            string name = Console.ReadLine();
            Console.WriteLine("Введiть опис:");
            string description = Console.ReadLine();
            Console.WriteLine("Введіть дату закінчення:");
            DateTime finishedAt = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введiть стан:");
            int state = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть Id виконавця:");
            int performerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть Id проекта:");
            int projectId = Convert.ToInt32(Console.ReadLine());
            // Serializing 
            var json = JsonConvert.SerializeObject(new ProjectTaskDTO { Id = id, Name = name, Description = description, CreatedAt = DateTime.Now, FinishedAt = finishedAt, PerformerId = performerId, ProjectId = projectId, State = state });
            // Creating content for Post request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Post request
            var response =  await client.PostAsync(path + "ProjectTasks", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task DeleteTaskById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.DeleteAsync(path + "ProjectTasks/" + id);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("ОперацIя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task PutTask()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть назву:");
            string name = Console.ReadLine();
            Console.WriteLine("Введiть опис:");
            string description = Console.ReadLine();
            Console.WriteLine("Введіть дату закінчення:");
            DateTime finishedAt = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введiть стан:");
            int state = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть Id виконавця:");
            int performerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть Id проекта:");
            int projectId = Convert.ToInt32(Console.ReadLine());
            // Serializing 
            var json = JsonConvert.SerializeObject(new ProjectTaskDTO { Id = id, Name = name, Description = description, CreatedAt = DateTime.Now, FinishedAt = finishedAt, PerformerId = performerId, ProjectId = projectId, State = state });
            // Creating content for Put request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Put request
            var response = await client.PutAsync(path + "ProjectTasks", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task GetTaskById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.GetAsync(path + "ProjectTasks/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var task = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<ProjectTaskDTO>();

                Console.WriteLine($"Id таски:{task.Id}\nНазва таски:{task.Name}\nId виконавця: {task.PerformerId}\nId проекту: {task.ProjectId}\nСтан: {task.State}\n");
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }

            Console.ReadKey();
        }


        private static async Task GetTasks()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            await Task.Delay(5000);
            var tasks = await (await client.GetAsync(path + "ProjectTasks")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<ProjectTaskDTO>>();
            foreach (var item in tasks)
                Console.WriteLine($"Id таски:{item.Id}\nНазва таски:{item.Name}\nId виконавця: {item.PerformerId}\nId проекту: {item.ProjectId}\nСтан: {item.State}\n");
            Console.ReadKey();
        }

        // Team methods for subMenu3
        private static async Task DeleteTeamById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.DeleteAsync(path + "Teams/" + id);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("ОперацIя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task PutTeam()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть назву:");
            string name = Console.ReadLine();
            // Serializing 
            var json = JsonConvert.SerializeObject(new TeamDTO { Id = id, Name = name, CreatedAt = DateTime.Now });
            // Creating content for Put request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Put request
            var response = await client.PutAsync(path + "Teams", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task PostTeam()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть назву:");
            string name = Console.ReadLine();
            // Serializing 
            var json = JsonConvert.SerializeObject(new TeamDTO { Id = id, Name = name, CreatedAt = DateTime.Now });
            // Creating content for Post request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Post request
            var response = await client.PostAsync(path + "Teams", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task GetTeamById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.GetAsync(path + "Teams/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var team = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<TeamDTO>();

                Console.WriteLine($"Id команди:{team.Id}\tНазва команди:{team.Name}\tДата створення:{team.CreatedAt}\n");
            }
            else
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result, Color.Red);
            }

            Console.ReadKey();
        }

        private static async Task GetTeams()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            var teams = await (await client.GetAsync(path + "Teams")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<TeamDTO>>();
            foreach (var item in teams)
                Console.WriteLine($"Id команди:{item.Id}\nНазва команди:{item.Name}\nДата створення:{item.CreatedAt}\n");
            Console.ReadKey();
        }


        //Users methods for subMenu2
        private static async Task DeleteUserById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.DeleteAsync(path + "Users/" + id);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task PutUser()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введiть last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введiть e-mail:");
            string email = Console.ReadLine();
            Console.WriteLine("Введiть дату народження:");
            DateTime birthday = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введiть Id команди:");
            int teamId = Convert.ToInt32(Console.ReadLine());
            // Serializing 
            var json = JsonConvert.SerializeObject(new UserDTO { Id = id, FirstName = firstName, LastName = lastName, RegisteredAt = DateTime.Now, Birthday = birthday, Email = email, TeamId = teamId });
            // Creating content for Put request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Put request
            var response = await client.PutAsync(path + "Users", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task PostUser()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введiть last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Введiть e-mail:");
            string email = Console.ReadLine();
            Console.WriteLine("Введіть дату народження:");
            DateTime birthday = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введiть Id команди:");
            int teamId = Convert.ToInt32(Console.ReadLine());
            // Serializing 
            var json = JsonConvert.SerializeObject(new UserDTO { Id = id, FirstName = firstName, LastName = lastName, RegisteredAt = DateTime.Now, Birthday = birthday, Email = email, TeamId = teamId });
            // Creating content for Post request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Post request
            var response = await client.PostAsync(path + "Users", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task GetUserById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());

            var response = client.GetAsync(path + "Users/" + id).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var user = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<UserDTO>();
                Console.WriteLine($"Id користувача:{user.Id}\nПовне iм'я користувача:{user.FirstName} {user.LastName}\nId команди: {user.TeamId}");
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task GetUsers()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            var users = await (await client.GetAsync(path + "Users")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<UserDTO>>();
            foreach (var item in users)
                Console.WriteLine($"Id користувача:{item.Id}\nПовне iм'я користувача:{item.FirstName} {item.LastName}\nId команди: {item.TeamId}\n");
            Console.ReadKey();
        }

        //Project methods for subMenu
        private static async Task PutProject()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть назву:");
            string name = Console.ReadLine();
            Console.WriteLine("Введiть опис:");
            string description = Console.ReadLine();
            Console.WriteLine("Введiть дедлайн:");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введiть Id автора:");
            int authorId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть Id команди:");
            int teamId = Convert.ToInt32(Console.ReadLine());
            // Serializing 
            var json = JsonConvert.SerializeObject(new ProjectDTO { Id = id, Name = name, Description = description, CreatedAt = DateTime.Now, Deadline = deadline, AuthorId = authorId, TeamId = teamId });
            // Creating content for Put request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Put request
            var response = await client.PutAsync(path + "Projects", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task DeleteProjectById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.DeleteAsync(path + "Projects/" + id);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("ОперацIя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task PostProject()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            // User input
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть назву проекта");
            string name = Console.ReadLine();
            Console.WriteLine("Введiть опис проекту:");
            string description = Console.ReadLine();
            Console.WriteLine("Введiть дедлайн:");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введiть Id автора:");
            int authorId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введiть Id команди:");
            int teamId = Convert.ToInt32(Console.ReadLine());
            // Serializing 
            var json = JsonConvert.SerializeObject(new ProjectDTO { Id = id, Name = name, Description = description, CreatedAt = DateTime.Now, Deadline = deadline, AuthorId = authorId, TeamId = teamId });
            // Creating content for Post request
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Post request
            var response = await client.PostAsync(path + "Projects", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Операцiя виконана успiшно!", Color.Green);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        private static async Task GetProjectById()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.GetAsync(path + "Projects/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var project = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<ProjectDTO>();
                Console.WriteLine($"Id проекта:{project.Id}\tНазва проекта:{project.Name}\nОпис:\t\t{project.Description}");
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        static async Task GetProjects()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            var projects = await (await client.GetAsync(path + "Projects")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<ProjectDTO>>();
            foreach (var item in projects)
                Console.WriteLine($"Id проекта:{item.Id}\nНазва проекта:{item.Name}\nОпис:\t\t{item.Description}\n");
            Console.ReadKey();
        }

        // LINQ methods for subMenu5
        public static async Task ShowLINQ1()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id користувача:");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                var response = await client.GetAsync(path + "LINQ/CountOfUserTasksInProject/" + id);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var usersTasks = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<List<ProjectCountDTO>>();
                    foreach (var item in usersTasks)
                        Console.WriteLine($"Id проекта:{item.Key.Id}\tНазва проекта:{item.Key.Name,10} Кiлькiсть таскiв:{item.Value}");
                }
                else
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
                }

            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        public static async Task ShowLINQ2()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id користувача:");
            int id = Convert.ToInt32(Console.ReadLine());

            var response = await client.GetAsync(path + "LINQ/TasksByUserIdWhereNameLess45/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var tasks = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<List<ProjectTaskDTO>>();
                foreach (var item in tasks)
                    Console.WriteLine($"Id таска:{item.Id}\nНазва таска:{item.Name}\nСтан таска:{item.State}\n");
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }


            Console.ReadKey();
        }

        public static async Task ShowLINQ3()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id користувача:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.GetAsync(path + "LINQ/FinishedTasksInThisYearByUserId/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var tasks = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<List<TaskIdNameDTO>>();
                foreach (var item in tasks)
                    Console.WriteLine($"Id таска:{item.Id}\tНазва таска:{item.Name}");
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        public static async Task ShowLINQ4()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            var teamsUsers = await (await client.GetAsync(path + "LINQ/TeamsMembersOverTenYearsOld")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<TeamsUsersDTO>>();
            foreach (var item in teamsUsers)
            {
                Console.WriteLine($"\n\nId команди:{item.Team.Id}\nНазва команди:{item.Team.Name}");
                Console.Write("Список користувачiв:\n");
                foreach (var user in item.Users)
                    Console.Write($"{user.FirstName} Дата реєстрацiї:{user.RegisteredAt}\n");
            }
            Console.ReadKey();
        }

        public static async Task ShowLINQ5()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            var usersTasks = await (await client.GetAsync(path + "LINQ/SortedUsersWithSortedTasks/")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<UsersTasksDTO>>();
            foreach (var item in usersTasks)
            {
                Console.WriteLine($"Id користувача:{item.User.Id}\tIм'я користувача:{item.User.FirstName}");
                foreach (var task in item.Tasks)
                    Console.WriteLine($"Назва таску:{task.Name}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static async Task ShowLINQ6()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            Console.WriteLine("Введiть Id користувача:");
            int id = Convert.ToInt32(Console.ReadLine());
            var response = await client.GetAsync(path + "LINQ/UserInfoById/" + id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var userInfo = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<UserInfoDTO>();
                Console.WriteLine($"Iм'я користувача:{userInfo.User.FirstName}\nОстаннiй проект користувача:{userInfo.LastProject.Name}\n" +
                                          $"Кiлькiсть таскiв пiд останнiм проектом:{userInfo.TasksCount}\nКiлькiсть незавершених або скасованих таскiв для користувача:{userInfo.UnfinishedAndCanceledTasksCount}\n" +
                                          $"Найтривалiший таск користувача за датою:{userInfo.LongestTask.Name}\n");
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync(), Color.Red);
            }
            Console.ReadKey();
        }

        public static async Task ShowLINQ7()
        {
            Console.WriteLine("Для повернення у МЕНЮ натиснiть будь-яку клавiшу\n", Color.Khaki);
            var projectsInfo = await (await client.GetAsync(path + "LINQ/ProjectsInfo")).EnsureSuccessStatusCode().Content.ReadAsAsync<List<ProjectInfoDTO>>();
            foreach (var item in projectsInfo)
            {
                Console.WriteLine($"Id проекту:{item.Project.Id}\nНазва проекту:{item.Project.Name}\n" +
                                  $"Назва найдовшого таску:{item.LongestTaskByDicription.Name}\nНазва найкоротшого таску:{item.LessTaskByName.Name}\n" +
                                  $"Кiлькiсть користувачiв в командi проекту:{item.UserCounWhereDescriptionMore20orTaskCounLess3}\n");
            }
            Console.ReadKey();
        }
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
