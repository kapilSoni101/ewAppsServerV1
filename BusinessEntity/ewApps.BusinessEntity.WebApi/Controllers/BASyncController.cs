using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BusinessEntity.WebApi.Controllers
{

  /// <summary>
  /// Conatins method to test/add connection and init  data with V1 connector.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class BASyncController : ControllerBase
  {

    #region Local Members

    IAppSyncServiceDS _syncService;
    ISyncTimeLogDS _syncTimeLogDS;

    #endregion Local Members

    #region Constructor

    /// <summary>
    /// initalize constructor.
    /// </summary>
    /// <param name="syncService"></param>
    public BASyncController(IAppSyncServiceDS syncService, ISyncTimeLogDS syncTimeLogDS)
    {
      _syncService = syncService;
      _syncTimeLogDS = syncTimeLogDS;
    }

    #endregion Constructor

    #region Test/Add Connection

    /// <summary>
    /// Test app connection with SAPB1
    /// </summary>
    /// <returns></returns>
    [HttpPost("testconnection")]
    public async Task<bool> TestConnectionAsync([FromBody] BATestConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {
      bool result = await _syncService.TestConnectionAsync(request);
      return result;
    }


    ///// <summary>
    ///// Test app connection with SAPB1
    ///// </summary>
    ///// <returns></returns>
    //[HttpPost("addconnection")]
    //public async Task<IActionResult> AddConnectionAsync([FromBody] BAAddConnectionReqDTO request, CancellationToken token = default(CancellationToken)) {
    //  bool result = await _syncService.AddConnectionAsync(request);
    //  return Ok();
    //}


    /// <summary>
    /// Test app connection with SAPB1
    /// </summary>
    /// <returns></returns>
    //ToDo: nitin- url is not correct. Parameter is in-correct it will not work never.
    //ToDo: nitin- Pls review return type.
    [HttpPost("manageconnection/{tenantid}")]
    public async Task<ActionResult> ManageConnectorConfigsAsync([FromBody] List<ERPConnectorConfigDTO> connectorConfigDTO, [FromRoute] Guid tenantId, CancellationToken token)
    {
      await _syncService.ManageConnectorConfigsAsync(connectorConfigDTO, tenantId, token);
      return Ok();
    }

    #endregion Test/Add Connection

    #region InitData

    /// <summary>
    /// Sync entity data from SAPB1 connector.
    /// </summary>
    /// <returns></returns>
    [HttpPut("pullerpdata")]
    public async Task<bool> PullERPDataAsync([FromBody] PullERPDataReqDTO request, CancellationToken token = default(CancellationToken))
    {
      Stopwatch s = new Stopwatch();
      s.Start();
      await _syncService.PullERPDataAsync(request, token);
      Log.Debug(string.Format("BE-Sync-InitDB: Total time taken: {0} in ms", s.ElapsedMilliseconds));
      s.Stop();

      return true;
    }

    #endregion InitData

    #region InitData

    /// <summary>
    /// Sync entity data from SAPB1 connector.
    /// </summary>
    /// <returns></returns>
    [HttpPut("erpattachmentdata")]
    public async Task<AttachmentResDTO> GetAttachmentFromERP([FromBody] AttachmentReqDTO request, CancellationToken token = default(CancellationToken))
    {

      return await _syncService.GetAttachmentFromERPAsync(request, token);
    }

    /// <summary>
    /// Sync entity data from SAPB1 connector.
    /// </summary>
    /// <returns></returns>
    [HttpPost("pushsalesorderdata/{tenantid}")]
    public async Task<bool> PushSalesOrderDataInERPAsync([FromRoute] Guid tenantid, [FromBody] BASalesOrderSyncDTO request, CancellationToken token = default(CancellationToken))
    {
      Stopwatch s = new Stopwatch();
      s.Start();
      await _syncService.PushSalesOrderDataInERPAsync(request, tenantid, token);
      Log.Debug(string.Format("BE-Sync-InitDB: Total time taken: {0} in ms", s.ElapsedMilliseconds));
      s.Stop();

      return true;
    }
    /// <summary>
    /// Sync entity data from SAPB1 connector.
    /// </summary>
    /// <returns></returns>
    [HttpPost("postcustomerdata/{tenantid}")]
    public async Task<bool> PostERPCustomerDataAsync([FromRoute] Guid tenantid, [FromBody] List<BACustomerSyncDTO> request, CancellationToken token = default(CancellationToken))
    {
      Stopwatch s = new Stopwatch();
      s.Start();
      await _syncService.PostERPCustomerDataAsync(request, tenantid, token);
      Log.Debug(string.Format("BE-Sync-InitDB: Total time taken: {0} in ms", s.ElapsedMilliseconds));
      s.Stop();

      return true;
    }

    /// <summary>
    /// Sync entity data from SAPB1 connector.
    /// </summary>
    /// <returns></returns>
    [HttpPost("pushsalesquotationdata/{tenantid}")]
    public async Task<bool> PushSalesQuotationDataInERPAsync([FromRoute] Guid tenantid, [FromBody] BASalesQuotationSyncDTO request, CancellationToken token = default(CancellationToken))
    {
      Stopwatch s = new Stopwatch();
      s.Start();
      await _syncService.PushSalesQuotationDataInERPAsync(request, tenantid, token);
      Log.Debug(string.Format("BE-Sync-InitDB: Total time taken: {0} in ms", s.ElapsedMilliseconds));
      s.Stop();

      return true;
    }

    #endregion InitData

    #region GET Sync time log detial

    /// <summary>
    /// Get connection list
    /// </summary>
    /// <returns></returns>
    [HttpGet("getsynctimelog/{tenantid}")]
    public async Task<List<BASyncTimeLogDTO>> GetSyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      return await _syncTimeLogDS.GetSyncTimeLogAsync(tenantId, token);
    }

    #endregion

    #region Notify

    /// <summary>
    /// Notify Application for chnage at SAPB1
    /// </summary>
    /// <returns></returns>
    [HttpPost("notify")]
    public async Task<IActionResult> NotifyApplicationAsync(NotifyAppDTO notifyDTO, CancellationToken token = default(CancellationToken))
    {
      //  _logger.Debug("[{Method}] Processing starts ", "NotifyApplication");
      bool result = await _syncService.NotifyApplicationAsync(notifyDTO, token);
      //_logger.Verbose("[{Method}] Processing ends with request {@notifyDTO}", "NotifyApplicationAsync", notifyDTO);
      // _logger.Debug("[{Method}] Processing ends ", "NotifyApplicationAsync");

      return Ok( result);
    }
    

    #endregion Notifiy

    /// <summary>
    /// Sync entity data from SAPB1 connector.
    /// </summary>
    /// <returns></returns>
    [HttpPut("itemprice")]
    public async Task<BASyncItemPriceDTO> PullItemPriceAsync([FromBody] PullItemPriceReqDTO request, CancellationToken token = default(CancellationToken))
    {
      return await _syncService.PullItemPriceAsync(request, token);
    }


  }
}