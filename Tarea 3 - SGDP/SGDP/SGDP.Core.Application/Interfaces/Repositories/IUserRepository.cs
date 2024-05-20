using SGDP.Core.Application.ViewModels.User;
using SGDP.Core.Domain.Entities;

namespace SGDP.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        Task<User> LoginAsync(LoginViewModel vm);

        Task<User> GetByUserNameAsync(string username);
    }
}
