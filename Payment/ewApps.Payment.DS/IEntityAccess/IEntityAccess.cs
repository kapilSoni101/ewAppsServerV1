/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;

namespace ewApps.Payment.DS {

  public interface IEntityAccess<TEntity> where TEntity : class {

    bool[] AccessList(Guid entityId);

    bool CheckAccessForBusiness(int operation, Guid entityId);

    bool CheckAccessForPartner(int operation, Guid entityId);
  }
}