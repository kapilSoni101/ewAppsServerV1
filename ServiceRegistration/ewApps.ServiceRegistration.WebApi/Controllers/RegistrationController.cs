/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 4 Sept 2019
 */
using  ewApps.ServiceRegistration.DS;
using  ewApps.ServiceRegistration.DTO;
using  ewApps.ServiceRegistration.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace  ewApps.ServiceRegistration.WebApi
{
  /// <summary>
  ///Api to support Service Registration CRUD operations
  /// </summary>
  [ApiController]
  [Route("api/ServiceRegistration")]
  [AllowAnonymous]
  public class RegistrationController : ControllerBase
  {

    #region Constructor and Private Member

    ILogger _logger;
    IRegistrationDS _serviceRegistrationDS;
    /// <summary>
    /// This is the constructor injecting app dataservice
    /// </summary>
    /// <param name="serviceRegistrationDS">Service Registration Data Service DI</param>
    public RegistrationController(IRegistrationDS serviceRegistrationDS)
    {
      _logger = Log.ForContext<RegistrationController>();
      _serviceRegistrationDS = serviceRegistrationDS;
      _logger.Information("[{Method}] Service initialized", "ServiceRegistryController");
    }
    #endregion

    #region CRUD

    /// <summary>
    /// Gets Service Regitration details 
    /// </summary>
    /// <param name="request">ServiceName and PreferenceCriteria</param>
    /// <param name="token">cancellation Token</param>
    /// <returns>Service Registration Info</returns>
    [HttpPost("getinfo")]
    public async Task<IActionResult> GetRegisteredServiceAsync(ServiceRequest request, CancellationToken token = default(CancellationToken))
    {
      _logger.Information("[{method}] Get ServiceRegistry Async-Begin", "GetServiceRegistryAsync");
      _logger.Debug("[{method}] is called with request {@request}", "GetServiceRegistryAsync", request);
      ServiceRegistry response = await _serviceRegistrationDS.GetRegisteredServiceAsync(request, token);
      _logger.Information("[{method}] Get ServiceRegistry Async-End", "GetServiceRegistryAsync");
      return Ok(response);
    }

    /// <summary>
    /// Gets Service Regitration details 
    /// </summary>
    /// <param name="serviceRefId">Unique Id of registered service/param>
    /// <param name="token">cancellation Token</param>
    /// <returns>Service Registration Info</returns>
    [HttpGet("serviceRefId")]
    public async Task<IActionResult> GetRegisteredServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {
      _logger.Information("[{method}] Get ServiceRegistry Async-Begin", "GetServiceRegistryAsync");
      _logger.Debug("[{method}] is called with request {@request}", "GetServiceRegistryAsync", serviceRefId);
      ServiceRegistry response = await _serviceRegistrationDS.GetRegisteredServiceAsync(serviceRefId, token);
      _logger.Information("[{method}] Get ServiceRegistry Async-End", "GetServiceRegistryAsync");
      return Ok(response);
    }

    /// <summary>
    /// Registeres the service
    /// </summary>
    /// <param name="serviceRegistery">Service Registration Object</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Registered service object</returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterServiceAsync([FromBody] ServiceRegistry serviceRegistery, CancellationToken token = default(CancellationToken))
    {
      _logger.Debug("[{Method}] Establish ServiceRegistry starts ", "RegisterService");
      ServiceRegistry serviceRegistry = await _serviceRegistrationDS.RegisterServiceAsync(serviceRegistery, token);
      _logger.Verbose("[{Method}] Processing ends establish ServiceRegistry- {@response} ", "RegisterService", serviceRegistry);
      _logger.Debug("[{Method}] Establish ServiceRegistry ends ", "RegisterService");

      return Ok(serviceRegistry);
    }

    /// <summary>
    /// Unregisters the Service.
    /// </summary>
    /// <param name="serviceRefId">service reference Id</param>
    /// <param name="token"> Cancellation Token</param>
    /// <returns>Status code  Deleted</returns>
    [HttpDelete("serviceRefId")]
    public async Task<IActionResult> UnregisterServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken))
    {
      _logger.Information("[{method}] Delete ServiceRegistry Async-Begin", "UnregisterServiceAsync");
      _logger.Debug("[{method}] is called with Shipper  {@request}", "UnregisterServiceAsync", serviceRefId);
      await _serviceRegistrationDS.UnregisterServiceAsync(serviceRefId, token);
      _logger.Information("[{method}] Delete Shipper Default Async-End", "UnregisterServiceAsync");
      return NoContent();
    }

    #endregion

  }
}
