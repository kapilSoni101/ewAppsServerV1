  using System; 
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.AppPortal.Data;
using ewApps.Core.UserSessionService;
using ewApps.AppPortal.DTO;
using System.Threading;

namespace ewApps.AppPortal.DS
{

  /// <summary>
  /// This class implements standard business logic and operations for Vendor entity.
  /// </summary>
  public class VendorDS : BaseDS<Vendor>, IVendorDS
  {

    #region Local Member 

    IVendorRepository _repository;
    IUserSessionManager _sessionmanager;
        IQConfigurationDS _qConfigurationDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sessionmanager"></param>
        public VendorDS(IVendorRepository repository, IUserSessionManager sessionmanager, IQConfigurationDS qConfigurationDS) : base(repository)
    {
      _repository = repository;
      _sessionmanager = sessionmanager;
            _qConfigurationDS = qConfigurationDS;
    }

        #endregion Constructor

        #region Get Configration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<VendorConfigurationDTO> GetVendorConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken cancellationToken = default(CancellationToken)) {
            VendorConfigurationDTO vendorConfigurationDTO = await _qConfigurationDS.GetVendorConfigurationDetailAsync(buspartnertenantid, cancellationToken);

            return vendorConfigurationDTO;
        }


        #endregion Get

        #region Add/Update/Delete



        #endregion Add/Update/Delete

    }

}