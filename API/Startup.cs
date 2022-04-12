using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Data;
using Application.JobApps.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
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
            services.AddControllers();

            services.AddMediatR(typeof(Startup), typeof(GetAllJobAppsQuery));

            // TODO - Update Db configuration w/Azure SQL connection string

            // Use this connection string to quickly test locally on clients w/o easy access to SQL Server

            //services.AddDbContext<JobAppsDbContext>(options =>
            //    options.UseInMemoryDatabase("JobApps"));

            services.AddDbContext<JobAppsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
            }

            // TODO - Move this back into only for dev once troubleshooting in Azure is complete
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(
            //options =>
            //{
            //    options.SwaggerEndpoint("/swagger", "v1");
            //    options.RoutePrefix = string.Empty;
            //}
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
