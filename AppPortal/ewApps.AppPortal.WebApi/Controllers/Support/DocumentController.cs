// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur <rthakur@eworkplaceapps.com>
 * Date: 04 March 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 March 2019
 */

using System;
using System.IO;
using System.Threading.Tasks;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.WebApi {

    /// <summary>
    /// This class represents all the operations to be performed for a document.
    /// </summary>
    [Route("api/[controller]")]
  [ApiController]
  public class DocumentController:ControllerBase {

    # region Local Variables

    IDMDocumentDS _documentDataService;
    IHttpHeaderHelper _httpHeaderHelper;
    private static IHttpContextAccessor _contextAccessor;
    static DMServiceSettings _thumbnailSetting;

    #endregion Local Variables

    #region Constructor  

    /// <summary>
    /// Initialize local variables for the document class.
    /// </summary>
    /// <param name="appSetting">Application setting class instance</param>
    /// <param name="documentDataService">Document DS class instance</param>
    /// <param name="contextAccessor">Context accessor</param>
    /// <param name="httpHeaderHelper">Htto header helper class instance</param>
    public DocumentController(IOptions<DMServiceSettings> appSetting, IDMDocumentDS documentDataService, IHttpContextAccessor contextAccessor, IHttpHeaderHelper httpHeaderHelper) {
      _httpHeaderHelper = httpHeaderHelper;
      _documentDataService = documentDataService;
      _contextAccessor = contextAccessor;
      _thumbnailSetting = appSetting.Value;
    }

    #endregion Constructor  

    #region Public Methods

    /// <summary>
    /// Gets the document detail to download document by document id.
    /// </summary>
    /// <param name="documentId">Document Id to get document information.</param>
    /// <returns>Filestream result with octet-stream content type for the required document Id.</returns>
    [HttpGet]
    [Route("documentdownloaddetail/{documentId}")]
    public async Task<IActionResult> GetDocumentDetailById(Guid documentId) {

      // Get document's basic information.
      DocumentResponseModel model = await _documentDataService.GetDocumentDetailById(documentId);

      // Get the file stream for document.
      FileStream stream = await _documentDataService.GetFileStream(documentId, model.FileStorageId, model.FileName, model.TenantId);

      // Raise file not found exception, if document is not available at physical location.
      if(stream == null)
        throw new FileNotFoundException();

      // returns a FileStreamResult with octet-stream content type
      return File(stream, "application/octet-stream");
    }

    #endregion Public Methods

  }
}