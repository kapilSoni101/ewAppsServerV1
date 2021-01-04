/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using ewApps.Core.BaseService;
using Microsoft.Extensions.Options;

namespace ewApps.Core.DMService {

    public class DMThumbnailDS:BaseDS<DMThumbnail>, IDMThumbnailDS {

    #region member varaible

    IDMThumbnailRepository _thumbnailRep;
        DMServiceSettings _thumbnailUrl;

    #endregion

    #region Constructor

    public DMThumbnailDS(IDMThumbnailRepository thumbnailRep, IOptions<DMServiceSettings> appSetting) 
: base(thumbnailRep) {
      _thumbnailRep = thumbnailRep;
      _thumbnailUrl = appSetting.Value;
    }

    #endregion Constructor
  }
}
