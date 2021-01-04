/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ewApps.Core.CommonService
{
  /// <summary>
  /// This class implements standard business logic and operations for Resource Helper.
  /// </summary>

  public class ResourceHelper
  {

    #region public methods 
    /// <summary>
    /// Gets the localized resouce.
    /// </summary>
    /// <typeparam name="T">Requested resource type.</typeparam>
    /// <param name="sateliteAssemblyName">Name of the satelite assembly.</param>
    /// <param name="resourceBaseName">Name of the resource base.</param>
    /// <param name="languageCode">The language code.</param>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns></returns>
    public static T GetLocalizedResouce<T>(string sateliteAssemblyName, string resourceBaseName, string languageCode, string resourceName) {
      Assembly myAssembly = Assembly.Load(sateliteAssemblyName);
      ResourceManager resourceManager = new ResourceManager(resourceBaseName, myAssembly);
      CultureInfo ci = new CultureInfo(languageCode);
      return (T)resourceManager.GetObject(resourceName, ci);
    } 
    #endregion

  }
}
