using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Domain.Entities;

namespace SGDP.Core.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatienteService _patienteService;
        private readonly IResultLabRepository _resultLabRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppointmentViewModel _appointmentViewModel;

        public AppointmentService(IAppointmentRepository appointmentRepository, IHttpContextAccessor httpContextAccessor, IPatienteService patienteService, IResultLabRepository resultLabRepository)
        {
            _appointmentRepository = appointmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _appointmentViewModel = httpContextAccessor.HttpContext.Session.Get<AppointmentViewModel>("user");
            _patienteService = patienteService;
            _resultLabRepository = resultLabRepository;
        }


        public async Task Update(SaveAppointmentViewModel viewModel)
        {
            Appointment appointment = await _appointmentRepository.GetById(viewModel.Id);
            int patientId = appointment.PacienteId ?? throw new ArgumentNullException(nameof(appointment.PacienteId), "patientId cannot be null");
            var p = await _patienteService.GetByIdSaveViewModel(patientId);

            appointment.Id = viewModel.Id;
            appointment.Patiente = p.Name;
            appointment.Doctor = viewModel.Doctor;
            appointment.Date = viewModel.Date;
            appointment.Time = viewModel.Time;
            appointment.why = viewModel.why;
            appointment.NameTest = "Ninguna";
            if (viewModel.clave != null)
            {
                appointment.clave = viewModel.clave;
            }
            if (viewModel.IdNameTest != null)
            {
                appointment.LabTestId = viewModel.IdNameTest;
            }

            await _appointmentRepository.UpdateAsync(appointment);

        }

        public async Task<List<AppointmentViewModel>> GetAllViewModel()
        {
            var appointmentlist = await _appointmentRepository.GetAllAsync();
            return appointmentlist.Select(appintment => new AppointmentViewModel
            {
                Id = appintment.Id,
                Patiente = appintment.Patiente,
                Medico = appintment.Doctor,
                Date = appintment.Date,
                Time = appintment.Time,
                why = appintment.why,
                clave = appintment.clave,
            }).ToList();
        }

        public async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel viewModel)
        {

            int patientId = viewModel.patientId ?? throw new ArgumentNullException(nameof(viewModel.patientId), "patientId cannot be null");
            var p = await _patienteService.GetByIdSaveViewModel(patientId);

            Appointment appointment = new();
            appointment.Id = viewModel.Id;
            appointment.Patiente = p.Name;
            appointment.Doctor = viewModel.Doctor;
            appointment.Date = viewModel.Date;
            appointment.Time = viewModel.Time;
            appointment.PacienteId = p.Id;
            appointment.why = viewModel.why;
            appointment.NameTest = viewModel.NameTest;
            appointment.NameTest = "Ninguna";
            appointment.clave = 1;
            //appointment.medicoID = item.IdPatient;
       

            appointment = await _appointmentRepository.AddAsync(appointment);

            SaveAppointmentViewModel vm = new();
            vm.Id = appointment.Id;
            vm.PatienteName = appointment.Patiente;
            vm.Doctor = appointment.Doctor;
            vm.Date = appointment.Date;
            vm.Time = appointment.Time;
            vm.why = appointment.why;
            vm.NameTest = appointment.NameTest;
            vm.clave = appointment.clave;

            return vm;
        }

        public async Task<SaveAppointmentViewModel> GetByIdSaveViewModel(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            int patientId = appointment.PacienteId ?? throw new ArgumentNullException(nameof(appointment.PacienteId), "IdNameTest cannot be null");
            var patient = await _patienteService.GetByIdSaveViewModel(patientId);

            SaveAppointmentViewModel vm = new();
            vm.Id = appointment.Id;
            vm.PatienteName = appointment.Patiente;
            vm.Doctor = appointment.Doctor;
            vm.patientId = appointment.PacienteId;
            vm.Date = appointment.Date;
            vm.Time = appointment.Time;
            vm.CedulaPatient = patient.Cedula;
            vm.PatienteLastName = patient.LastName;
            vm.why = appointment.why;
            vm.NameTest = appointment.NameTest;
            vm.clave = appointment.clave;

            return vm;
        }

        public async Task Delete(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);
            var result = await _resultLabRepository.GetPatientIdByResultIdAsync(appointment.PacienteId);
            if (result != null)
            {
                await _resultLabRepository.DeleteAsync(result);
            }
            await _appointmentRepository.DeleteAsync(appointment);
        }
    }
}
