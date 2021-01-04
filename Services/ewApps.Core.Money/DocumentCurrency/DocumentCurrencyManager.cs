using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.Money{
  public class DocumentCurrencyManager: IDocumentCurrencyManager {

    #region Constructor/Properties

    DocumentCurrencyDBContext _documentCurrencyDBContext;
    private ILogger<DocumentCurrencyManager> _logger;

    /// <summary>
    /// Constructor with a db context object. 
    /// </summary>
    /// <param name="documentCurrencyDBContext"></param>
    public DocumentCurrencyManager(DocumentCurrencyDBContext documentCurrencyDBContext,ILogger<DocumentCurrencyManager> logger) {
      _documentCurrencyDBContext = documentCurrencyDBContext;
      _logger = logger;
    }

    #endregion Constructor/Properties

    #region  Public methods

    /// <summary>
    /// Gets Document currency for the Document Id and Type
    /// </summary>
    /// <param name="token"> cancellation Token</param>
    /// <returns></returns>
    public  DocumentCurrency GetDocumentCurrency(Guid documentId,string documentType, CancellationToken token = default(CancellationToken)) {

      DocumentCurrency dCurrency =  _documentCurrencyDBContext.DocumentCurrency.Where<DocumentCurrency>(x=>x.DocumentId ==documentId && x.DocumentType ==documentType).FirstOrDefault();
      _logger.LogTrace("[{Method}] Gets defined document currency {@curency}", "GetDocumentCurrencyAsync", dCurrency);
      return dCurrency;
    }

    /// <summary>
    /// Adds Document Currency
    /// </summary>
    /// <param name="documentCurrency">document currency object that need to be added</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Added document currency Entity</returns>
    public async Task<DocumentCurrency> AddDocumentCurrency(DocumentCurrency documentCurrency, CancellationToken token = default(CancellationToken)) {
      //Check Server in the Cache with givenName for duplicacy 
      _logger.LogDebug("[{Method}] Add - Starts", "AddDocumentCurrency");
      _logger.LogTrace("[{Method}] Add {@documentCurrency}", "AddDocumentCurrency", documentCurrency);
      // Add to Database
      documentCurrency.ID = new Guid();
      //Add webhookserver defination to the DB
      _documentCurrencyDBContext.Add<DocumentCurrency>(documentCurrency);
      await _documentCurrencyDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Add Document Currency - Ends", "AddDocumentCurrency");
      return documentCurrency;
    }

    /// <summary>
    /// Updates Document Currency
    /// </summary>
    /// <param name="documentCurrency">document currency object that need to be added</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>Updated documentcurrency Entity</returns>
    public async Task<DocumentCurrency> UpdateDocumentCurrencyAsync(DocumentCurrency documentCurrency, CancellationToken token = default(CancellationToken)) {
      //Check Server in the Database with givenName
      _logger.LogDebug("[{Method}] Update document currency - Starts", "UpdateDocumentCurrencyAsync");
      _logger.LogTrace("[{Method}] Update document currency {@document}", "UpdateDocumentCurrencyAsync", documentCurrency);

      _documentCurrencyDBContext.Update<DocumentCurrency>(documentCurrency);
      await _documentCurrencyDBContext.SaveChangesAsync();
      _logger.LogDebug("[{Method}] Update Document currency - ends", "UpdateDocumentCurrencyAsync");
      return documentCurrency;
    }

    /// <summary>
    /// Deletes document currency
    /// </summary>
    /// <param name="documentCurrency">Document currency that need to be deleted</param>
    /// <param name="token"> cancellation Token</param>
    /// <returns>void</returns>
    public async Task DeleteDocumentCurrencyAsync(DocumentCurrency documentCurrency, CancellationToken token = default(CancellationToken)) {
      _logger.LogDebug("[{Method}] Delete - Starts", "DeleteDocumentCurrencyAsync");
      _logger.LogTrace("[{Method}] Delete  {@document}", "DeleteDocumentCurrencyAsync", documentCurrency);
      _documentCurrencyDBContext.Remove<DocumentCurrency>(documentCurrency);
      await _documentCurrencyDBContext.SaveChangesAsync(token);
      _logger.LogDebug("[{Method}] Delete - Ends", "DeleteDocumentCurrencyAsync");
    }
    
    #endregion
  }
}
