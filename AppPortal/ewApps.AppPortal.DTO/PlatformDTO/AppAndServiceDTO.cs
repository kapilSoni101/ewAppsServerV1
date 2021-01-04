//Response

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.DMService;

namespace ewApps.AppPortal.DTO {

  /// <summary>
  /// Class providing properties for application and its service details.
  /// </summary>
  public class AppAndServiceDTO {

    /// <summary>
    /// Application details.
    /// </summary>
    public AppDetailDTO AppDetailDTO
    {
      get;
      set;
    }

    /// <summary>
    /// Thumbnail details of the application
    /// </summary>
    public ThumbnailAddAndUpdateDTO ThumbnailAddAndUpdateDTO
    {
      get;
      set;
    }

    /// <summary>
    /// List of the services asociated with the application
    /// </summary>
    public List<AppServiceDTO> AppServiceDTOs
    {
      get;
      set;
    }
     [NotMapped]
    public List<AppServiceDTO> AppServiceModelList {
      get;
      set;
    }

  }
}
