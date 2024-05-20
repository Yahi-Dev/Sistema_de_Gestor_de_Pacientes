using Microsoft.EntityFrameworkCore;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Domain.Entities;
using SGDP.Infrastructure.Persistence.Contexts;
using System.ComponentModel.DataAnnotations;

namespace SGDP.Infrastructure.Persistence.Repositories
{
    public class GenericsRepository<Entity> : IGenericRepositoryAsync<Entity> where Entity : class
    {
        private readonly ApplicationContext _dbcontext;


        public GenericsRepository(ApplicationContext applicationContext)
        {
            _dbcontext = applicationContext;
        }


        //Agregar
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbcontext.Set<Entity>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }



        //Editar
        public virtual async Task UpdateAsync(Entity entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }


        //Borrar
        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbcontext.Set<Entity>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        //Devolver todos los productos guardados
        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbcontext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _dbcontext.Set<Entity>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetById(int id)
        {
            return await _dbcontext.Set<Entity>().FindAsync(id);
        }
    }
}
