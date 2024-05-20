using Microsoft.AspNetCore.Mvc;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.DashBoard;
using SGDP.Middlewares;
using SGDP.Models;
using System.Diagnostics;

namespace SGDP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _userService;
        private readonly IMedicoService _medicoService;
        private readonly ILabTestService _labTestService;

        public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUserSession, IUserService userService,
            IMedicoService medicoService, ILabTestService labTestService)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
            _userService = userService;
            _medicoService = medicoService;
            _labTestService = labTestService;
        }

        public async Task<IActionResult> Index()
        {
            DashBoardAdminViewModel vm = new();

            var infouser = await _userService.GetAllViewModel();
            var infomedico = await _medicoService.GetAllViewModel();
            var infolabtest = await _labTestService.GetAllViewModel();

            vm.UsersCount = infouser.Count;
            vm.MedicosCount = infomedico.Count;
            vm.ActiveLabTestsCount = infolabtest.Count;


            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
