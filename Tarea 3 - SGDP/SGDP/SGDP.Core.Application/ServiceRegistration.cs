using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.Services;

namespace SGDP.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMedicoService, MedicoService>();
            services.AddTransient<ILabTestService, LabTestService>();
            services.AddTransient<IPatienteService, PatientService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IResultLabService, ResultLabService>();
            #endregion
        }
    }
}
