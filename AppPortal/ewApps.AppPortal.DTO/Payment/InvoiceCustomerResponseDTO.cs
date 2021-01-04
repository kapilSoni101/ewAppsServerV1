using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    public class InvoiceCustomerResponseDTO {

        public CustomerPaymentInfoDTO customerPaymentInfoDTO {
            get;set;
        }

        public List<BAARInvoiceDTO> listInvoice {
            get; set;
        }

    }
}
