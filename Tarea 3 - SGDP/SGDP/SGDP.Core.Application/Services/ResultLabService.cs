using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Result;
using SGDP.Core.Domain.Entities;


namespace SGDP.Core.Application.Services
{
    public class ResultLabService : IResultLabService
    {
        private readonly IPatientRepository _patientreposotiry;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppointmentViewModel _appointViewModel;
        private readonly ILabTestRepository _labTestRepository;
        private readonly IResultLabRepository _resultLab;
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRepository _appointmentRepository;
        public ResultLabService(IPatientRepository patientRepository, IHttpContextAccessor httpContextAccessor
        ,ILabTestRepository labTestRepository, IResultLabRepository resultLab, IAppointmentRepository appointmentRepository,
         IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            _appointmentRepository = appointmentRepository;
            _resultLab = resultLab;
            _patientreposotiry = patientRepository;
            _httpContextAccessor = httpContextAccessor;
            _labTestRepository = labTestRepository;
            _appointViewModel = httpContextAccessor.HttpContext.Session.Get<AppointmentViewModel>("user");
        }

        public async Task UpdateAndCreateResult(SaveAppointmentViewModel viewModel)
        {
            int labTestId = viewModel.IdNameTest ?? throw new ArgumentNullException(nameof(viewModel.IdNameTest), "IdNameTest cannot be null");
            var r = await _labTestRepository.GetById(labTestId);

            var a = await _appointmentService.GetByIdSaveViewModel(viewModel.Id);

            int patientId = a.patientId ?? throw new ArgumentNullException(nameof(a.patientId), "patientId cannot be null");
            var p = await _patientreposotiry.GetById(patientId);

            var save = await _appointmentRepository.GetById(viewModel.Id);

            Result result = new();
            result.NamePatient = p.Name;
            result.LastNamePatient = p.LastName;
            result.cedulapatient = p.Cedula;
            result.patientId = p.Id;
            result.AppointmentId = save.Id;
            result.NameLabTest = r.TestName;
            result.clave = 2;
            result.ResultTest = "Proceso";

            a.NameTest = r.TestName;
            await _resultLab.AddAsync(result);

            a.clave = 2;
            a.IdNameTest = viewModel.IdNameTest;
            await _appointmentService.Update(a);
        }
        public async Task CreateResult(ResultLabViewModel vm)
        {
            var r = await _resultLab.GetById(vm.IdLabTest);

            r.ResultTest = vm.Result;
            r.clave = 3;

           await _resultLab.UpdateAsync(r);

            SaveAppointmentViewModel vmAppointment = new();

            int appointmentId = r.AppointmentId ?? throw new ArgumentNullException(nameof(r.AppointmentId), "AppointmentId cannot be null");

            var p = await _appointmentRepository.GetById(appointmentId);
            vmAppointment.Id = p.Id;
            vmAppointment.PatienteName = p.Patiente;
            vmAppointment.Doctor = p.Doctor;
            vmAppointment.Date = p.Date;
            vmAppointment.Time = p.Time;
            vmAppointment.why = p.why;
            vmAppointment.NameTest = p.NameTest;
            vmAppointment.clave = 3;

            await _appointmentService.Update(vmAppointment);
        }


        public async Task<List<LabTestResultViewModel>> GetAllViewModel()
        {
            var resultlist = await _resultLab.GetAllAsync();
            return resultlist.Select(result => new LabTestResultViewModel
            {
                Id = result.Id,
                NamePatient = result.NamePatient,
                LastNamePatient = result.LastNamePatient,
                CedulaPatient = result.cedulapatient,
                LabTestName = result.NameLabTest,
                Clave = result.clave,
            }).ToList();
        }

        public async Task<ResultLabViewModel> GetByIdSaveViewModel(int id)
        {
            var p = await _resultLab.GetById(id);

            ResultLabViewModel vm = new();
            vm.IdLabTest = p.Id;
            vm.Result = p.ResultTest;
            return vm;
        }


        public async Task<LabTestResultViewModel> GetByInforesult(int id)
        {
            var appointment = await _appointmentService.GetByIdSaveViewModel(id);
            var result = await _resultLab.GetPatientIdByResultIdAsync(appointment.patientId);

            LabTestResultViewModel vm = new();
            vm.NamePatient = result.NamePatient;
            vm.LastNamePatient = result.LastNamePatient;
            vm.CedulaPatient = result.cedulapatient;
            vm.LabTestName = result.NameLabTest;
            vm.ResultTest = result.ResultTest;
            return vm;
        }



        public Task Update(SaveAppointmentViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel viewModel)
        {
            throw new NotImplementedException();
        }


        public async Task Delete(int id)
        {
            var result =  await _resultLab.GetById(id);
            await _resultLab.DeleteAsync(result);
        }



        Task<SaveAppointmentViewModel> IGenericService<SaveAppointmentViewModel, LabTestResultViewModel>.GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
