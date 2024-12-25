﻿using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DockerHubBackend.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<EntityEntry<TEntity>> Add(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        TEntity Update(TEntity entity);
        public void Delete(int id);
    }
}
