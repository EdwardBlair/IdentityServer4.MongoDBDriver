// Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Common.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Common.MongoDBRepository
{
    public abstract class MongoDBRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public MongoDBRepository(IMongoDBRepositoryOptions<MongoDBRepository<TEntity>> options)
        {
            if (options == null)
            {
                throw new NullReferenceException(nameof(options));
            }

            IMongoDatabase database = options.Database;

            if (database == null)
            {
                if (options.ConnectionString == null)
                {
                    throw new NullReferenceException(nameof(options.ConnectionString));
                }

                var url = new MongoUrl(options.ConnectionString);

                database = new MongoClient(url).GetDatabase(url.DatabaseName);
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
            {
                BsonClassMap.RegisterClassMap(ClassMapInitializer);
            }

            _collection = database.GetCollection<TEntity>(options.CollectionName);

            CollectionInitializer.Invoke(_collection);
        }

        protected virtual Action<BsonClassMap<TEntity>> ClassMapInitializer => (classMap) => { classMap.AutoMap(); };

        protected virtual Action<IMongoCollection<TEntity>> CollectionInitializer => (collection) => { };

        public virtual async Task<long> DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var deleteResult = await _collection.DeleteManyAsync(predicate);
            return deleteResult.DeletedCount;
        }

        public virtual async Task<long> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var deleteResult = await _collection.DeleteOneAsync(predicate);
            return deleteResult.DeletedCount;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var results = new List<TEntity>();

            using (var cursor = await _collection.FindAsync(predicate))
            {
                while (await cursor.MoveNextAsync())
                {
                    results.AddRange(cursor.Current);
                }
            }

            return results;
        }

        public virtual async Task<IEnumerable<TEntity>> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
            return entities;
        }

        public virtual async Task<TEntity> InsertOneAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            var result = await _collection.FindOneAndReplaceAsync(predicate, entity, new FindOneAndReplaceOptions<TEntity, TEntity>() { IsUpsert = true, ReturnDocument = ReturnDocument.After });
            return result;
        }
    }
}
