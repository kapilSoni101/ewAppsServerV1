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
  /// This class manage DBConnection for diffrent DBContext.
  /// </summary>
  /// <seealso cref="ewApps.Core.DbConProvider.IConnectionManager" />
  public class ConnectionManager:IConnectionManager {

    #region Local Members

    private SqlConnection _connection;

    #endregion  Local Members


    #region Public Methods

    public SqlConnection GetConnection(string conString) {
      if(_connection == null)
        _connection = new SqlConnection(conString);
      return _connection;
    }

    #endregion Public Methods
  }
}
