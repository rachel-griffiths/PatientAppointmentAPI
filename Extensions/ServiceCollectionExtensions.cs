using PatientAppointmentAPI.Core.Interfaces;
using PatientAppointmentAPI.Core.Interfaces.Repositories;
using PatientAppointmentAPI.Core.Services;
using PatientAppointmentAPI.Infrastructure.Repositories;

namespace PatientAppointmentAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        //Register repositories
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Register services
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        return services;
    }
}