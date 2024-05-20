using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Infrastructure.Persistence.Contexts;
using SGDP.Infrastructure.Persistence.Repositories;

namespace SGDP.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"), m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericsRepository<>));
            services.AddTransient<ILabTestRepository, LabTestRepository>();
            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IResultLabRepository, ResultLabRepository>();
            #endregion
        }
    }
}
