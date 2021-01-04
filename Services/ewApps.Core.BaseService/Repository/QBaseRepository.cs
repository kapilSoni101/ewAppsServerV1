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
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ewApps.Core.BaseService {

    /// <summary>
    /// This is the base Repository class for all entities. It provides basic, common 
    /// database operations.
    /// </summary>
    /// <typeparam name="TEntity">The type of the target entity.</typeparam>
    /// <typeparam name="U">The type of DbContext.</typeparam>
    /// <seealso cref="ewApps.Core.Data.IQBaseRepository" />
    public class QBaseRepository<U>: IQBaseRepository where U : DbContext {

        #region Local Members

        /// <summary>
        /// Instance of DbContext to execute sql query.
        /// </summary>
        protected U _context;        

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QBaseRepository{U}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public QBaseRepository(U context) {
            _context = context;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Asynchronously gets the entity (of type V) list based on input SQL and parameters.
        /// </summary>
        /// <typeparam name="V">The type of entity of result.</typeparam>
        /// <param name="sql">A valid SQL string that should contains all properties of TEntity entity.</param>
        /// <param name="parameters">The list of sql parameters (if null none of sql parameters applied).</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>Returns entity (of type V) list based on input SQL and parameters.</returns>
        public async Task<List<V>> GetQueryEntityListAsync<V>(string sql, object[] parameters, CancellationToken token = default(CancellationToken)) where V : class {
            IQueryable<V> querable;

            if(parameters != null && parameters.Length > 0) {
                querable = _context.Query<V>().FromSql(sql, parameters);
            }
            else {
                querable = _context.Query<V>().FromSql(sql);
            }

            return await querable.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<V> GetQueryEntityAsync<V>(string sql, object[] parameters = null, CancellationToken token = default(CancellationToken)) where V : class {
            if(parameters != null && parameters.Length > 0)
                return await _context.Query<V>().FromSql(sql, parameters).FirstOrDefaultAsync<V>();
            else {
                return await _context.Query<V>().FromSql(sql).FirstOrDefaultAsync<V>();
            }
        }

        #endregion Get

    }
}
