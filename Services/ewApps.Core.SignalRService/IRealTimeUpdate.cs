using System;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.SignalRService
{
  /// <summary>
  /// Interface class for RealTimeUpdate, This can be implemented for any provider.
  /// Server Code will use methods defined in this class so if underline provider is chnage Code will be intact.
  /// </summary>
  public interface IRealTimeUpdate {
    /// <summary>
    /// Event delegate for Add Invoice Event
    /// </summary>
    /// <param name="messageName"></param>
    /// <param name="messagePayload">Message/event information/object</param>
    /// <param name="clientInfo">Client information</param>
    /// <param name="receiverGroupName">Receiver name</param>
    /// <param name="sendToAll">boolean if message need to be send to all connected clients</param>
    /// <param name="token">Cacellation Token</param>
    /// <returns>void</returns>
    //Task SendMessageAsync(MessagePayload msgPayload, string groupName, bool sendToAllClients =false, CancellationToken token = default(CancellationToken));
    Task SendMessageAsync(string messageName, MessagePayload messagePayload, string receiverGroupName, bool sendToAll = false, ClientInfo clientInfo=null, CancellationToken token = default(CancellationToken));
  }
}