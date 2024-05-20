using Microsoft.EntityFrameworkCore;
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
    public class ResultLabRepository : GenericsRepository<Result>, IResultLabRepository
    {
        private readonly ApplicationContext _dbcontext;

        public ResultLabRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbcontext = applicationContext;
        }

        public async Task<Result> GetPatientIdByResultIdAsync(int? resultId)
        {
            var result = await _dbcontext.Results
                .FirstOrDefaultAsync(r => r.patientId == resultId);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
