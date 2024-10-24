﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = new List<T>(data);
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<T>>(Data);
        }

        public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
           var query = Data.AsQueryable();

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Select(include).AsQueryable() as IQueryable<T>;
                }
            }
            return await Task.FromResult(query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id));
        }

        public Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity != null)
            {
                Data.Add(entity);
            }
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T updateEntity, CancellationToken cancellationToken)
        {
            if (updateEntity != null)
            {
                var dataEntity = Data.FirstOrDefault(x => x.Id == updateEntity.Id);
                if (dataEntity != null)
                {
                    var index = Data.IndexOf(dataEntity);
                    Data[index] = updateEntity;

                    return Task.FromResult(updateEntity);
                }
            }
            // Если элемент не найден, возвращаем null:
            return Task.FromResult<T>(null);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = Data.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                // Удаляем найденный элемент:
                Data.Remove(entity);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}