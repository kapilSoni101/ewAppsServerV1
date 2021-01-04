using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using  ewApps.ServiceRegistration.DTO;
using  ewApps.ServiceRegistration.Entity;
using Serilog;

namespace  ewApps.ServiceRegistration.Data
{
  /// <summary>
  /// Service Registry Repository class
  /// </summary>
  public class RegistrationRepository : IRegistrationRepository
  {

    #region Constructor and private variables
    private AppDbContext _context;
    private ILogger _logger;
    private List<ServiceRegistry> _dataDictionary;
    private bool _dbRequired;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">App DB context</param>
    public RegistrationRepository(AppDbContext context)
    {
      _context = context;
      _logger = Log.ForContext<RegistrationRepository>();
      _dataDictionary = new List<ServiceRegistry>();
      _dbRequired = true;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbRequired">Boolean to specify the in memory data is used or DAtabase is used</param>
    public RegistrationRepository(bool dbRequired)
    {
      _context = null;
      _logger = Log.ForContext<RegistrationRepository>();
      _dataDictionary = new List<ServiceRegistry>();
      _dbRequired = false;
    }

    #endregion

    #region ServiceRegistry

    ///<inheritdoc/>
    public async Task<ServiceRegistry> GetRegisteredServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {
      if (_dbRequired)
      {
        ServiceRegistry shipperDefaults = _context.ServiceRegistry.FirstOrDefault(x => x.ServiceRefId == serviceRefId);
        return shipperDefaults;
      }
      else
      {
        ServiceRegistry shipperDefaults = _dataDictionary.Find(x => x.ServiceRefId == serviceRefId);
        return shipperDefaults;
      }
    }

    public async Task<ServiceRegistry> GetRegisteredServiceAsync(ServiceRequest req, CancellationToken token = default(CancellationToken))
    {
      if (string.IsNullOrEmpty(req.PreferenceCriteria))
      {
        if (_dbRequired)
        {
          ServiceRegistry shipperDefaults = _context.ServiceRegistry.FirstOrDefault(x => x.ServiceName == req.ServiceName);
          return shipperDefaults;
        }
        else
        {

          ServiceRegistry shipperDefaults = _dataDictionary.Find(x => x.ServiceName == req.ServiceName);
          return shipperDefaults;
        }
      }
      else
      {
        if (_dbRequired)
        {
          ServiceRegistry shipperDefaults = _context.ServiceRegistry.FirstOrDefault(x => x.ServiceName == req.ServiceName && x.PreferenceCriteria == req.PreferenceCriteria);
          return shipperDefaults;
        }
        else
        {
          ServiceRegistry shipperDefaults = _dataDictionary.Find(x => x.ServiceName == req.ServiceName && x.PreferenceCriteria == req.PreferenceCriteria);
          return shipperDefaults;
        }
      }
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
      if (_dbRequired)
      {
        await _context.ServiceRegistry.AddAsync(serviceRegistry, token);
      }
      else
      {
        _dataDictionary.Add(serviceRegistry);
      }
      return serviceRegistry;
    }
    ///<inheritdoc/>
    public async Task UnregisterServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {

      ServiceRegistry serviceRegistry = await GetRegisteredServiceAsync(serviceRefId, token);
      if (_dbRequired)
      {
        _context.ServiceRegistry.Remove(serviceRegistry);
        await _context.SaveChangesAsync(token);
      }
      else
      {
        _dataDictionary.Remove(serviceRegistry);
      }
     
      return;
    }

    #endregion

    ///<inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken token = default(CancellationToken))
    {
      if (_dbRequired)
        await _context.SaveChangesAsync(token);
    }
  }
}
