using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;


namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = new List<T>(data);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync(T entity) {
            if (entity != null) {
                Data.Add(entity);
            }
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T updateEntity) {
            if (updateEntity != null) {
                var dataEntity = Data.FirstOrDefault(x => x.Id == updateEntity.Id);
                if (dataEntity != null) {
                    var index = Data.IndexOf(dataEntity);
                    Data[index] = updateEntity;

                    return Task.FromResult(updateEntity);
                }
            }
            // Если элемент не найден, возвращаем null:
            return Task.FromResult<T>(null);
        }

        public Task<bool> DeleteAsync(Guid id) {
            var entity = Data.FirstOrDefault(x => x.Id == id);
            if (entity != null) {
                // Удаляем найденный элемент:
                Data.Remove(entity);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}