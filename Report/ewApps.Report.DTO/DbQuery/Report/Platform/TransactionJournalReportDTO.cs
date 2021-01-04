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

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Report.DTO
{

  /// <summary>
  /// This class is a DTO with consolidate information of <see cref="TransactionJournalReportDTO"/> .
  /// </summary>
  public class TransactionJournalReportDTO
  {

    /// <summary>
    /// System generated unique public number.
    /// </summary>
    public string PublisherId {
      get; set;
    }

    /// <summary>
    /// Name of the Publisher .
    /// </summary>
    public string PublisherName {
      get; set;
    }

    /// <summary>
    /// System generated unique business number.
    /// </summary>
    public string BusinessId {
      get; set;
    }

    /// <summary>
    /// Name of the Business .
    /// </summary>
    public string BusinessName {
      get; set;
    }

    /// <summary>
    /// System generated unique Customer number.
    /// </summary>
    public string CustomerId {
      get; set;
    }

    /// <summary>
    /// Name of the Customer .
    /// </summary>
    public string CustomerName {
      get; set;
    }

    /// <summary>
    /// System generated unique Payee number.
    /// </summary>
    public string PayeeId {
      get; set;
    }

    /// <summary>
    /// Name of the Payee(TenantUser) .
    /// </summary>
    public string PayeeUserName {
      get; set;
    }

    /// <summary>
    /// Type of the Payee(Ex BUsiness or Customer) .
    /// </summary>
    public string PayeeUserType {
      get; set;
    }

    /// <summary>
    /// System generated unique Transaction number.
    /// </summary>
    public string TransactionId {
      get; set;
    }

    /// <summary>
    /// Status of the Transaction .
    /// </summary>
    public string TransactionStatus {
      get; set;
    }

    /// <summary>
    /// Date and Time of the Transaction .
    /// </summary>
    public DateTime TransactionDateTime {
      get; set;
    }

    /// <summary>
    /// The transaction value (or amount)
    /// </summary>
    public decimal TransactionAmount {
      get; set;
    }

    /// <summary>
    /// Name of the Service And Attribute.
    /// </summary>
    public string ServiceAttributeName {
      get; set;
    }
 

    /// <summary>
    /// Client Ip address where Payment done 
    /// </summary>
    public string ClientIP {
      get; set;
    }

    /// <summary>
    /// Client Browser Information where Payment Done 
    /// </summary>
    public string ClientBrowser {
      get; set;
    }


    /// <summary>
    /// Client Operating System Information where Payment Done 
    /// </summary>
    public string ClientOS {
      get; set;
    }






  }
}
