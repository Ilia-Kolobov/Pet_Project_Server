using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BSA_EF.DAL.Interfaces;
using BSA_EF.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using BSA_EF.BLL.Services;
using BSA_EF.BLL.Interfaces;
using BSA_EF.DAL;

namespace BSA_EF.WebAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILINQService, LINQService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IProjectTasksService, ProjectTasksService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<DbContext, SqlServerDbContext>();

            services.AddDbContext<SqlServerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("QueryModelsDatabase")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SqlServerDbContext context)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
         
            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
            
        }
    }
}
