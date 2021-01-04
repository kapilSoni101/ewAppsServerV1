/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Sourabh Agarwal
 * Last Updated On: 29 November 2018
 */



using System.Threading.Tasks;

namespace ewApps.Shipment.Data {


  /// <summary>
  /// UnitOfWork for Shipment Enitities
  /// </summary>
  public interface IShipmentUnitOfWork {

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
    /// Saves all.
    /// </summary>
    /// <param name="enableSaveChnages">if set to <c>true</c> [enable save chnages].</param>
    void SaveAll(bool enableSaveChnages = false);
  }
}
