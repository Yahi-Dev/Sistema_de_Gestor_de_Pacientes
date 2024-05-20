using SGDP.Core.Application.ViewModels.User;

namespace SGDP.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
        Task<bool> CheckUsernameExistsAsync(string username);
        Task<bool> CheckEditById(SaveUserViewModel vm);
    }
}
