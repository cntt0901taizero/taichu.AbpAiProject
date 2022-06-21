using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BaseApplication.CacheManager;
using BaseApplication.Dtos;
using BaseApplication.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace BaseApplication.Factory
{
    public interface IAppFactory: IScopedDependency
    {
        //bool IsSuperAdmin();
        IMediator Mediator { get; }
        IUnitOfWork CurrentUnitOfWork { get; }
        IConfiguration AppSettingConfiguration { get; }
        IHostingEnvironment HostingEnvironment { get; }
        IRepository<TEntity, TPrimaryKey> Repository<TEntity, TPrimaryKey>()
            where TEntity : class, IEntity<TPrimaryKey>;
        IDbConnectionFactory DefaultDbFactory { get; }
        IObjectMapper ObjectMapper { get; }
        ITempFileCacheManager TempFileCacheManager { get; }
        ICurrentUser CurrentUser { get; }
        // Lấy thông tin co so , tinh, huyen,xa 
        //Task<UserSessionDto> GetUserSessionAsync();
        //UserSessionDto UserSession { get; }
        //Task ChangeUserSession(Guid? userId);
        ///// <summary>
        ///// Xóa cache UserSession
        ///// </summary>
        //void ClearCacheUserSession();
        //Task<bool> CheckPermissions(List<string> policies, bool isAnd = false, bool isThrowException = false, string errorMessage = "Không có quyền truy cập");
        //ILogger Logger { get; }
        IDistributedEventBus DistributedEventBus { get; }
        /// <summary>
        /// Lấy Service mà không cần injector
        /// </summary>
        /// <returns></returns>
        TService GetServiceDependency<TService>();
        /// <summary>
        /// Cache custom để lưu theo nhóm
        /// </summary>
        ICacheManager CacheManager { get; }
        IJsonSerializer JsonSerializer { get; }
        IGuidGenerator GuidGenerator { get; }
    }
}
