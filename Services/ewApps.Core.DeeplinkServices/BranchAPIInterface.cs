using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ewApps.Core.DeeplinkServices
{
  /// <summary>
  /// This class provides the methods to generate any url based on input information.
  /// </summary>
  public class BranchAPIInterface
  {
    IHttpRequestProcessor _requestProcessor;

    // Configured branch uri.
    private string _branchAPIUri = string.Empty;

    // Configured branch key.
    private string _branchAPIKey = string.Empty;


    public BranchAPIInterface(string deeplinkApiKey, string deeplinkApiUrl) {
      _branchAPIKey = deeplinkApiKey;
      _branchAPIUri = deeplinkApiUrl;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BranchAPIInterface"/> class.
    /// </summary>
    /// <param name="linkInstance">The link instance.</param>
    public BranchAPIInterface(Link linkInstance, string deeplinkApiKey, string deeplinkApiUrl) : this(deeplinkApiKey, deeplinkApiUrl) {
      _requestProcessor = new HttpRequestProcessor(new System.Net.Http.HttpClient());
      DeepLink = linkInstance;
      //// Reads branch uri from Config file.
      //_branchAPIUri = AppSettingHelper.GetBranchAPIUrl();
      //// Reads branch Key from Config file.
      //_branchAPIKey = AppSettingHelper.GetBranchAPIKey();
    }

    /// <summary>
    /// The link model contains link related data.
    /// </summary>
    public Link DeepLink
    {
      get;
      private set;
    }

    /// <summary>
    /// Gets the branch object.
    /// </summary>
    /// <returns>Returns 'BranchDeepLinkModel' instance with branch key and deep link data.</returns>
    private BranchDeepLinkModel GetBranchObject() {
      BranchDeepLinkModel bModel = new BranchDeepLinkModel();
      bModel.branch_key = _branchAPIKey;
      bModel.data = DeepLink.ActionAndPara;
      return bModel;
    }

    /// <summary>
    /// Generates the short URL based on branch key and link data.
    /// </summary>
    /// <returns>Returns generated short url.</returns>
    public string GenerateShortURL() {
      Stopwatch sw = new Stopwatch();
      sw.Start();
      BranchResult responseString = null;
      Int16 retryAttempt = 3;
      for (int i = 1; i <= retryAttempt; i++)
      {
        try
        {
          BranchDeepLinkModel branchDeeplinkModel = GetBranchObject();
          //Generate API request
          responseString = _requestProcessor.ExecutePOSTRequest<BranchResult, BranchDeepLinkModel>(_branchAPIUri, "", AcceptMediaTypeEnum.JSON, null, null, null, branchDeeplinkModel);
          break;
        }
        catch (Exception ex)
        {
          StringBuilder exDetail = new StringBuilder();
          exDetail.AppendLine("Branch API Error-");
          exDetail.AppendLine("Base Exception Type-" + ex.GetBaseException().GetType().FullName);
          exDetail.AppendFormat("{0} Exception Detail-Message-{1}", ex.GetBaseException().GetType().FullName, ex.Message);
          exDetail.AppendFormat("{0} Exception Detail-StackTrace-{1}", ex.GetBaseException().GetType().FullName, ex.StackTrace);

          if (ex is AggregateException)
          {
            (ex as AggregateException).Handle(exAggrEx => {
              exDetail.AppendLine("=================================================");
              exDetail.AppendLine("Child Exception Type-" + exAggrEx.GetBaseException().GetType().FullName);
              exDetail.AppendFormat("{0} Exception Detail-Message-{1}", exAggrEx.GetBaseException().GetType().FullName, exAggrEx.Message);
              exDetail.AppendFormat("{0} Exception Detail-StackTrace-{1}", ex.GetBaseException().GetType().FullName, exAggrEx.StackTrace);
              exDetail.AppendLine("Sub-Exception Type-" + exAggrEx.GetType().FullName);
              exDetail.AppendLine("=================================================");
              return true;
            });
          }
          exDetail.AppendFormat("Branch Error Attempt Count - {0} : ", i);
          //MessageLogger.Instance.LogMessage(string.Format("Branch Error Log: {0}", exDetail.ToString()), LoggerCategory.Production, null, false);
          if (retryAttempt <= i)
          {
            throw;
          }
        }
      }
      sw.Stop();
      // MessageLogger.Instance.LogMessage("GenerateShortUrl:" + sw.ElapsedMilliseconds, LoggerCategory.Debug, null, false);
      return responseString.url;
    }

    public string GetDeeplinkObjectJSON() {
      return JsonSerializer.Serialize<DeepLinkModel>(DeepLink.ActionAndPara);
    }
  }
}
