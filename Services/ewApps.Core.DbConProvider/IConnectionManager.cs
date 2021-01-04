/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 16 May 2019
 */

using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace ewApps.Core.DbConProvider {

  /// <summary>
  /// This interface implements methods to manage DBConnection for diffrent DBContext.
  /// </summary>
  /// <seealso cref="ewApps.Core.DbConProvider.IConnectionManager" />
  public interface IConnectionManager {

    /// <summary>
    /// Gets the connection.
    /// </summary>
    /// <param name="conString">The con string.</param>
    /// <returns></returns>
    SqlConnection GetConnection(string conString);
  }
}
