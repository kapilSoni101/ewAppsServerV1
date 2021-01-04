using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.ServiceProcessor {
    /// <summary>
    /// Service executor class to execute request based on supported input options.
    /// </summary>
    public class ServiceExecutor {

        #region Local Members

        private string _serviceRegistryServiceKey;

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceExecutor"/> class.
        /// </summary>
        /// <param name="serviceRegistryServiceKey">The service key to get base execution method from service registry.</param>
        public ServiceExecutor(string serviceRegistryServiceKey) {
            _serviceRegistryServiceKey = serviceRegistryServiceKey;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        ///  Asynchronously executes service request based on input options.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="useCache">if set to <c>true</c> [use cache].</param>
        /// <returns>Return requested object of type T.</returns>
        /// <exception cref="InvalidOperationException">
        /// Raise when input Request Type is not supported
        /// or
        /// Raise when input protocol is not supported.
        /// </exception>
        public async Task<T> ExecuteAsync<T>(RequestOptions requestOptions, bool useCache) {

            // REST
            if(requestOptions.ProtocolType == ServiceProtocolTypeEnum.REST) {
                HttpServiceExecutor httpServiceExecutor = new HttpServiceExecutor(_serviceRegistryServiceKey);
                return await httpServiceExecutor.ExecuteAsync<T>(requestOptions, false);
            }
            else {
                throw new InvalidOperationException("Invalid Protocol Type");
            }
        }

        /// <summary>
        /// Executes service request based on input options.
        /// </summary>
        /// <typeparam name="T">Response data type.</typeparam>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="useCache">if set to <c>true</c> [use cache].</param>
        /// <returns>Return requested object of type T.</returns>
        /// <exception cref="InvalidOperationException">
        /// Raise when input Request Type is not supported
        /// or
        /// Raise when input protocol is not supported.
        /// </exception>
        public T Execute<T>(RequestOptions requestOptions, bool useCache) {
            if(requestOptions.ProtocolType == ServiceProtocolTypeEnum.REST) {
                HttpServiceExecutor httpServiceExecutor = new HttpServiceExecutor(_serviceRegistryServiceKey);
                return httpServiceExecutor.Execute<T>(requestOptions, false);
            }
            else {
                throw new InvalidOperationException("Invalid Protocol Type");
            }
        }

        #endregion Public Methods
    }
}
