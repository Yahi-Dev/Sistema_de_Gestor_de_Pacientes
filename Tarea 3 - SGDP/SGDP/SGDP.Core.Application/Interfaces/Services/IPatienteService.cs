using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Medico;
using SGDP.Core.Application.ViewModels.Pacientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Application.Interfaces.Services
{
    public interface IPatienteService : IGenericService<SavePatientViewModel, PatientViewModel>
    {
        Task<List<ListPatient>> GetAllOnlyPatient();
    }
}
