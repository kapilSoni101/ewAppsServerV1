﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Sourabh Agarwal
 * Last Updated On: 29 November 2018
 */


using System;
using System.Threading.Tasks;
using System.Transactions;

namespace ewApps.Shipment.Data {

    public class ShipmentUnitOfWork:IShipmentUnitOfWork {

        #region Local Members

        private readonly ShipmentDbContext _appDbContext;
//        private readonly IUnitOfWork _coreUnitOfWork;

        private bool _disableSaveChanges = false;


        #endregion Local Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentUnitOfWork"/> class.
        /// </summary>
        /// <param name="appDbContext">The application database context.</param>
        /// <param name="coreDbContext">The core database context.</param>
        /// <param name="disableSaveChanges">if set to <c>true</c> [disable save changes].</param>
        public ShipmentUnitOfWork(ShipmentDbContext appDbContext,  bool disableSaveChanges) {
            _appDbContext = appDbContext;
//            _coreUnitOfWork = coreUnitOfWork;
            _disableSaveChanges = disableSaveChanges;
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save() {
            if(_disableSaveChanges == false) {
                _appDbContext.SaveChanges();
            }
        }


        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync() {
            if(_disableSaveChanges == false) {
                await _appDbContext.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Saves all.
        /// </summary>
        /// <param name="enableSaveChnages">if set to <c>true</c> [enable save chnages].</param>
        public void SaveAll(bool enableSaveChnages = false) {

            if(_disableSaveChanges == false || enableSaveChnages == true) {

                using(var txscope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled)) {
                    try {

                        _appDbContext.SaveChanges();

                        //_coreUnitOfWork.SaveAll(true);

                        txscope.Complete();
                    }
                    catch(Exception) {
                        throw;
                    }
                }
            }
        }


        #endregion Public Methods
    }
}
