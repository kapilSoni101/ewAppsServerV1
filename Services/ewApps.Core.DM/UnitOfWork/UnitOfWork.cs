﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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
using System.Threading.Tasks;
using System.Transactions;

namespace ewApps.Core.DMService {

  /// <summary>
  /// This class provides methods save enitty 
  /// </summary>
  /// <seealso cref="ewApps.Core.Data.IUnitOfWork" />
  public class UnitOfWorkDM:IUnitOfWorkDM {

    #region Local Members

    private readonly DMDBContext _dmdbContext;
    private bool _disableSaveChanges = false;


    #endregion Local Members

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="coreDbContext">The core database context.</param>
    /// <param name="disableSaveChanges">if set to <c>true</c> [disable save changes].</param>
    public UnitOfWorkDM(DMDBContext dmdbContext, bool disableSaveChanges= false) {
      _dmdbContext = dmdbContext;
      _disableSaveChanges = disableSaveChanges;
    }

    #endregion

    #region Public Methods 

    /// <summary>
    /// Saves this instance.
    /// </summary>
    public void Save() {
      if(_disableSaveChanges == false) {
        _dmdbContext.SaveChanges();
      }
    }

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <returns></returns>
    public async Task SaveAsync() {
      if(_disableSaveChanges == false) {
        await _dmdbContext.SaveChangesAsync();
      }
    }

    /// <summary>
    /// Saves all instances.
    /// </summary>
    /// <param name="enableSaveChnages">if set to <c>true</c> [enable save chnages].</param>
    public void SaveAll(bool enableSaveChnages = false) {
      try
      {
        _dmdbContext.SaveChanges();
      }
      catch (Exception ex)
      {

       // throw;
      }
     

      //if (_disableSaveChanges == false || enableSaveChnages == true) {

      //  using(var txscope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled)) {
      //    try {

      //      _dmdbContext.SaveChanges();

      //      txscope.Complete();
      //    }
      //    catch(Exception ex) {
      //      throw;
      //    }
      //  }
     // }
    }

    #endregion Public Methods

  }
}
