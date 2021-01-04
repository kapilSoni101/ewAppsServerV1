using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.Money {
  /// <summary>
  /// Containain currency conversion information. 
  /// </summary>
  public interface IDocumentCurrencyManager {

    /// <summary>
    /// Gets Document currency for the Document Id and Type
    /// </summary>
    /// <param name="token"> cancellation Token</param>
    /// <returns></returns>
    DocumentCurrency GetDocumentCurrency(Guid documentId, string documentType, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Adds Document Currency
    /// </summary>
    /// <param name="documentCurrency">document currency object that need to be added</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Added document currency Entity</returns>
    Task<DocumentCurrency> AddDocumentCurrency(DocumentCurrency documentCurrency, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Updates Document Currency
    /// </summary>
    /// <param name="documentCurrency">document currency object that need to be added</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Updated documentcurrency Entity</returns>
    Task<DocumentCurrency> UpdateDocumentCurrencyAsync(DocumentCurrency documentCurrency, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Deletes document currency
    /// </summary>
    /// <param name="documentCurrency">Document currency that need to be deleted</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>void</returns>
    Task DeleteDocumentCurrencyAsync(DocumentCurrency documentCurrency, CancellationToken token = default(CancellationToken));

  }
}
