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
using System.IO;
using System.Text;

namespace ewApps.Core.CommonService 
{ /// <summary>
                               /// IT does io and cache operation to get Required XSLT file
                               /// </summary>
  public static class XSLTHelper {
    // Engg: where it should be?
    #region member variable 
    private const string EDSateliteAssemblyName = "ewApps.ED.Common";
    // Engg: where it should be?
    private const string EDSateliteAssemblyResourceName = "ewApps.ED.Common.Resources.EDResource";
    private const string EventEmailXsltFileKey = "EventEmailXsltFileKey";
    private const string EventPushXsltFileKey = "EventPushXsltFileKey";
    #endregion

    #region public methods 
    /// <summary>
    /// Gets the Push  XSLT file based on user language from the cache if available
    /// otherwise load from the file system.
    /// </summary>
    /// <param name="regionLanguage">user language</param>
    /// <returns></returns>
    public static string GetPushXsltFile(string regionLanguage) {
      string cacheKey = string.Format("{0}-{1}", EventPushXsltFileKey, regionLanguage);
      if (CacheHelper.IsInCache(cacheKey)) {
        return CacheHelper.GetData<string>(cacheKey); ;
      }
      else {
        //Engg: should define in Config file.
        string xsltResourcePath = "/resourcePath/";
        string xsltTextFile = FileHelper.ReadFileAsText(GetPUSHLocalizedTemplatePath(regionLanguage, xsltResourcePath));
        //CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
        return xsltTextFile;
      }
    }



    // <summary>
    /// Gets the XSLT file based on user language from the cache if available
    /// otherwise load from the file system.
    /// </summary>
    /// <param name="regionLanguage">user language</param>
    /// <returns></returns>
    public static string GetEmailXsltFile(string regionLanguage) {
      //string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
      //if (CacheHelper.IsInCache(cacheKey)) {
      //  return CacheHelper.GetData<string>(cacheKey); ;
      //}
      //else {
      //  //Engg: should define in Config file.
      //  string xsltResourcePath = "/resourcePath/";
      //  string xsltTextFile = FileHelper.ReadFileAsText(GetEmailLocalizedTemplatePath(regionLanguage, xsltResourcePath));
      //  //CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
      //  return xsltTextFile;
      //}
      string path = @"C:\ewAppsServices\ewAppsServer\ewApps.Payment.DS\XSL\BusinessUserInvitationEmail.xslt";

      string xsltTextFile = FileHelper.ReadFileAsText(path);
      return xsltTextFile;
    }
    #endregion

    #region private methods 
    /// <summary>
    /// Gets the XSLT template language specific path from cache or generates from localized strings 
    /// </summary>
    /// <param name="language"></param>
    /// <param name="xsltResourcePath"></param>
    /// <returns></returns>
    private static string GetEmailLocalizedTemplatePath(string language, string xsltResourcePath) {
      string resourceKey = "ED_Notification_XSL_EventEmail";
      string cacheKey = string.Format("{0}-{1}", language, resourceKey);
      if (CacheHelper.IsInCache(cacheKey)) {
        return (CacheHelper.GetData<string>(cacheKey));
      }
      else {
        string languageSpecXSLTPath = ResourceHelper.GetLocalizedResouce<string>(EDSateliteAssemblyName, EDSateliteAssemblyResourceName, language, resourceKey);
        languageSpecXSLTPath = Path.Combine(xsltResourcePath, languageSpecXSLTPath);
        //CacheHelper.SetData(cacheKey, languageSpecXSLTPath, string.Empty);
        return languageSpecXSLTPath;
      }
    }

    /// <summary>
    /// Gets the XSLT template language specific path from cache or generates from localized strings 
    /// </summary>
    /// <param name="language"></param>
    /// <param name="xsltResourcePath"></param>
    /// <returns></returns>
    private static string GetPUSHLocalizedTemplatePath(string language, string xsltResourcePath) {
      string resourceKey = "ED_Notification_XSL_EventPUSH";
      string cacheKey = string.Format("{0}-{1}", language, resourceKey);
      if (CacheHelper.IsInCache(cacheKey)) {
        return (CacheHelper.GetData<string>(cacheKey));
      }
      else {
        string languageSpecXSLTPath = ResourceHelper.GetLocalizedResouce<string>(EDSateliteAssemblyName, EDSateliteAssemblyResourceName, language, resourceKey);
        languageSpecXSLTPath = Path.Combine(xsltResourcePath, languageSpecXSLTPath);
        //CacheHelper.SetData(cacheKey, languageSpecXSLTPath, string.Empty);
        return languageSpecXSLTPath;
      }
    }
    #endregion
  }
}
