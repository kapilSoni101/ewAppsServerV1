using ewApps.ServiceRegistration.DTO;
using ewApps.ServiceRegistration.Entity;
using System.Threading;
using System.Threading.Tasks;


namespace ewApps.ServiceRegistration.Data
{
  /// <summary>
  /// Interface for Service Registry repository
  /// </summary>
  public interface IRegistrationRepository
  {

    #region ServiceRegistry

    /// <summary>
    /// Gets the regsitered service from repository
    /// </summary>
    /// <param name="serviceRefId"> service reference id</param>
    /// <param name="token">cancellation token</param>
    /// <returns>Registered Service Object</returns>
    Task<ServiceRegistry> GetRegisteredServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Gets the registered service from repository
    /// </summary>
    /// <param name="request">Request with Service name and preference criteria</param>
    /// <param name="token">Cancellation Token</param>
    /// <returnsRegistered Service object></returns>
    Task<ServiceRegistry> GetRegisteredServiceAsync(ServiceRequest request, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Registers Service to the repository
    /// </summary>
    /// <param name="ServiceRegistry">sServiceRegistry object to be registered</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>registered Service object</returns> 
    Task<ServiceRegistry> RegisterServiceAsync(ServiceRegistry serviceRegistry, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Unregisters the service from repository
    /// </summary>
    /// <param name="serviceRefId"> Service reference Id</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Void</returns>

    Task UnregisterServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken));

    #endregion

    /// <summary>
    /// Saves the chnages made in the repository to the DB
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken token = default(CancellationToken));

  }
}