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

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Transaction Journal Report
    /// </summary>
    public interface IQTransactionJournalReportDS :IBaseDS<BaseDTO>  
  {


    /// <summary>
    /// Get All Tranaction Journal Report for Payment Tracing  
    /// </summary>
    /// <returns></returns>
    Task<List<TransactionJournalReportDTO>> GetTransactionJournalListAsync(ListFilterDTO filter);
  }
}
