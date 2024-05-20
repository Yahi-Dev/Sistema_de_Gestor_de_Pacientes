using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Result;
using SGDP.Middlewares;

namespace SGDP.Controllers
{
    public class ResultController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IResultLabService _resultservice;

        public ResultController(ValidateUserSession validateUserSession, IResultLabService resultservice)
        {
            _validateUserSession = validateUserSession;
           _resultservice = resultservice;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(await _resultservice.GetAllViewModel());
        }

        public async Task<IActionResult> SendResult(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            var vm = await _resultservice.GetByIdSaveViewModel(id);
            
            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> SendResult(ResultLabViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            await _resultservice.CreateResult(vm);
            return RedirectToRoute(new { controller = "Result", action = "Index" });
        }


        public async Task<IActionResult>DeleteResult(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            var  p = await _resultservice.GetByIdSaveViewModel(id);
            return View("DeleteResult", p);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePostResult(ResultLabViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
             await _resultservice.Delete(vm.IdLabTest);
            return RedirectToRoute(new { controller = "Result", action = "Index" });
        }


        public async Task<IActionResult> ViewInfoResult(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            var p = await _resultservice.GetByInforesult(id);
            return View("ViewInfoResult", p);
        }
    }
}
