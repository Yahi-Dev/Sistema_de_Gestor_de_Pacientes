using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.User;
using SGDP.Core.Domain.Entities;


namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _userViewModel;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }


        public async Task<UserViewModel> Login(LoginViewModel vm)
        {

            User user  = await _userRepository.LoginAsync(vm);

            if (user == null)
            {
                return null;
            }
            UserViewModel uservm = new();
            uservm.Id = user.Id;
            uservm.Name = user.Name;
            uservm.LastName = user.LastName;
            uservm.Email = user.Email;
            uservm.Image = user.ImagePathMedico;
            uservm.UserName = user.UserName;
            uservm.TypeUser = user.TypeUser;

            return uservm;
        }

        public async Task Update(SaveUserViewModel viewModel)
        {
            User user = await _userRepository.GetById(viewModel.Id);
            user.Id = viewModel.Id;
            user.Name = viewModel.Name;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.UserName = viewModel.UserName;
            user.TypeUser = viewModel.TypeUser;
            user.ImagePathMedico = viewModel.ImagePath;
            if (viewModel.Password != null)
            {
                user.Password = viewModel.Password;
                await _userRepository.UpdateAsync(user);
            }
            else
            {
                await _userRepository.UpdateAsync(user);
            }

        }
        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userlist = await _userRepository.GetAllAsync();
            return userlist.Select(user => new UserViewModel{ 
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Image = user.ImagePathMedico,
                TypeUser = user.TypeUser,
            }).ToList();
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            var existingUser = await _userRepository.GetByUserNameAsync(username);
            return existingUser != null;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel viewModel)
        {
            User user = new();
            user.Id = viewModel.Id;
            user.Name = viewModel.Name;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.UserName = viewModel.UserName;
            user.TypeUser = viewModel.TypeUser;
            user.Password = viewModel.Password;
            user.ImagePathMedico = viewModel.ImagePath;


            user  = await _userRepository.AddAsync(user);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.UserName = user.UserName;
            vm.TypeUser = user.TypeUser;
            vm.ImagePath = user.ImagePathMedico;
            vm.Password = user.Password;

            return vm;
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetById(id);

            SaveUserViewModel vm = new();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.UserName = user.UserName;
            vm.TypeUser = user.TypeUser;
            vm.Password = user.Password;
            vm.ImagePath = user.ImagePathMedico;

            return vm;
        }

        public async Task<bool>CheckEditById(SaveUserViewModel vm)
        {
            var user = await _userRepository.GetById(vm.Id);

            if (user.UserName == vm.UserName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            await _userRepository.DeleteAsync(user);
        }

    }
}