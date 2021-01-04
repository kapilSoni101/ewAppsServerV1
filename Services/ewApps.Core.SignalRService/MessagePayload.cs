using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.SignalRService
{
  /// <summary>
  /// Information about the Message
  /// </summary>
  public class MessagePayload {
    /// <summary>
    /// Event Name
    /// </summary>
    public string EventName {
      get; set;
    }
    /// <summary>
    /// Event info/object
    /// </summary>
    public object EventInfo {
      get; set;
    }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="eventName">Name of the event </param>
    /// <param name="eventInfo">Any information about the event</param>
    public MessagePayload(string eventName, object eventInfo) {
      EventName = eventName;
      EventInfo = eventInfo;
    }
  }
}