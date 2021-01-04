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


using System.Threading.Tasks;

namespace ewApps.AppMgmt.Data {


  /// <summary>
  /// UnitOfWork Interface for Core Enitty
  /// </summary>
  public interface IUnitOfWork {

    /// <summary>
    /// Saves this instance.
    /// </summary>
    void Save();

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();

    /// <summary>
    /// Saves all instances.
    /// </summary>
    /// <param name="enableSaveChnages">if set to <c>true</c> [enable save chnages].</param>
    /// <returns></returns>
    void SaveAll(bool enableSaveChnages = false);
  }
}
