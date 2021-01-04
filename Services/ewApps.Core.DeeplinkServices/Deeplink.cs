using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.DeeplinkServices
{
  /// <summary>
  /// Sets up data for Deeplink
  /// It contains: Actions, One RelativeUrl, Other parameters as K-V pairs
  /// It is derived from LINK class from the Commonruntime
  /// </summary>
  public class Deeplink : Link
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Deeplink"/> class.
    /// </summary>
    public Deeplink()
    {
    }

    /// <summary>
    /// Sets the deeplink parameters.
    /// Extract the ACtions and RelativeURL from other parameters..
    /// </summary>
    public void SetLinkParameters(Dictionary<string, string> parameters)
    {
      // Get actions which is comma seperated value
      string[] actions;
      if (parameters.ContainsKey("Actions"))
      {
        actions = parameters["Actions"].Split(',');
        foreach (string action in actions)
          base.AddAction(action);
      }

      // Generate relativeUrl
      string relativeDesktopUrl = "";  // Default
      if (parameters.ContainsKey("RelativeURL"))
      {
        relativeDesktopUrl = parameters["RelativeURL"];
      }

      // Set other parameters
      // For example:DocumentId, Loggedin user Id, SenderId, event Information
      foreach (string parameter in parameters.Keys)
      {
        if (parameter == "Actions" || parameter == "RelativeURL") continue;
        base.AddParameter(new KeyValuePair<string, string>(parameter, parameters[parameter]));
      }
      base.SetDesktopURL(relativeDesktopUrl);
    }
  }
}
