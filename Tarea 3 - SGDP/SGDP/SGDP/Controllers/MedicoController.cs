using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.User;
using SGDP.Middlewares;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.ViewModels.Medico;

namespace SGDP.Controllers
{
    public class MedicoController : Controller
    {

        private readonly ValidateUserSession _validateUserSession;

        private readonly IMedicoService _medicoservice;

        public MedicoController(ValidateUserSession validateUserSession, IMedicoService medicoService)
        {
            _validateUserSession = validateUserSession;
            _medicoservice = medicoService;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View( await _medicoservice.GetAllViewModel());
        }


        public IActionResult RegisterMedico()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View("SaveMedico", new SaveMedicoViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> RegisterMedico(SaveMedicoViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel medicoVm = await _medicoservice.Add(vm);

            if (medicoVm.Id != 0 && medicoVm != null)
            {
                medicoVm.ImagePath = UploadFile(vm.FileImg, medicoVm.Id);

                await _medicoservice.Update(medicoVm);
            }

            return RedirectToRoute(new { controller = "Medico", action = "Index" });
            
        }



        public async Task<IActionResult> EditMedico(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveMedicoViewModel vm = await _medicoservice.GetByIdSaveViewModel(id);
            return View("SaveMedico", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditMedico(SaveMedicoViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid && vm.FileImg != null && vm.ImagePath != null)
            {
                return View("SaveMedico", vm);
            }

            SaveMedicoViewModel saveVm = await _medicoservice.GetByIdSaveViewModel(vm.Id);
            vm.ImagePath = UploadFile(vm.FileImg, saveVm.Id, true, saveVm.ImagePath);

            await _medicoservice.Update(vm);
            return RedirectToRoute(new { controller = "Medico", action = "Index" });
            

        }

        public async Task<IActionResult> DeleteMedico(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View(await _medicoservice.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePostMedico(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _medicoservice.Delete(id);

            string basePath = $"/Images/Medico/{id}";
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
            return RedirectToRoute(new { controller = "Medico", action = "Index" });
        }

        public async Task<IActionResult> InfoMedico(int id)
        {
            var vm = await _medicoservice.GetByIdSaveViewModel(id);
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
            string basePath = $"/Images/Medico/{id}";
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

