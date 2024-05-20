using Microsoft.AspNetCore.Mvc;
using SGDP.Middlewares;

namespace SGDP.Controllers
{
    public class AssistantController : Controller
    {
        private readonly ValidateUserSession _userSession;

        public AssistantController(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }
        public IActionResult Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View();
        }
    }
}
