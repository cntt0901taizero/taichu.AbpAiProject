using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace BaseApplication.Factory
{
    public interface IDbConnectionFactory : ITransientDependency, IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction DbTransaction { get; }
        /// <summary>
        /// commit DbTransaction và Dispose DbTransaction
        /// </summary>
        void Commit();
        /// <summary>
        /// Rollback DbTransaction và Dispose DbTransaction
        /// </summary>
        void Rollback();
    }
}
