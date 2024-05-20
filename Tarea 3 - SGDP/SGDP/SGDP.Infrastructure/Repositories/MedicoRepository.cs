using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Domain.Entities;
using SGDP.Infrastructure.Persistence.Contexts;

namespace SGDP.Infrastructure.Persistence.Repositories
{
    public class MedicoRepository : GenericsRepository<Medico>, IMedicoRepository
    {
        private readonly ApplicationContext _dbcontext;

        public MedicoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbcontext = applicationContext;
        }
    }
}
