using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.User;
using SGDP.Middlewares;
using SGDP.Core.Application.Helpers;

namespace SGDP.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userservice;
        private readonly ValidateUserSession _validateUserSession;
        public UserController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userservice = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }

        public IActionResult Login()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            UserViewModel uservm = await _userservice.Login(vm);

            if (uservm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", uservm);
                if (uservm.TypeUser == "Administrador")
                {                
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Assistant", action = "Index" });
                }
            }
            else
            {
                ModelState.AddModelError("newuser", "Datos de acceso incorrecto");
            }
            return View(vm);
        }

        public async Task<IActionResult> Usuarios()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(await _userservice.GetAllViewModel());
        }


        public IActionResult RegisterUser()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View("SaveUsuario", new SaveUserViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            var CheckExist = await _userservice.CheckUsernameExistsAsync(vm.UserName);

            if (CheckExist)
            {
                ViewBag.Check = "Ya existe un Nombre de usuario con ese nombre, elige otro.";
                return View("SaveUsuario", vm);
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View("SaveUsuario", vm);
                }

                SaveUserViewModel userVm = await _userservice.Add(vm);

                if (userVm.Id != 0 && userVm != null)
                {
                    userVm.ImagePath = UploadFile(vm.FileImg, userVm.Id);

                    await _userservice.Update(userVm);
                }

                return RedirectToRoute(new { controller = "User", action = "Usuarios" });
            }
        }



        public async Task<IActionResult> EditUser(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveUserViewModel vm = await _userservice.GetByIdSaveViewModel(id);
            return View("SaveUsuario", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            var CheckExist = await _userservice.CheckEditById(vm);

            if (CheckExist)
            {
                if (!ModelState.IsValid && vm.FileImg != null && vm.ImagePath != null && vm.ConfirmPassword != null)
                {
                    return View("SaveUsuario", vm);
                }

                SaveUserViewModel userVm = await _userservice.GetByIdSaveViewModel(vm.Id);
                vm.ImagePath = UploadFile(vm.FileImg, userVm.Id, true, userVm.ImagePath);

                await _userservice.Update(vm);
                return RedirectToRoute(new { controller = "User", action = "Usuarios" });
            }
            else
            {
                ViewBag.Check = "Ya existe un Nombre de usuario con ese nombre, elige otro.";
                return View("SaveUsuario", vm);
            }
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View(await _userservice.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePostUser(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _userservice.Delete(id);

            string basePath = $"/Images/User/{id}";
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
            return RedirectToRoute(new { controller = "User", action = "Usuarios" });
        }

        public async Task<IActionResult> InfoUser(int id)
        {
            var vm = await _userservice.GetByIdSaveViewModel(id);
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
            string basePath = $"/Images/User/{id}";
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
