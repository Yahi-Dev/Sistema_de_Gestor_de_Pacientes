using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.ViewModels.User;
using SGDP.Core.Application.Helpers;

namespace SGDP.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor httpContext;


        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel vm = httpContext.HttpContext.Session.Get<UserViewModel>("user");
            if (vm == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
