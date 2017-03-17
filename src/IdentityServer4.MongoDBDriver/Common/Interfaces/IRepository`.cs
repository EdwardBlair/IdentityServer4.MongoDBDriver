// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRepository<TEntity> : IRepository
    {
        Task<long> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<long> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> InsertManyAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Returns the updated entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertOneAsync(TEntity entity);

        /// <summary>
        /// Returns the updated entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity);
    }
}
