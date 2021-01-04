using ewApps.ServiceRegistration.DTO;
using ewApps.ServiceRegistration.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.ServiceRegistration.DS
{
  public interface IRegistrationDS
  {
    ///<summary>
    /// Gets registered service Detail
    /// </summary>
    /// <param name="serviceRefId">Servire reference number</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>ServiceRegistry Details</returns>
    Task<ServiceRegistry> GetRegisteredServiceAsync(string serviceRefId, CancellationToken token = default(CancellationToken));

    ///<summary>
    /// Gets registered service Detail
    /// </summary>
    /// <param name="serviceRefId">Servire reference number</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>ServiceRegistry Details</returns>
    Task<ServiceRegistry> GetRegisteredServiceAsync(ServiceRequest request, CancellationToken token = default(CancellationToken));

    ///<summary>
    /// Registers Service
    /// </summary>
    /// <param name="ServiceRegistry">ServiceRegistry details</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Registered Service object</returns>
    Task<ServiceRegistry> RegisterServiceAsync(ServiceRegistry serviceRegistry, CancellationToken token = default(CancellationToken));

    ///<summary>
    /// Unregisters Service 
    /// </summary>
    /// <param name="servireRefId">servireRefId</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>void</returns>
    Task UnregisterServiceAsync(string servireRefId, CancellationToken token = default(CancellationToken));
  }
}
