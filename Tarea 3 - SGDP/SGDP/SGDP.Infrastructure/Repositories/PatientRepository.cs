using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Domain.Entities;
using SGDP.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Infrastructure.Persistence.Repositories
{
    public class PatientRepository : GenericsRepository<Patient>, IPatientRepository
    {
        private readonly ApplicationContext _dbcontext;
        public PatientRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbcontext = applicationContext;
        }
    }
}
