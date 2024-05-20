using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.LabTest;

namespace SGDP.Core.Application.Interfaces.Services
{
    public interface ILabTestService : IGenericService<SaveLabTestViewModel, LabTestViewModel>
    {
        Task<List<ListLabTest>> GetAllOnlyLabTest();
    }
}
