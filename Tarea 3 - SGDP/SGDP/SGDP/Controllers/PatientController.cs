using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Medico;
using SGDP.Core.Application.ViewModels.Pacientes;
using SGDP.Middlewares;

namespace SGDP.Controllers
{
    public class PatientController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;

        private readonly IPatienteService _patientService;

        public PatientController(ValidateUserSession validateUserSession, IPatienteService patientService)
        {
            _validateUserSession = validateUserSession;
            _patientService = patientService;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(await _patientService.GetAllViewModel());
        }


        public IActionResult RegisterPatient()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View("SavePatient", new SavePatientViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> RegisterPatient(SavePatientViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SavePatient", vm);
            }

            SavePatientViewModel patientVm = await _patientService.Add(vm);

            if (patientVm.Id != 0 && patientVm != null)
            {
                patientVm.ImagePathMedico = UploadFile(vm.FileImg, patientVm.Id);

                await _patientService.Update(patientVm);
            }

            return RedirectToRoute(new { controller = "Patient", action = "Index" });

        }



        public async Task<IActionResult> EditPatient(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SavePatientViewModel vm = await _patientService.GetByIdSaveViewModel(id);
            return View("SavePatient", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditPatient(SavePatientViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid && vm.FileImg != null && vm.ImagePathMedico != null)
            {
                return View("SavePatient", vm);
            }

            SavePatientViewModel saveVm = await _patientService.GetByIdSaveViewModel(vm.Id);
            vm.ImagePathMedico = UploadFile(vm.FileImg, saveVm.Id, true, saveVm.ImagePathMedico);

            await _patientService.Update(vm);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });


        }

        public async Task<IActionResult> DeletePatient(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View(await _patientService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePostPatient(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _patientService.Delete(id);

            string basePath = $"/Images/Patient/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> InfoPatient(int id)
        {
            var vm = await _patientService.GetByIdSaveViewModel(id);
            return View(vm);
        }
        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Patient/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
