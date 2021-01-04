using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CacheService;
using  ewApps.ServiceRegistration.DTO;
using  ewApps.ServiceRegistration.Entity;
using Serilog;

namespace  ewApps.ServiceRegistration.Data
{
  /// <summary>
  /// Service Registry Repository class, it uses EwAppsDistributedCache DI for storing the Objects
  /// </summary>
  public class RegistrationCacheRepository : IRegistrationRepository
  {

    #region Constructor and private variables
   
    private ILogger _logger;
    private IEwAppsDistributedCache _cache;
   
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">App DB context</param>
    public RegistrationCacheRepository(IEwAppsDistributedCache cache)
    {
    
      _logger = Log.ForContext<RegistrationCacheRepository>();

      _cache = cache;
    }

   

    #endregion

    #region ServiceRegistry

    ///<inheritdoc/>
    public async Task<ServiceRegistry> GetRegisteredServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {
      return await _cache.GetAsync<ServiceRegistry>(serviceRefId, token);
    }

    public async Task<ServiceRegistry> GetRegisteredServiceAsync(ServiceRequest req, CancellationToken token = default(CancellationToken))
    {
      return null;
    }

    ///<inheritdoc/>
    public async Task<ServiceRegistry> RegisterServiceAsync(ServiceRegistry serviceRegistry, CancellationToken token = default(CancellationToken))
    {
      //Check if service exists already 
      ServiceRequest request = new ServiceRequest();
      request.ServiceName = serviceRegistry.ServiceName;
      request.PreferenceCriteria = serviceRegistry.PreferenceCriteria;
      ServiceRegistry s = await GetRegisteredServiceAsync(request, token);
      if (s != null) return s;

      //else add it to DB
      if (serviceRegistry.ID == Guid.Empty)
      {
        serviceRegistry.ID = Guid.NewGuid();
      }
      //else add it to DB
      if (string.IsNullOrEmpty(serviceRegistry.ServiceRefId))
      {
        serviceRegistry.ServiceRefId = serviceRegistry.ID.ToString();
      }
      await _cache.SetAsync<ServiceRegistry>(serviceRegistry.ServiceRefId, serviceRegistry,null, token);
      return serviceRegistry;
    }
    ///<inheritdoc/>
    public async Task UnregisterServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {

      await _cache.RemoveAsync(serviceRefId, token);
     
      return;
    }

    #endregion

    ///<inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken token = default(CancellationToken))
    {
      
    }
  }
}
