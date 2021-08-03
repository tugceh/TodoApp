using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Info.Initializations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using todoApp.Data;
using todoApp.Initializations;
using todoApp.Repository;
using todoApp.ServiceLayer;

namespace todoApp
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionConfigs.ConfigureServices(services, _configuration);
            //AutoMapperConfigs.AddMapper(services);

            services.Configure<ApplicationInfoOptions>(_configuration.GetSection("ApplicationInfoOptions"));
            services.Configure<CachingOptions>(_configuration.GetSection("CachingOptions"));

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DbContext, ApplicationDbContext>();

            //services.AddRazorPages();
            //services.AddMvcCore().AddApiExplorer();
            //services.AddMvc();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            var container = new ContainerBuilder();
            container.RegisterModule(new RepositoryModule());
            container.RegisterModule(new ServiceModule());
            container.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.GetInterfaces().Any(x => x == typeof(IInitialization))).AsImplementedInterfaces();

            container.Populate(services); // services cannot change after populating
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseCors("AllowAllOrigin");
            app.UseRouting();

            //app.UseAuthorization();

            app.UseSwagger().UseSwaggerUI(s =>
            {
                s.RoutePrefix = "swagger";
                s.SwaggerEndpoint($"./v1/swagger.json", "v1");
                s.OAuthClientId("swagger-test");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureExceptionHandler(app.ApplicationServices.GetService<ILogger<Startup>>());
        }
    }
}
