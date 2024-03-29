﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignalRProject.DataAccess.Entities;
using SignalRProject.DataAccess.Repositories.Interfaces;

namespace SignalRProject.DataAccess.Repositories.EntityFramework
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task Create(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task CreateRange(List<TEntity> items)
        {
            await _dbSet.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            var result = await _dbSet.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveRange(List<TEntity> items)
        {
            _dbSet.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
