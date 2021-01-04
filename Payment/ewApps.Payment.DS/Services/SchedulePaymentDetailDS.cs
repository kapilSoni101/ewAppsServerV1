
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>/Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ScheduledJobService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Recurring payment contains all schedule information.
    /// 1) Add recurring and Generate schedule job and add.
    /// 2) Make payment for each Schedule job.
    /// 3) Generate the log for each schedule payment.
    /// 4) Update the Recurring for each payment for nextSchedule payment and remaining schedule job. 
    /// </summary>
    public class SchedulePaymentDetailDS:BaseDS<SchedulePaymentDetail>, ISchedulePaymentDetailDS {

        #region Member variable

        ISchedulePaymentDetailRepository _recurringPaymentRepository;
        IPaymentUnitOfWork _unitOfWork;
        IUserSessionManager _usManager;

        IPayAppServiceDS _payAppServiceDS;
        PaymentAppSettings _appSetting;

        private const string SchedulePaymentDetailDocumentType = "SchedulePaymentDetail";

        #endregion Member variable

        #region Contructor

        /// <summary>Initializing local variablesInitializing local variables</summary>        
        public SchedulePaymentDetailDS(ISchedulePaymentDetailRepository recurringPaymentRepository,
            IPaymentUnitOfWork unitOfWork,
            IUserSessionManager usManager,
            IOptions<PaymentAppSettings> appSetting) : base(recurringPaymentRepository) {
            _recurringPaymentRepository = recurringPaymentRepository;
            _unitOfWork = unitOfWork;
            _usManager = usManager;
            _appSetting = appSetting.Value;
        }

        #endregion Constructor

        #region Get

        #endregion Get


    }

}

