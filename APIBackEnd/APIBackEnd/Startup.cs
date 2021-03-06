using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBackEnd.Data;
using APIBackEnd.Models.Interface;
using APIBackEnd.Models.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace APIBackEnd
{

    public class Startup
    {
        public IConfiguration Configuration { get;}
        /// <summary>
        /// Constructor for Startup class which will allow secret json to be used
        /// </summary>
        public Startup()
        {
            var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /// Using the Mvc as our middle ware
            services.AddMvc();
            /// Setting our database schema to default connection
            services.AddDbContext<BoPeepDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //// handling Refernce looping using Newtonsoft
            services.AddControllers()
              .AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );

            // Using Implementing swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Be Peep API", Version = "v1" });
            });

            services.AddTransient<IActivityManager, ActivityService>();
            services.AddTransient<IReviewManager, ReviewsService>();
            services.AddTransient<ITagManager, TagServices>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            /// Using the Swagger
            app.UseSwagger();
            /// The UI fro Swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Be Peep API");
            });

            app.UseRouting();
            //setting a route to be a home and an index, if id exist in a route , it will be used for URL
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
