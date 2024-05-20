using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Medico;

namespace SGDP.Core.Application.Interfaces.Services
{
    public interface IMedicoService : IGenericService<SaveMedicoViewModel, MedicoViewModel>
    {
        Task<List<ListMedico>> GetAllOnlyMedico();
    }
}
