
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.BusinessService.Services;
using Template.BusinessService.Services.Abstract;
using Template.DAL.Repositories;
using Template.DAL.Repositories.Abstract;
using Template.DAL.Repositories.Core;
using Template.DTO.MappingProfiles;
using Template.Entities.Context;

namespace Template.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment configuration)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(configuration.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{configuration.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("ApplicationDbContext")));

            services.AddSingleton<IConfiguration>(Configuration);

            //Automapper Configurations
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOToDomainMapping());
                cfg.AddProfile(new DomainToDTOMapping());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Include Repository DI  
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Include Service DI
            services.AddScoped<ITemplateService, TemplateService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
