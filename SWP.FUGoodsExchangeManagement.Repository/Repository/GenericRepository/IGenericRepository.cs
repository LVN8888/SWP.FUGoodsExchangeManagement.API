﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Repository.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);
        Task<TEntity> GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");

        Task<TEntity> GetByID(int id);
        Task Insert(TEntity entity);
        //Task Delete(object id);
        void Update(TEntity entityToUpdate);
        Task<int> Count(Expression<Func<TEntity, bool>> filter = null);
    }
}
