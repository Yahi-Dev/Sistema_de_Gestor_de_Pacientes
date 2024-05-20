using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Medico;
using SGDP.Core.Application.ViewModels.Pacientes;
using SGDP.Core.Domain.Entities;


namespace SGDP.Core.Application.Services
{
    public class PatientService : IPatienteService
    {
        private readonly IPatientRepository _patientreposotiry;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PatientViewModel _patientViewModel;

        public PatientService(IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor)
        {
           _patientreposotiry = patientRepository;
            _httpContextAccessor = httpContextAccessor;
            _patientViewModel = httpContextAccessor.HttpContext.Session.Get<PatientViewModel>("user");
        }


        public async Task Update(SavePatientViewModel viewModel)
        {
            Patient patiente = await _patientreposotiry.GetById(viewModel.Id);
            patiente.Id = viewModel.Id;
            patiente.Name = viewModel.Name;
            patiente.LastName = viewModel.LastName;
            patiente.ImagePathMedico = viewModel.ImagePathMedico;
            patiente.Phone =  viewModel.Phone;
            patiente.Direccion = viewModel.Direccion;
            patiente.Cedula = viewModel.Cedula;
            patiente.Sex = viewModel.Sex;
            patiente.FechaDeNacimiento = viewModel.FechaDeNacimiento;
            patiente.Question1 = viewModel.Question1;
            patiente.Question2 = viewModel.Question2;

            await _patientreposotiry.UpdateAsync(patiente);
        }

        public async Task<List<PatientViewModel>> GetAllViewModel()
        {
            var patientlist = await _patientreposotiry.GetAllAsync();
            return patientlist.Select(patient => new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                LastName = patient.LastName,
                ImagePathMedico = patient.ImagePathMedico,
                Phone = patient.Phone,
                Cedula = patient.Cedula,
                Sex = patient.Sex,
                FechaDeNacimiento = patient.FechaDeNacimiento
            }).ToList();
        }

        public async Task <List<ListPatient>> GetAllOnlyPatient()
        {
            var p = await _patientreposotiry.GetAllAsync();
            var list = new List<ListPatient>();
            foreach (var item in p)
            {
                list.Add(new ListPatient()
                {
                    IdPatient = item.Id,
                    PatientName = item.Name,
                    PatientLastName = item.LastName,
                });
            }
            return list;
        }

        public async Task<SavePatientViewModel> Add(SavePatientViewModel viewModel)
        {
            Patient patiente = new();
            patiente.Id = viewModel.Id;
            patiente.Name = viewModel.Name;
            patiente.LastName = viewModel.LastName;
            patiente.ImagePathMedico = viewModel.ImagePathMedico;
            patiente.Phone =  viewModel.Phone;
            patiente.Direccion = viewModel.Direccion;
            patiente.Cedula = viewModel.Cedula;
            patiente.Sex = viewModel.Sex;
            patiente.FechaDeNacimiento = viewModel.FechaDeNacimiento;
            patiente.Question1 = viewModel.Question1;
            patiente.Question2 = viewModel.Question2;

            patiente = await _patientreposotiry.AddAsync(patiente);

            SavePatientViewModel vm = new();

            vm.Id = patiente.Id;
            vm.Name = patiente.Name;
            vm.LastName = patiente.LastName;
            vm.ImagePathMedico = patiente.ImagePathMedico;
            vm.Phone = patiente.Phone;
            vm.Direccion = patiente.Direccion;
            vm.Cedula = patiente.Cedula;
            vm.Sex = patiente.Sex;
            vm.FechaDeNacimiento = patiente.FechaDeNacimiento;
            vm.Question1 = patiente.Question1;
            vm.Question2 = patiente.Question2;


            return vm;
        }


        public async Task<SavePatientViewModel> GetByIdSaveViewModel(int id)
        {
            var patiente = await _patientreposotiry.GetById(id);

            SavePatientViewModel vm = new();
            vm.Id = patiente.Id;
            vm.Name = patiente.Name;
            vm.LastName = patiente.LastName;
            vm.ImagePathMedico = patiente.ImagePathMedico;
            vm.Phone = patiente.Phone;
            vm.Direccion = patiente.Direccion;
            vm.Cedula = patiente.Cedula;
            vm.Sex = patiente.Sex;
            vm.FechaDeNacimiento = patiente.FechaDeNacimiento;
            vm.Question1 = patiente.Question1;
            vm.Question2 = patiente.Question2;

            return vm;
        }

        public async Task Delete(int id)
        {
            var patient = await _patientreposotiry.GetById(id);
            await _patientreposotiry.DeleteAsync(patient);
        }
    }
}
