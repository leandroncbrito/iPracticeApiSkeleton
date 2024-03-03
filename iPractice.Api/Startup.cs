using System.Collections.Generic;
using iPractice.Application.Commands;
using iPractice.Application.Commands.Handlers;
using iPractice.Application.Interfaces;
using iPractice.Application.Queries;
using iPractice.Application.Queries.Handlers;
using iPractice.Application.Services;
using iPractice.DataAccess;
using iPractice.DataAccess.Interfaces;
using iPractice.DataAccess.Repositories;
using iPractice.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace iPractice.Api
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
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " iPractice APIs"
                });
            });

            RegisterHandlers(services);
            RegisterRepositories(services);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Sqlite")));

            services.AddControllers();
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            services.AddScoped<ITimeSlotService, TimeSlotService>();
            
            services.AddScoped<ICommandHandler<CreateTimeSlotsCommand>, CreateTimeSlotsCommandHandler>();
            services.AddScoped<ICommandHandler<CreateAppointmentCommand>, CreateAppointmentCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateAvailabilityCommand>, UpdateAvailabilityCommandHandler>();
            
            services.AddScoped<IQueryHandler<GetAvailablePsychologistsQuery, IEnumerable<Psychologist>>, GetAvailablePsychologistsQueryHandler>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IRepository<TimeSlot>, Repository<TimeSlot>>();
            services.AddScoped<IRepository<Psychologist>, Repository<Psychologist>>();
            services.AddScoped<IRepository<Client>, Repository<Client>>();
            services.AddScoped<IRepository<TimeSlot>, Repository<TimeSlot>>();
            
            services.AddScoped<IPsychologistRepository, PsychologistRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenUp v1"); });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
