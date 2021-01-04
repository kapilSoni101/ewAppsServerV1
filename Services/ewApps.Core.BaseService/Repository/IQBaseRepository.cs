/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.BaseService {

    /// <summary>
    /// This interface provides generic methods to execute Get/Find/Add/Update/Delete query agianst respective entity type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of domain entity.</typeparam>
    public interface IQBaseRepository {

        Task<List<V>> GetQueryEntityListAsync<V>(string sql, object[] parameters, CancellationToken token = default(CancellationToken)) where V : class;


        /// <summary>
        /// Asynchronously creates a entity (of type V) by executing input raw SQL query and parameters.
        /// </summary>
        /// <typeparam name="V">The type parameter (other than domain entity) to be map with raw sql result.</typeparam>
        /// <param name="sql">The raw SQL query to be execute to get required System.Collections.Generic.List`1.</param>
        /// <param name="parameters">The values to be assigned to parameters in input raw sql.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>Returns a task that represents the asynchronous operation. The task result contains a entity or null if no record found.</returns>
        Task<V> GetQueryEntityAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class;

    }
}
