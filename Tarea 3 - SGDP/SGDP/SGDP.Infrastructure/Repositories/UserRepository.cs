using Microsoft.EntityFrameworkCore;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.ViewModels.User;
using SGDP.Core.Domain.Entities;
using SGDP.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericsRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbcontext;

        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbcontext = applicationContext;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            return await base.AddAsync(entity);
        }

        public override async Task UpdateAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            await base.UpdateAsync(entity);
        }

        public async Task<User> LoginAsync(LoginViewModel vm)
        {
            string passwordEncryp = PasswordEncryption.ComputeSha256Hash(vm.Password);
            User user = await _dbcontext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == vm.UserName && user.Password == passwordEncryp);

            return user;
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _dbcontext.Set<User>().FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
