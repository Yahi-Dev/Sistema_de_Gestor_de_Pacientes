using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Domain.Entities;
using SGDP.Infrastructure.Persistence.Contexts;

namespace SGDP.Infrastructure.Persistence.Repositories
{
    public class LabTestRepository : GenericsRepository<LabTests>, ILabTestRepository
    {
        private readonly ApplicationContext _dbcontext;

        public LabTestRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbcontext = applicationContext;
        }
    }
}
