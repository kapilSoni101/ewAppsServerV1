/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 23 November 2018
 */

using System.Data.SqlClient;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.BusinessEntity.Data
{

  // Hari Sir Review

  /// <summary>  
  /// This class contains a session of core database and can be used to query and 
  /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
  /// </summary>
  public partial class BusinessEntityDbContext : DbContext
  {

    #region Local Members

    protected string _connString;
    protected IConnectionManager _connectionManager;

    #endregion Local Members

    #region Constructor

    /// <summary>
    /// Default constructor to initialize member variables (if any).
    /// </summary>
    /// <param name="options">The DbContextOptions instance carries configuration information such as: 
    /// (a) The database provider to use, UseSqlServer or UseSqlite
    /// (b) Connection string or identifier of the database instance    
    /// </param>
    /// <param name="connString">The Core database connection string.</param>
    public BusinessEntityDbContext(DbContextOptions<BusinessEntityDbContext> options, string connString) : base(options)
    {
      _connString = connString;
    }


    /// <summary>
    /// Constructor to initialize member variables (if any).
    /// </summary>
    /// <param name="options">The DbContextOptions instance carries configuration information such as: 
    /// (a) The database provider to use, UseSqlServer or UseSqlite
    /// (b) Connection string or identifier of the database instance    
    /// </param>
    /// <param name="appSetting">Instance of Appsettings object that contains core database 
    /// connection string.
    /// </param>
    public BusinessEntityDbContext(DbContextOptions<BusinessEntityDbContext> options, IOptions<BusinessEntityAppSettings> appSetting, IConnectionManager connectionManager) : base(options)
    {
      _connString = appSetting.Value.ConnectionString;
      _connectionManager = connectionManager;
    }

    #endregion Constructor

    #region DbContext Override Methods

    /// <inheritdoc />  
    protected override void OnModelCreating(ModelBuilder builder)
    {

      #region Default Value Section

      #endregion
      MasterData.Init(builder);
      base.OnModelCreating(builder);
    }


    /// <inheritdoc />  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // For Db Migration 
      if (_connectionManager == null)
      {
        optionsBuilder.UseSqlServer(_connString);
      }
      else
      {
        SqlConnection conn = _connectionManager.GetConnection(_connString);
        optionsBuilder.UseSqlServer(conn);
      }

      base.OnConfiguring(optionsBuilder);
    }

    #endregion DbContext Override Methods

    #region DbSets

    #region BA DBSets

    /// <summary>
    /// DbSet&lt;BAARInvoice&gt; can be used to query and save instances of BAARInvoice entity. 
    /// Linq queries can written using DbSet&lt;BAARInvoice&gt; that will be translated to sql query and executed against database BAARInvoice table.
    /// </summary>
    public DbSet<BAARInvoice> BAARInvoice
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAARInvoiceItem&gt; can be used to query and save instances of BAARInvoiceItem entity. 
    /// Linq queries can written using DbSet&lt;BAARInvoiceItem&gt; that will be translated to sql query and executed against database BAARInvoiceItem table.
    /// </summary>
    public DbSet<BAARInvoiceItem> BAARInvoiceItem
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAARInvoiceAttachment&gt; can be used to query and save instances of BAARInvoiceAttachment entity. 
    /// Linq queries can written using DbSet&lt;BAARInvoiceAttachment&gt; that will be translated to sql query and executed against database BAARInvoiceAttachment table.
    /// </summary>
    public DbSet<BAARInvoiceAttachment> BAARInvoiceAttachment
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAAPInvoice&gt; can be used to query and save instances of BAAPInvoice entity. 
    /// Linq queries can written using DbSet&lt;BAAPInvoice&gt; that will be translated to sql query and executed against database BAARInvoice table.
    /// </summary>
    public DbSet<BAAPInvoice> BAAPInvoice
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAAPInvoiceItem&gt; can be used to query and save instances of BAAPInvoiceItem entity. 
    /// Linq queries can written using DbSet&lt;BAAPInvoiceItem&gt; that will be translated to sql query and executed against database BAARInvoiceItem table.
    /// </summary>
    public DbSet<BAAPInvoiceItem> BAAPInvoiceItem
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAAPInvoiceAttachment&gt; can be used to query and save instances of BAAPInvoiceAttachment entity. 
    /// Linq queries can written using DbSet&lt;BAAPInvoiceAttachment&gt; that will be translated to sql query and executed against database BAARInvoiceAttachment table.
    /// </summary>
    public DbSet<BAAPInvoiceAttachment> BAAPInvoiceAttachment
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAContract&gt; can be used to query and save instances of BAContract entity. 
    /// Linq queries can written using DbSet&lt;BAContract&gt; that will be translated to sql query and executed against database BAContract table.
    /// </summary>
    public DbSet<BAContract> BAContract
    {
      get;
      set;
    }
    /// <summary>
    /// DbSet&lt;BAVendorContract&gt; can be used to query and save instances of BAVendorContract entity. 
    /// Linq queries can written using DbSet&lt;BAVendorContract&gt; that will be translated to sql query and executed against database BAVendorContract table.
    /// </summary>
    public DbSet<BAVendorContract> BAVendorContract
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BACustomer&gt; can be used to query and save instances of BACustomer entity. 
    /// Linq queries can written using DbSet&lt;BACustomer&gt; that will be translated to sql query and executed against database BACustomer table.
    /// </summary>
    public DbSet<BACustomer> BACustomer
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BACustomerAddress&gt; can be used to query and save instances of BACustomerAddress entity. 
    /// Linq queries can written using DbSet&lt;BACustomerAddress&gt; that will be translated to sql query and executed against database BACustomerAddress table.
    /// </summary>
    public DbSet<BACustomerAddress> BACustomerAddress
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BACustomerContact&gt; can be used to query and save instances of BACustomerContact entity. 
    /// Linq queries can written using DbSet&lt;BACustomerContact&gt; that will be translated to sql query and executed against database BACustomerContact table.
    /// </summary>
    public DbSet<BACustomerContact> BACustomerContact
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BACustomerPaymentDetail&gt; can be used to query and save instances of BACustomerPaymentDetail entity. 
    /// Linq queries can written using DbSet&lt;BACustomerPaymentDetail&gt; that will be translated to sql query and executed against database BACustomerPaymentDetail table.
    /// </summary>
    public DbSet<BACustomerPaymentDetail> BACustomerPaymentDetail
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAVendor&gt; can be used to query and save instances of BAVendor entity. 
    /// Linq queries can written using DbSet&lt;BAVendor&gt; that will be translated to sql query and executed against database BAVendor table.
    /// </summary>
    public DbSet<BAVendor> BAVendor
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAVendorAddress&gt; can be used to query and save instances of BAVendorAddress entity. 
    /// Linq queries can written using DbSet&lt;BAVendorAddress&gt; that will be translated to sql query and executed against database BAVendorAddress table.
    /// </summary>
    public DbSet<BAVendorAddress> BAVendorAddress
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAVendorContact&gt; can be used to query and save instances of BAVendorContact entity. 
    /// Linq queries can written using DbSet&lt;BAVendorContact&gt; that will be translated to sql query and executed against database BAVendorContact table.
    /// </summary>
    public DbSet<BAVendorContact> BAVendorContact
    {
      get;
      set;
    }
    /// <summary>
    /// DbSet&lt;BADelivery&gt; can be used to query and save instances of BADelivery entity. 
    /// Linq queries can written using DbSet&lt;BADelivery&gt; that will be translated to sql query and executed against database BADelivery table.
    /// </summary>
    public DbSet<BADelivery> BADelivery
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BADeliveryItem&gt; can be used to query and save instances of BADeliveryItem entity. 
    /// Linq queries can written using DbSet&lt;BADeliveryItem&gt; that will be translated to sql query and executed against database BADeliveryItem table.
    /// </summary>
    public DbSet<BADeliveryItem> BADeliveryItem
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BADeliveryAttachment&gt; can be used to query and save instances of BADeliveryAttachment entity. 
    /// Linq queries can written using DbSet&lt;BADeliveryAttachment&gt; that will be translated to sql query and executed against database BADeliveryAttachment table.
    /// </summary>
    public DbSet<BADeliveryAttachment> BADeliveryAttachment
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAItemMaster&gt; can be used to query and save instances of BAItemMaster entity. 
    /// Linq queries can written using DbSet&lt;BAItemMaster&gt; that will be translated to sql query and executed against database BAItemMaster table.
    /// </summary>
    public DbSet<BAItemMaster> BAItemMaster
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAItemMasterAttachment&gt; can be used to query and save instances of BAItemMasterAttachment entity. 
    /// Linq queries can written using DbSet&lt;BAItemMasterAttachment&gt; that will be translated to sql query and executed against database BAItemMasterAttachment table.
    /// </summary>
    public DbSet<BAItemMasterAttachment> BAItemMasterAttachment
    {
      get;
      set;
    }


    /// <summary>
    /// DbSet&lt;BAPurchaseInquiry&gt; can be used to query and save instances of BAPurchaseInquiry entity. 
    /// Linq queries can written using DbSet&lt;BAPurchaseInquiry&gt; that will be translated to sql query and executed against database BAPurchaseInquiry table.
    /// </summary>
    public DbSet<BAPurchaseInquiry> BAPurchaseInquiry
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAPurchaseInquiryItem&gt; can be used to query and save instances of BAPurchaseInquiryItem entity. 
    /// Linq queries can written using DbSet&lt;BAPurchaseInquiryItem&gt; that will be translated to sql query and executed against database BAPurchaseInquiryItem table.
    /// </summary>
    public DbSet<BAPurchaseInquiryItem> BAPurchaseInquiryItem
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAPurchaseOrder&gt; can be used to query and save instances of BAPurchaseOrder entity. 
    /// Linq queries can written using DbSet&lt;BAPurchaseOrder&gt; that will be translated to sql query and executed against database BAPurchaseOrder table.
    /// </summary>
    public DbSet<BAPurchaseOrder> BAPurchaseOrder
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAPurchaseOrder&gt; can be used to query and save instances of BAPurchaseOrderAttachment entity. 
    /// Linq queries can written using DbSet&lt;BAPurchaseOrderAttachment&gt; that will be translated to sql query and executed against database BAPurchaseOrderAttachment table.
    /// </summary>
    public DbSet<BAPurchaseOrderAttachment> BAPurchaseOrderAttachment
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAPurchaseOrderItem&gt; can be used to query and save instances of BAPurchaseOrderItem entity. 
    /// Linq queries can written using DbSet&lt;BAPurchaseOrderItem&gt; that will be translated to sql query and executed against database BAPurchaseOrderItem table.
    /// </summary>
    public DbSet<BAPurchaseOrderItem> BAPurchaseOrderItem
    {
      get;
      set;
    }



    /// <summary>
    /// DbSet&lt;BASalesOrder&gt; can be used to query and save instances of TenantAppService entity. 
    /// Linq queries can written using DbSet&lt;BASalesOrder&gt; that will be translated to sql query and executed against database BASalesOrder table.
    /// </summary>
    public DbSet<BASalesOrder> BASalesOrder
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BASalesOrder&gt; can be used to query and save instances of BASalesOrderItem entity. 
    /// Linq queries can written using DbSet&lt;BASalesOrderItem&gt; that will be translated to sql query and executed against database BASalesOrder table.
    /// </summary>
    public DbSet<BASalesOrderItem> BASalesOrderItem
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BASalesOrderAttachment&gt; can be used to query and save instances of BASalesOrderAttachment entity. 
    /// Linq queries can written using DbSet&lt;BASalesOrderAttachment&gt; that will be translated to sql query and executed against database BASalesOrderAttachment table.
    /// </summary>
    public DbSet<BASalesOrderAttachment> BASalesOrderAttachment
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BASalesQuotation&gt; can be used to query and save instances of BASalesQuotation entity. 
    /// Linq queries can written using DbSet&lt;BASalesQuotation&gt; that will be translated to sql query and executed against database BASalesOrder table.
    /// </summary>
    public DbSet<BASalesQuotation> BASalesQuotation
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BASalesQuotationItem&gt; can be used to query and save instances of BASalesQuotationItem entity. 
    /// Linq queries can written using DbSet&lt;BASalesQuotationItem&gt; that will be translated to sql query and executed against database BASalesOrder table.
    /// </summary>
    public DbSet<BASalesQuotationItem> BASalesQuotationItem
    {
      get;
      set;
    }
    /// <summary>
    /// DbSet&lt;BASalesQuotationAttachment&gt; can be used to query and save instances of BASalesQuotationAttachment entity. 
    /// Linq queries can written using DbSet&lt;BASalesQuotationAttachment&gt; that will be translated to sql query and executed against database BASalesQuotationAttachment table.
    /// </summary>
    public DbSet<BASalesQuotationAttachment> BASalesQuotationAttachment
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;Role&gt; can be used to query and save instances of Role entity. 
    /// Linq queries can written using DbSet&lt;role &gt; that will be translated to sql query and executed against database role table.
    /// </summary>
    public DbSet<Role> Role
    {
      get; set;
    }
    /// <summary>
    /// DbSet&lt;BAContractItem&gt; can be used to query and save instances of BAContractItem entity. 
    /// Linq queries can written using DbSet&lt;BAContractItem&gt; that will be translated to sql query and executed against database BAContractItem table.
    /// </summary>
    public DbSet<BAContractItem> BAContractItem
    {
      get;
      set;
    }
    /// <summary>
    /// DbSet&lt;BAContractAttachment&gt; can be used to query and save instances of BAContractAttachment entity. 
    /// Linq queries can written using DbSet&lt;BAContractAttachment&gt; that will be translated to sql query and executed against database BAContractAttachment table.
    /// </summary>
    public DbSet<BAContractAttachment> BAContractAttachment
    {
      get;
      set;
    }
    /// DbSet&lt;role linking&gt; can be used to query and save instances of role linking entity. 
    /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database role linking table.    /// DbSet&lt;VerifiedAddress&gt; can be used to query and save instances of VerifiedAddress entity. 
    public DbSet<RoleLinking> RoleLinking
    {
      get; set;
    }
    #endregion BA DBSets

    /// <summary>
    /// DbSet&lt;ERPConnector&gt; can be used to query and save instances of ERPConnector entity. 
    /// Linq queries can written using DbSet&lt;ERPConnector&gt; that will be translated to sql query and executed against database ERPConnector table.
    /// </summary>
    public DbSet<ERPConnector> ERPConnector
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;ERPConnectorConfig&gt; can be used to query and save instances of ERPConnectorConfig entity. 
    /// Linq queries can written using DbSet&lt;ERPConnectorConfig&gt; that will be translated to sql query and executed against database ERPConnectorConfig table.
    /// </summary>
    public DbSet<ERPConnectorConfig> ERPConnectorConfig
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;SyncHistory&gt; can be used to query and save instances of SyncHistory entity. 
    /// Linq queries can written using DbSet&lt;SyncHistory&gt; that will be translated to sql query and executed against database SyncHistory table.
    /// </summary>
    public DbSet<SyncHistory> SyncHistory
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;SyncTimeLog&gt; can be used to query and save instances of SyncTimeLog entity. 
    /// Linq queries can written using DbSet&lt;SyncTimeLog&gt; that will be translated to sql query and executed against database SyncTimeLog table.
    /// </summary>
    public DbSet<SyncTimeLog> SyncTimeLog
    {
      get;
      set;
    }
    /// <summary>
    /// DbSet&lt;BAASN&gt; can be used to query and save instances of BAASN entity. 
    /// Linq queries can written using DbSet&lt;BAASN&gt; that will be translated to sql query and executed against database BAASN table.
    /// </summary>
    public DbSet<BAASN> BAASN
    {
      get;
      set;
    }
    /// <summary>
    /// DbSet&lt;BAASNItem&gt; can be used to query and save instances of BAASNItem entity. 
    /// Linq queries can written using DbSet&lt;BAASNItem&gt; that will be translated to sql query and executed against database BAASNItem table.
    /// </summary>
    public DbSet<BAASNItem> BAASNItem
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;BAASNAttachment&gt; can be used to query and save instances of BAASNAttachment entity. 
    /// Linq queries can written using DbSet&lt;BAASNAttachment&gt; that will be translated to sql query and executed against database BAASNAttachment table.
    /// </summary>
    public DbSet<BAASNAttachment> BAASNAttachment
    {
      get;
      set;
    }


    /// <summary>
    /// DbSet&lt;ASNotification&gt; can be used to query and save instances of ASNotification entity. 
    /// Linq queries can written using DbSet&lt;ASNotification&gt; that will be translated to sql query and executed against database ASNotification table.
    /// </summary>
    public DbSet<ASNotification> ASNotification
    {
      get;
      set;
    }

    /// <summary>
    /// DbSet&lt;TenantUserAppPreference&gt; can be used to query and save instances of TenantUserAppPreference entity. 
    /// Linq queries can written using DbSet&lt;TenantUserAppPreference&gt; that will be translated to sql query and executed against database TenantUserAppPreference table.
    /// </summary>
    public DbSet<TenantUserAppPreference> TenantUserAppPreference
    {
      get;
      set;
    }

    #endregion DbSets
  }
}