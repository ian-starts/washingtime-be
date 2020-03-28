using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WashingTime.Exceptions.ExceptionFilters;
using WashingTime.Infrastructure;
using WashingTime.Infrastructure.Repositories;
using WashingTime.Mapping.Profiles;

namespace WashingTime
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                    opt =>
                    {
                        opt.Filters.Add<UnexpectedArgumentExceptionFilter>();
                        opt.Filters.Add<EntityNotFoundExceptionFilter>();
                        opt.Filters.Add<EntityDeletionRestrictionExceptionFilter>();
                        opt.Filters.Add<UniqueConstraintViolationExceptionFilter>();
                    })
                .AddNewtonsoftJson(
                    x =>
                    {
                        x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        x.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });
            ;
            services.AddPostgreSqlHeldNodigContext(
                Configuration["Database:PostgresSQL:ConnectionString"],
                typeof(Startup).GetTypeInfo().Assembly.GetName().Name,
                Environment.IsDevelopment());
            services.AddAutoMapper(
                cfg =>
                {
                    cfg.AddProfile<WashingTimeProfile>();
                    cfg.AllowNullCollections = true;
                    cfg.AllowNullDestinationValues = true;
                },
                typeof(Startup).Assembly);
            services.Scan(
                scan =>
                {
                    scan
                        .FromAssemblyOf<WashingTimeContext>()
                        .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
                });
            services.AddSwaggerDocument(settings => settings.Title = "WashingTime Club API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(
                configure =>
                {
                    configure.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}