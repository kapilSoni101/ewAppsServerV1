/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 2 May 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 2 May 2019
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Transaction Journal Report
    /// </summary>
    public class QTransactionJournalReportDS:BaseDS<BaseDTO>, IQTransactionJournalReportDS {

        #region Local Member
        IQTransactionJournalReportRepository _transactionJournalReportRepository;

        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="transactionJournalReportRepository"></param>
        public QTransactionJournalReportDS(IQTransactionJournalReportRepository transactionJournalReportRepository) : base(transactionJournalReportRepository) {
            _transactionJournalReportRepository = transactionJournalReportRepository;
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<List<TransactionJournalReportDTO>> GetTransactionJournalListAsync(ListFilterDTO filter) {
            return await _transactionJournalReportRepository.GetTransactionJournalListAsync(filter);
        }

    }
}
