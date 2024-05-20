using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Result;
using SGDP.Middlewares;

namespace SGDP.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IMedicoService _medicoservice;
        private readonly IPatienteService _patientservice;
        private readonly IAppointmentService _appointmentservice;
        private readonly ILabTestService _LabTestservice;
        private readonly IResultLabService _resultLab;

        public AppointmentController(
        ValidateUserSession validateUserSession,
        IAppointmentService appointmentservice,
        IMedicoService medicoservice,
        IPatienteService patientservice,
        IResultLabService resultLab,
        ILabTestService LabTestservice)
        {
            _validateUserSession = validateUserSession;
            _appointmentservice = appointmentservice;
            _patientservice = patientservice;
            _medicoservice = medicoservice;
            _resultLab = resultLab; 
            _LabTestservice = LabTestservice;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(await _appointmentservice.GetAllViewModel());
        }


        public async Task<IActionResult> RegisterAppointment()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            SaveAppointmentViewModel vm = new();
            vm.ListMedico = await _medicoservice.GetAllOnlyMedico();
            vm.ListPatient = await _patientservice.GetAllOnlyPatient();

            return View("SaveAppointment", vm);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAppointment(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            vm.ListMedico = await _medicoservice.GetAllOnlyMedico();
            vm.ListPatient = await _patientservice.GetAllOnlyPatient();

            if (!ModelState.IsValid)
            {
                return View("SaveAppointment", vm);
            }

            await _appointmentservice.Add(vm);

            return RedirectToRoute(new { controller = "Appointment", action = "Index" });

        }



        public async Task<IActionResult> AssignAppointment(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveAppointmentViewModel vm = await _appointmentservice.GetByIdSaveViewModel(id);
            vm.ListLabTest =await _LabTestservice.GetAllOnlyLabTest();
            return View("AssignAppointment", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AssignAppointment(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            await _resultLab.UpdateAndCreateResult(vm);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> ConsultAppointment(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            SaveAppointmentViewModel viewmodel = await _appointmentservice.GetByIdSaveViewModel(id);
            return View("ConsultAppointment", viewmodel);
        }
        public async Task<IActionResult> EditAppointment(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveAppointmentViewModel vm = await _appointmentservice.GetByIdSaveViewModel(id);
            vm.ListMedico = await _medicoservice.GetAllOnlyMedico();
            vm.ListPatient = await _patientservice.GetAllOnlyPatient();
            return View("SaveAppointment", vm);
        }


        [HttpPost]
        public async Task<IActionResult> EditAppointment(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            vm.ListMedico = await _medicoservice.GetAllOnlyMedico();
            vm.ListPatient = await _patientservice.GetAllOnlyPatient();

            if (!ModelState.IsValid)
            {
                return View("SaveAppointment", vm);
            }
            await _appointmentservice.Update(vm);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }


        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View(await _appointmentservice.GetByIdSaveViewModel(id));
        }



        [HttpPost]
        public async Task<IActionResult> DeletePostAppointment(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _appointmentservice.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }
    }
}
