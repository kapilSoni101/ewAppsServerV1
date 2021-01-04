/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 25 Jun 2018
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

/// <summary>
/// Common Response DTO from connector 
/// </summary>
  public class ConectorResDTO
  {
    /// <summary>
    /// Request status - Sucess/Failure 
    /// </summary>
    public string ResponseStatus { get; set; }
    /// <summary>
    /// Any data return by Carrier, It has value for UPS carrier has error
    /// </summary>
    public object ResponsePayload { get; set; }
    /// <summary>
    /// Response object from shipment Connector
    /// </summary>
    public object Error { get; set; }
  }

 
}
