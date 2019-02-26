using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Data;
using TestProject.Data.Entities;
using TestProject.Repositories;
using TestProject.Services;

namespace TestProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<CarDbContext>(c => c.UseInMemoryDatabase("car"));


            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IGarageRepository, GarageRepository>();
            services.AddTransient<ICarService, CarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
            InitializeInMemoryDatabase(app);
        }

        private static void InitializeInMemoryDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CarDbContext>();
                context.Areas.Add(new AreaEntity {Id = 1, Name = "area", DateCreated = DateTime.Now});
                context.Garages.Add(new GarageEntity {Id = 1, Name = "garage", DateCreated = DateTime.Now, AreaId = 1});
                context.CarCategories.Add(new CarCategoryEntity
                    {Id = 1, Name = "category", DateCreated = DateTime.Now});
                context.Cars.Add(new CarEntity
                    {Id = 1000, Description = "carDescription", Title = "title",
                        DateCreated = DateTime.Now, CategoryId = 1, GarageId = 1});
                context.SaveChanges();
            }
        }
    }
}
