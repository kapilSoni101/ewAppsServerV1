using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.ServiceProcessor {

  public interface IHttpHeaderHelper {

    void SetHeaderValue(string key, object value);

  }
}
