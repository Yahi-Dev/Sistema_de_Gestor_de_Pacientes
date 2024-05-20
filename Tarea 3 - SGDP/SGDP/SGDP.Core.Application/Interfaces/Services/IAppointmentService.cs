using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.LabTest;
using SGDP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Application.Interfaces.Services
{
    public interface IAppointmentService : IGenericService<SaveAppointmentViewModel, AppointmentViewModel>
    {
    }
}
