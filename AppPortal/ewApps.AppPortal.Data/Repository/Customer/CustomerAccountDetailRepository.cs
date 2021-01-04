using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.Data {
   public  class CustomerAccountDetailRepository:BaseRepository<CustomerAccountDetail, AppPortalDbContext>, ICustomerAccountDetailRepository {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sessionManager">The session manager.</param>
        public CustomerAccountDetailRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {

        }

        ///<inheritdoc/>
        public async Task<List<CustomerAccountDTO>> GetCustomerAccListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT custacc.ID,custacc.AccountType,custacc.AccountJson
                                    FROM ap.CustomerAccountDetail as custacc where custacc.CustomerId  = @customerId And custacc.Deleted = @deleted  ");

            SqlParameter parameterS = new SqlParameter("@customerId", customerId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);

            List<CustomerAccountDTO> customerAccountDTOs = await GetQueryEntityListAsync<CustomerAccountDTO>(query, new object[] { parameterS, parameterDeleted });
            return customerAccountDTOs;

        }
        #endregion
    }
}
