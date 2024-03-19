using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using VideoService.Application;
using VideoService.Application.Abstractions;
using VideoService.Domain.Interfaces;
using VideoService.Persistence;
using VideoService.Persistence.Repositories;
using VideoService.Web.Settings;
using VideoSevice.Presentation.Consumers;

namespace VideoService.Web.Extension
{
    public static class ServiceExtencions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration, string connectionStringSectionName)
        {
            var connection = configuration.GetConnectionString(connectionStringSectionName);
            services.AddDbContext<VideoContext>(options =>
                                options.UseNpgsql(connection,
                                b => b.MigrationsAssembly(typeof(VideoContext).Assembly.GetName().Name)));
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
        }

        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, string elasticUriSection)
        {
            services.AddSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration[elasticUriSection]))
                    {
                        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                    .Enrich.WithProperty("Environment", environment.EnvironmentName)
                    .ReadFrom.Configuration(configuration);
            });
        }
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration, string massTransitSettingsSectionName)
        {
            var settings = configuration.GetSection(massTransitSettingsSectionName).Get<MassTransitSettings>();
            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<PatientCreatedConsumer>();
                x.AddConsumeObserver<ConsumeObserver>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.VirtualHost, h =>
                    {
                        h.Username(settings.UserName);
                        h.Password(settings.Password);
                    });
                    cfg.AddRawJsonSerializer();
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));
                });
            });
        }
    }
}
