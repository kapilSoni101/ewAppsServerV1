using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Contains all required customer SignUp methods.
    /// </summary>
    public interface ICustomerSignUpDS {

        /// <summary>
        /// Signup Customer .
        /// <summary>
        /// </summary>
        /// <param name="customerSignUpDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerSignUpResDTO> CustomerSignUpAsync(List<CustomerSignUpReqDTO> customerSignUpDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Signup customer.
        /// Method will use in standalone case.
        /// </summary>
        /// <param name="signupCustDTO"></param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<CustomerSignUpResDTO> SignUpCustomerAsync(SignUpBACustomerDTO signupCustDTO, CancellationToken token = default(CancellationToken));

    }
}
