﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoRedis.Services
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(ObjectId id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(FilterDefinition<T> filter, T entity);

        Task<bool> RemoveAsync(FilterDefinition<T> filter);
    }
}
