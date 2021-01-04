/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 28 August2019
 * 
 * Contributor/s: 
 * Last Updated On: 
 */

using System.Data.Common;
using System.Data.SqlClient;
using ewApps.Core.DbConProvider;
using ewApps.Core.NotificationService;
using ewApps.Payment.Common;
using ewApps.Payment.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.QData {

    public partial class QPaymentDBContext:DbContext {


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
        public QPaymentDBContext(DbContextOptions<QPaymentDBContext> options, string connString) : base(options) {
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
        public QPaymentDBContext(DbContextOptions<QPaymentDBContext> options, IOptions<PaymentAppSettings> appSetting, IConnectionManager connectionManager) : base(options) {
            _connString = appSetting.Value.QConnectionString;
            _connectionManager = connectionManager;
        }

        #endregion Constructor

        #region DbContext Override Methods

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //optionsBuilder.UseSqlServer(_connString);
            //DbConnection connection = _connectionManager.GetConnection();
            //if(connection != null) {
            //  optionsBuilder.UseSqlServer(connection);
            //}
            //else {
            //  optionsBuilder.UseSqlServer(_connection);
            //}
            optionsBuilder.UseSqlServer(_connectionManager.GetConnection(_connString));
            base.OnConfiguring(optionsBuilder);
        }

        #endregion DbContext Override Methods

        #region DBQuery


        public DbQuery<BAARInvoiceViewDTO> BAARInvoiceDetailDQQuery {
            get; set;
        }

        public DbQuery<BAARInvoiceItemViewDTO> BAARInvoiceItemViewDTOQuery {
            get;set;
        }

        // 
        public DbQuery<BAAPInvoiceViewDTO> BAAPInvoiceViewDTOQuery {
            get; set;
        }

        public DbQuery<PaymentDetailDQ> PaymentDetailDQQuery {
            get; set;
        }

        public DbQuery<PreAuthPaymentDetailDQ> PreAuthPaymentDetailDQQuery {
            get; set;
        }

        public DbQuery<PreAuthPaymentDTO> PreAuthPaymentDTOQuery {
            get; set;
        }

        /// <summary>
        /// Contains all propertites of Invoice. Map properties from SQL to entity object.
        /// </summary>
        public DbQuery<BAARInvoiceEntityDTO> BAARInvoiceEntityDTOQuery {
            get; set;
        }

        /// <summary>
        /// Map invoice and its related table column values.
        /// </summary>
        public DbQuery<BAARInvoiceDTO> BAARInvoiceDTOQuery {
            get; set;
        }

        /// <summary>
        /// Contains all propertites of APInvoice. Map properties from SQL to entity object.
        /// </summary>
        public DbQuery<BAAPInvoiceEntityDTO> BAAPInvoiceEntityDTOQuery {
            get; set;
        }

        /// <summary>
        /// Map APinvoice and its related table column values.
        /// </summary>
        public DbQuery<BAAPInvoiceDTO> BAAPInvoiceDTOQuery {
            get; set;
        }

        #endregion DBQuery

    }
}
