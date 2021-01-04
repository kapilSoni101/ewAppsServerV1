/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 3 December 2018
 */



using ewApps.BusinessEntity.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public partial class BusinessEntityDbContext {

        public DbQuery<ERPConnectorConfigDQ> ERPConnectorConfigDQQuery {
            get; set;
        }
        public DbQuery<BACustomerDTO> BACustomerDTO {
            get; set;
        }
        public DbQuery<CustomerAddressDTO> BACustomerAddressDTO {
            get; set;
        }
        public DbQuery<CustomerContactDTO> BACustomerContactDTO {
            get; set;
        }
        public DbQuery<BusCustomerSetUpAppDTO> BusCustomerSetUpAppDTOQuery {
            get; set;
        }
        public DbQuery<BusCustomerSetUpAppViewDTO> BusCustomerSetUpAppViewDTOQuery {
            get; set;
        }
    public DbQuery<VendorAddressDTO> VendorAddressDTOQuery
    {
      get; set;
    }
    public DbQuery<VendorContactDTO> VendorContactDTOQuery
    {
      get; set;
    }
    public DbQuery<BusVendorSetUpAppDTO> BusVendorSetUpAppDTOQuery
    {
      get; set;
    }
    public DbQuery<BusVendorSetUpAppViewDTO> BusVendorSetUpAppViewDTOQuery
    {
      get; set;
    }

    public DbQuery<BusVendorDTO> BusVendorDTO
    {
      get; set;
    }

    /// <summary>
    /// This is use to get BusBADeliveryDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of BusBADeliveryDTO.
    /// </remarks>
    public DbQuery<BusBADeliveryDTO> BusBADeliveryDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBADeliveryViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBADeliveryViewDTO.
        /// </remarks>
        public DbQuery<BusBADeliveryViewDTO> BusBADeliveryViewDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBADeliveryItemDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBADeliveryItemDTO.
        /// </remarks>
        public DbQuery<BusBADeliveryItemDTO> BusBADeliveryItemDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBADeliveryAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBADeliveryAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBADeliveryAttachmentDTO> BusBADeliveryAttachmentDTOQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get BusBAContractDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAContractDTO.
        /// </remarks>
        public DbQuery<BusBAContractDTO> BusBAContractDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBAContractItemDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAContractItemDTO.
        /// </remarks>
        public DbQuery<BusBAContractItemDTO> BusBAContractItemDTOQuery {
            get; set;
        }
        /// <summary>
        /// This is use to get BusBAContractAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAContractAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBAContractAttachmentDTO> BusBAContractAttachmentDTOQuery {
            get; set;
        }

        public DbQuery<CustBAContractItemDTO> CustBAContractItemDTOQuery {
            get; set;
        }

        public DbQuery<CustBAContractAttachmentDTO> CustBAContractAttachmentDTOQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get BusBAContractViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAContractViewDTO.
        /// </remarks>
        public DbQuery<BusBAContractViewDTO> BusBAContractViewDTOQuery {
            get; set;
        }


        public DbQuery<BusBAItemMasterDTO> BusBAItemMasterDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesQuotationDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesQuotationDTOQuery.
        /// </remarks>
        public DbQuery<BusBASalesQuotationDTO> BusBASalesQuotationDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesQuotationViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesQuotationViewDTOQuery.
        /// </remarks>
        public DbQuery<BusBASalesQuotationViewDTO> BusBASalesQuotationViewDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesQuotationItemDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesQuotationItemDTOQuery.
        /// </remarks>
        public DbQuery<BusBASalesQuotationItemDTO> BusBASalesQuotationItemDTOQuery {
            get; set;
        }

        public DbQuery<CustBASalesQuotationItemDTO> CustBASalesQuotationItemDTOQuery {
            get; set;
        }

        public DbQuery<CustBASalesQuotationAttachmentDTO> CustBASalesQuotationAttachmentDTOQuery {
            get; set;
        }
        /// <summary>
        /// This is use to get BusBASalesQuotationAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesQuotationAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBASalesQuotationAttachmentDTO> BusBASalesQuotationAttachmentDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesOrderDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesOrderDTO.
        /// </remarks>
        public DbQuery<BusBASalesOrderDTO> BusBASalesOrderDTOQuery {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;BAPurchaseOrderItem&gt; can be used to query and save instances of BAPurchaseOrderItem entity. 
        /// Linq queries can written using DbSet&lt;BAPurchaseOrderItem&gt; that will be translated to sql query and executed against database BAPurchaseOrderItem table.
        /// </summary>
        public DbQuery<BAPurchaseOrderItemDTO> BAPurchaseOrderItemDTOQuery {
            get;
            set;
        }

        /// <summary>
        /// This is use to get BAPurchaseOrderDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BAPurchaseOrderDTO.
        /// </remarks>
        public DbQuery<BAPurchaseOrderDTO> BAPurchaseOrderDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BAPurchaseOrderViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BAPurchaseOrderViewDTO.
        /// </remarks>
        public DbQuery<BAPurchaseOrderViewDTO> BAPurchaseOrderViewDTOQuery {
            get; set;
        }

        public DbQuery<BAPurchaseOrderAttachmentDTO> BAPurchaseOrderAttachmentDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesOrderViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesOrderViewDTO.
        /// </remarks>
        public DbQuery<BusBASalesOrderViewDTO> BusBASalesOrderViewDTOQuery {
            get; set;
        }

        public DbQuery<CustBASalesQuotationDTO> CustBASalesQuotationDTOQuery {
            get; set;
        }

        public DbQuery<CustBASalesQuotationViewDTO> CustBASalesQuotationViewDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesOrderItemDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesOrderItemDTO.
        /// </remarks>
        public DbQuery<BusBASalesOrderItemDTO> BusBASalesOrderItemDTOQuery {
            get; set;
        }
        /// <summary>
        /// This is use to get BusBASalesOrderAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesOrderAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBASalesOrderAttachmentDTO> BusBASalesOrderAttachmentDTOQuery {
            get; set;
        }

        public DbQuery<CustBASalesOrderDTO> CustBASalesOrderDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBASalesOrderViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBASalesOrderViewDTO.
        /// </remarks>
        public DbQuery<CustBASalesOrderViewDTO> CustBASalesOrderViewDTOQuery {
            get; set;
        }

        public DbQuery<CustBADeliveryDTO> CustBADeliveryDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBADeliveryViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBADeliveryViewDTO.
        /// </remarks>
        public DbQuery<CustBADeliveryViewDTO> CustBADeliveryViewDTOQuery {
            get; set;
        }

        public DbQuery<CustBADeliveryItemDTO> CustBADeliveryItemDTOQuery {
            get; set;
        }

        public DbQuery<CustBADeliveryAttachmentDTO> CustBADeliveryAttachmentDTOQuery {
            get; set;
        }

        public DbQuery<CustBAItemMasterDTO> CustBAItemMasterDTOQuery {
            get; set;
        }

        public DbQuery<CustBAItemMasterViewDTO> CustBAItemMasterViewDTOQuery {
            get; set;
        }

        public DbQuery<CustBAContractDTO> CustBAContractDTOQuery {
            get; set;
        }

        public DbQuery<CustBAContractViewDTO> CustBAContractViewDTOQuery {
            get; set;
        }


        public DbQuery<BusBAItemMasterViewDTO> BusBAItemMasterViewDTOQuery {
            get; set;
        }

        public DbQuery<BASyncTimeLogDTO> BASyncTimeLogDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBAASNDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAASNDTO.
        /// </remarks>
        public DbQuery<BusBAASNDTO> BusBAASNDTOQuery {
            get; set;
        }

        public DbQuery<CustBAASNDTO> CustBAASNDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBAASNItemDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAASNItemDTO.
        /// </remarks>
        public DbQuery<BusBAASNItemDTO> BusBAASNItemDTOQuery {
            get; set;
        }

        public DbQuery<CustBAASNItemDTO> CustBAASNItemDTOQuery {
            get; set;
        }
        public DbQuery<CustBAASNAttachmentDTO> CustBAASNAttachmentDTOQuery {
            get; set;
        }
        /// <summary>
        /// This is use to get BusBAASNAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAASNAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBAASNAttachmentDTO> BusBAASNAttachmentDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBAASNViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAASNViewDTO.
        /// </remarks>
        public DbQuery<BusBAASNViewDTO> BusBAASNViewDTOQuery {
            get; set;
        }


        public DbQuery<CustBAASNViewDTO> CustBAASNViewDTOQuery {
            get; set;
        }

        public DbQuery<ASNotificationDTO> ASNotificationDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBAARInvoiceAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAARInvoiceAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBAARInvoiceAttachmentDTO> BusBAARInvoiceAttachmentDTOQuery {
            get; set;
        }

        public DbQuery<BAARPurchaseOrderAttachmentDTO> BAARPurchaseOrderAttachmentDTOQuery {
            get;set;
        }

        /// <summary>
        /// This is use to get BusBAARInvoiceAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAARInvoiceAttachmentDTO.
        /// </remarks>
        public DbQuery<CustBASalesOrderAttachmentDTO> CustBASalesOrderAttachmentDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusBAItemAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusBAItemAttachmentDTO.
        /// </remarks>
        public DbQuery<BusBAItemAttachmentDTO> BusBAItemAttachmentDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get CustBAItemAttachmentDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of CustBAItemAttachmentDTO.
        /// </remarks>
        public DbQuery<CustBAItemAttachmentDTO> CustBAItemAttachmentDTOQuery {
            get; set;
        }
    /// <summary>
    /// This is use to get VendorBAContractDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of VendorBAContractDTOQuery.
    /// </remarks>
    public DbQuery<BusBAVendorContractDTO> BusBAVendorContractDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// This is use to get VendorBAContractViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of VendorBAContractViewDTO.
    /// </remarks>
    public DbQuery<BusBAVendorContractViewDTO> BusBAVendorContractViewDTOQuery
    {
      get; set;
    }

    /// <summary>
    /// This is use to get VendorBAContractDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
    /// is translated into database query.
    /// </summary>
    /// <remarks>
    /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
    /// to properties of VendorBAContractDTOQuery.
    /// </remarks>
    public DbQuery<VendorBAContractDTO> VendorBAContractDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get VendorBAContractViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of VendorBAContractViewDTO.
        /// </remarks>
        public DbQuery<VendorBAContractViewDTO> VendorBAContractViewDTOQuery {
            get; set;
        }

    }
}