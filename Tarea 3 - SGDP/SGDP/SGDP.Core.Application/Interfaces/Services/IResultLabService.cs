using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Result;
using SGDP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Application.Interfaces.Services
{
    public interface IResultLabService : IGenericService<SaveAppointmentViewModel, LabTestResultViewModel>
    {
        Task UpdateAndCreateResult(SaveAppointmentViewModel viewModel);
        Task CreateResult(ResultLabViewModel vm);
        Task<ResultLabViewModel> GetByIdSaveViewModel(int id);
        Task<LabTestResultViewModel> GetByInforesult(int id);
    }
}
