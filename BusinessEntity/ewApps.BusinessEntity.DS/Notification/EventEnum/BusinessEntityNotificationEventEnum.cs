using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DS {
    public enum BusinessEntityNotificationEventEnum:long {
        // New A/R Invoice is generated
        BizAddARInvoiceForBusinessUser = 1,
        BizAddARInvoiceForCustomerUser = 2,
        BizBulkAddARInvoice = 3,
        BizUpdateARInvoice = 4,
        CustomerUpdateARInvoice=5,
        BizAddSalesQuotation=6,
        CustomerAddSalesQuotation=7,
        BizUpdateSalesQuotation=8,
        CustomerUpdateSalesQuotation = 9,
        BizAddSalesOrder =10,
        BizUpdateSalesOrder=11,
        CustomerAddSalesOrder = 12,
        CustomerUpdateSalesOrder = 13,
        BizAddDelivery =14,
        BizUpdateDelivery=15,
        CustomerAddDelivery = 16,
        CustomerUpdateDelivery = 20,
        BizAddASN =21,
        BizUpdateASN=22,
        CustomerAddASN = 23,
        CustomerUpdateASN = 24,
        BizAddContract =25,
        BizUpdateContract=26,
        CustomerAddContract = 27,
        CustomerUpdateContract = 28,

        BizAddCutsomer = 29,
        BizUpdateCutsomer = 30

        //CustUserNewARInvoiceIsGenerated = 30,
        //CustUserNewARInvoiceIsUpdated = 31,
        //CustUserNewBulkARInvoiceIsGenerated = 32

    }
}
