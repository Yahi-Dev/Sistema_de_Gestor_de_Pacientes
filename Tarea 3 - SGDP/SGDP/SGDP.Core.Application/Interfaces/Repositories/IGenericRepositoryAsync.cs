using SGDP.Core.Domain.Entities;

namespace SGDP.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetById(int id);
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties);
    }
}
