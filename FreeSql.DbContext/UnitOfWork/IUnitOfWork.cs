using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace FreeSql
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// 该对象 Select/Delete/Insert/Update/InsertOrUpdate 与工作单元事务保持一致，可省略传递 WithTransaction
        /// </summary>
        IFreeSql Orm { get; }

        /// <summary>
        /// 开启事务，或者返回已开启的事务
        /// </summary>
        /// <param name="isCreate">若未开启事务，则开启</param>
        /// <returns></returns>
        DbTransaction GetOrBeginTransaction(bool isCreate = true);

        /// <summary>
        /// 开启事务，或者返回已开启的事务
        /// </summary>
        /// <param name="isCreate">若未开启事务，则开启</param>
        /// <returns></returns>
        Task<DbTransaction> GetOrBeginTransactionAsync(bool isCreate = true);

        IsolationLevel? IsolationLevel { get; set; }

        void Commit();

        ValueTask CommitAsync();

        void Rollback();

        ValueTask RollbackAsync();

        /// <summary>
        /// 工作单元内的实体变化跟踪
        /// </summary>
        DbContext.EntityChangeReport EntityChangeReport { get; }

        /// <summary>
        /// 用户自定义的状态数据，便于扩展
        /// </summary>
        Dictionary<string, object> States { get; }
    }
}