/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Asha Sharda
 * Date: 4 July 2019
 *
 */

using System.Threading;
using System.Threading.Tasks;
using ewApps.ServiceRegistration.Entity;
using ewApps.ServiceRegistration.Data;
using Serilog;
using System.Collections.Generic;
using System.Net.Http;
using System;
using ewApps.ServiceRegistration.DTO;

namespace ewApps.ServiceRegistration.DS
{
  /// <inheritdoc/>
  public class RegistrationDS : IRegistrationDS
  {

    #region Constructor and private variables

    private ILogger _logger;
    private IRegistrationRepository _eRepository;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="eRepository">App ServiceRegistry Repository</param>
    public RegistrationDS(IRegistrationRepository eRepository)
    {
      _logger = Log.ForContext<RegistrationDS>();
      _eRepository = eRepository;
      _logger.Information("[{Method}] Service initialized", "ServiceRegistryDS");
    }

    #endregion

    #region CRUD
    /// <inheritdoc/>
    public async Task<ServiceRegistry> GetRegisteredServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {
      _logger.Debug("[{method}] Calls data service with serviceRefId {@serviceRefId}", "GetServiceRegistryAsync", serviceRefId);
      ServiceRegistry ServiceRegistry = await _eRepository.GetRegisteredServiceAsync(serviceRefId, token);
      _logger.Debug("[{method}] call responsed with response {@response}", "GetServiceRegistryAsync", ServiceRegistry);
      return ServiceRegistry;
    }
    /// <inheritdoc/>
    public async Task<ServiceRegistry> GetRegisteredServiceAsync(ServiceRequest request, CancellationToken token = default(CancellationToken))
    {
      _logger.Debug("[{method}] Calls data service with request {@request}", "GetServiceRegistryAsync", request);
      ServiceRegistry ServiceRegistry = await _eRepository.GetRegisteredServiceAsync(request, token);
      _logger.Debug("[{method}] call responsed with response {@response}", "GetServiceRegistryAsync", ServiceRegistry);
      return ServiceRegistry;
    }
    /// <inheritdoc/>
    public async Task<ServiceRegistry> RegisterServiceAsync(ServiceRegistry serviceRegistry, CancellationToken token = default(CancellationToken))
    {
      if (string.IsNullOrEmpty(serviceRegistry.ServiceRefId)) serviceRegistry.ServiceRefId = Guid.NewGuid().ToString();
      _logger.Debug("[{method}] Adds default with Shipper {@request}", "EstablishServiceRegistryAsync", serviceRegistry);
      //Check for Existance, ifexist return the existing object
      ServiceRegistry service = await IsExists(serviceRegistry, token);
      if (service != null) return service;
      //Else store the Data and provide the new object
      ServiceRegistry registeredServiceInfo = await _eRepository.RegisterServiceAsync(serviceRegistry, token);
      await _eRepository.SaveChangesAsync(token);
      _logger.Debug("[{method}] Adds default with response {@response}", "AddServiceRegistryAsync", registeredServiceInfo);
      return registeredServiceInfo;

    }

    /// <inheritdoc/>
    public async Task UnregisterServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {
      _logger.Debug("[{method}] Calls data service with serviceRefId {@serviceRefId} ", "DeleteServiceRegistryAsync", serviceRefId);
      await _eRepository.UnregisterServiceAsync(serviceRefId, token);
      await _eRepository.SaveChangesAsync(token);
    }




    #endregion

    private async Task<ServiceRegistry> IsExists(ServiceRegistry serviceRegistry, CancellationToken token = default(CancellationToken))
    {
      ServiceRegistry s = await GetRegisteredServiceAsync(serviceRegistry.ServiceRefId, token);
      if (s != null) return s;

      ServiceRequest request = new ServiceRequest();
      request.ServiceName = serviceRegistry.ServiceName;
      if (serviceRegistry.PreferenceCriteria != null)
        request.PreferenceCriteria = serviceRegistry.PreferenceCriteria;
      s = await GetRegisteredServiceAsync(request, token);
      return s;
    }


  }
}
