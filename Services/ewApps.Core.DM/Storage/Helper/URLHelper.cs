using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.DMService {
  public class URLHelper {

    public static string GetSubDomainURL(string subDomain, string URL) {
      return string.Format(URL, subDomain);
    }

  }
}
