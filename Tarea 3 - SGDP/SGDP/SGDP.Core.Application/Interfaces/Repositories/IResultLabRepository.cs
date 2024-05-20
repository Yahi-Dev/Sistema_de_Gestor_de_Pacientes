﻿using SGDP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Application.Interfaces.Repositories
{
    public interface IResultLabRepository : IGenericRepositoryAsync<Result>
    {
        Task<Result> GetPatientIdByResultIdAsync(int? resultId);
    }
}
