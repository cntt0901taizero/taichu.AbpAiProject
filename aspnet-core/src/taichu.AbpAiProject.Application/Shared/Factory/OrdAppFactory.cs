using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using BaseApplication.CacheManager;
using BaseApplication.Dtos;
using BaseApplication.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace BaseApplication.Factory
{
    public class AppFactory : IAppFactory
    {
        #region LazyGetRequiredService
        protected IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new object();
        public TService GetServiceDependency<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }

        public AppFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        protected TService LazyGetRequiredService<TService>(ref TService reference)
            => LazyGetRequiredService(typeof(TService), ref reference);

        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference != null) return reference;
            lock (ServiceProviderLock)
            {
                if (reference == null)
                {
                    reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                }
            }
            return reference;
        }
        #endregion

        #region Repository
        private Dictionary<Type, object> _repositories;
        public IRepository<TEntity, TPrimaryKey> Repository<TEntity, TPrimaryKey>() where TEntity : class, IEntity<TPrimaryKey>
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = ServiceProvider.GetService(typeof(IRepository<TEntity, TPrimaryKey>));
            }
            return (IRepository<TEntity, TPrimaryKey>)_repositories[type];
        }
        #endregion

        private IConfiguration _configuration;
        public IConfiguration AppSettingConfiguration => _configuration ?? LazyGetRequiredService(ref _configuration);

        private IDbConnectionFactory _defaultConn;
        public IDbConnectionFactory DefaultDbFactory
        {
            get
            {
                if (_defaultConn == null)
                {
                    _defaultConn = new DbConnectionFactory(AppSettingConfiguration.GetConnectionString("Default"));
                }

                return _defaultConn;
            }
        }

        private IMediator _mediator;
        public IMediator Mediator => _mediator ?? LazyGetRequiredService(ref _mediator);

        private IUnitOfWorkManager _unitOfWorkManager;
        protected IUnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService(ref _unitOfWorkManager);
        public IUnitOfWork CurrentUnitOfWork => UnitOfWorkManager?.Current;

        private IObjectMapper _objectMapper;
        public IObjectMapper ObjectMapper => LazyGetRequiredService(ref _objectMapper);

        private ITempFileCacheManager _tempFileCacheManager;
        public ITempFileCacheManager TempFileCacheManager => LazyGetRequiredService(ref _tempFileCacheManager);

        private IHostingEnvironment _env;
        public IHostingEnvironment HostingEnvironment => LazyGetRequiredService(ref _env);

        private ICurrentUser _currentUser;
        public ICurrentUser CurrentUser => LazyGetRequiredService(ref _currentUser);

        //private UserSessionDto _niisUserSessionDto;
        //public async Task<UserSessionDto> GetUserSessionAsync()
        //{
        //    try
        //    {
        //        if (CurrentUser == null || CurrentUser?.Id.HasValue != true)
        //        {
        //            return null;
        //        }
        //        if (_niisUserSessionDto == null)
        //        {
        //            _niisUserSessionDto = await Mediator.Send(new GetUserSession.Query()
        //            {
        //                UserId = CurrentUser.Id.Value
        //            });
        //        }
        //        return _niisUserSessionDto;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}

        //public async Task ChangeUserSession(Guid? userId)
        //{
        //    if (userId.HasValue)
        //    {
        //        // check _niisUserSessionDto có giá trị UserId = userId thì return giữ nguyên giá trị của _niisUserSessionDto
        //        if (_niisUserSessionDto != null && _niisUserSessionDto.UserId == userId)
        //        {
        //            return;
        //        }
        //        var currentPrincipalAccessor = GetServiceDependency<ICurrentPrincipalAccessor>();
        //        var newPrincipal = new ClaimsPrincipal(
        //            new ClaimsIdentity(
        //                new Claim[]
        //                {
        //                    new Claim(AbpClaimTypes.UserId, userId.ToString()),
        //                }
        //            )
        //        );
        //        using (currentPrincipalAccessor.Change(newPrincipal))
        //        {
        //            _niisUserSessionDto = null;
        //            await GetUserSessionAsync();
        //        }
        //    }
        //}

        //public UserSessionDto UserSession
        //{
        //    get
        //    {
        //        if (_niisUserSessionDto == null)
        //        {
        //            _niisUserSessionDto = AsyncHelper.RunSync(async () => await GetUserSessionAsync());
        //        }
        //        return _niisUserSessionDto;
        //    }
        //}

        //public void ClearCacheUserSession()
        //{
        //    var user = this.CurrentUser;
        //    var cacheUserSession = GetServiceDependency<IDistributedCache<UserSessionDto>>();
        //    cacheUserSession.Remove("UserSession_" + user.Id);
        //}

        protected IDistributedEventBus distributedEventBus;
        public IDistributedEventBus DistributedEventBus => LazyGetRequiredService(ref distributedEventBus);

        private ICacheManager _cacheManager;
        public ICacheManager CacheManager => LazyGetRequiredService(ref _cacheManager);

        private IJsonSerializer _jsonSerializer;
        public IJsonSerializer JsonSerializer => LazyGetRequiredService(ref _jsonSerializer);

        private IGuidGenerator _guidGenerator;
        public IGuidGenerator GuidGenerator => LazyGetRequiredService(ref _guidGenerator);

        //#region Logger
        //public ILogger Logger => _lazyLogger.Value;
        //private Lazy<ILogger> _lazyLogger => new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);
        //protected ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);
        //private ILoggerFactory _loggerFactory;

        //private bool? _isSuperAdmin;
        //public bool IsSuperAdmin()
        //{
        //    if (!_isSuperAdmin.HasValue)
        //    {
        //        _isSuperAdmin = UserSession?.IsSuperAdmin ?? false;
        //    }
        //    return _isSuperAdmin.Value;
        //}

        //#endregion

        //#region Kiểm tra quyền
        ///// <summary>
        ///// Kiểm tra tài khoản hiện tại có danh sách quyền không?
        ///// </summary>
        ///// <param name="policies"> Danh sách quyền cần kiểm tra</param>
        ///// <param name="isAnd">Kiểm tra điều kiện và (tất cả các quyền trong danh sách phải được gán cho user). 
        ///// Nếu isAnd = false thì chỉ cần 1 quyền trong danh sách thỏa mãn là đủ.</param>
        ///// <param name="isThrowException">Ném ngoại lệ trong hàm</param>
        ///// <returns></returns>
        //public async Task<bool> CheckPermissions(List<string> policies, 
        //    bool isAnd = false,
        //    bool isThrowException = false,
        //    string errorMessage = "Không có quyền truy cập")
        //{
        //    var users = await GetUserSessionAsync();
        //    if (users == null)
        //    {
        //        return ReturnFalseCheckPermissions(isThrowException, errorMessage);
        //    }
        //    // policies trống return true
        //    if (policies?.Any() != true)
        //    {
        //        return true;
        //    }
        //    // là super admin return true
        //    if (users.IsSuperAdmin)
        //    {
        //        return true;
        //    }
        //    var grantedPolicies = users?.GrantedPermissions ?? new List<string>();
        //    if (grantedPolicies?.Any() != true)
        //    {
        //        return ReturnFalseCheckPermissions(isThrowException, errorMessage);
        //    }
        //    if (isAnd)
        //    {
        //        foreach (var policy in policies)
        //        {
        //            if (string.IsNullOrEmpty(policy))
        //            {
        //                continue;
        //            }
        //            var authorized = grantedPolicies.Contains(policy.Trim());
        //            // và
        //            if (!authorized)
        //            {
        //                return ReturnFalseCheckPermissions(isThrowException, errorMessage);
        //            }
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        foreach (var policy in policies)
        //        {
        //            if (string.IsNullOrEmpty(policy))
        //            {
        //                continue;
        //            }
        //            var authorized = grantedPolicies.Contains(policy.Trim());
        //            // hoặc (có permission thì return ra luôn)
        //            if (authorized)
        //            {
        //                return true;
        //            }
        //        }
        //        return ReturnFalseCheckPermissions(isThrowException, errorMessage);
        //    }
        //}

        //private bool ReturnFalseCheckPermissions(bool isThrowException, string errorMessage)
        //{
        //    if (isThrowException)
        //    {
        //        throw new UserFriendlyException(errorMessage);
        //    }
        //    return false;
        //}
        //#endregion
    }
}
