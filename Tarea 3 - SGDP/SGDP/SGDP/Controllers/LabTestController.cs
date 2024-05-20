using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.LabTest;
using SGDP.Middlewares;

namespace SGDP.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly ILabTestService _labTestservice;

        public LabTestController(ValidateUserSession validateUserSession, ILabTestService labTestservice)
        {
            _validateUserSession = validateUserSession;
            _labTestservice = labTestservice;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(await _labTestservice.GetAllViewModel());
        }


        public IActionResult RegisterLabTest()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View("SaveLabTest", new SaveLabTestViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> RegisterLabTest(SaveLabTestViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", vm);
            }

            await _labTestservice.Add(vm);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });

        }



        public async Task<IActionResult> EditLabTest(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            SaveLabTestViewModel vm = await _labTestservice.GetByIdSaveViewModel(id);
            return View("SaveLabTest", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditLabTest(SaveLabTestViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveLabTest", vm);
            }

            await _labTestservice.Update(vm);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });


        }

        public async Task<IActionResult> DeleteLabTest(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            return View(await _labTestservice.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePostLabTest(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }

            await _labTestservice.Delete(id);
            return RedirectToRoute(new { controller = "LabTest", action = "Index" });
        }

    }
}
