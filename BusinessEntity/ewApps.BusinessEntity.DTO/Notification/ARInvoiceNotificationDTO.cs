using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class ARInvoiceNotificationDTO
  {

    public Guid InvoiceId
    {
      get; set;
    }

    public string ERPARInvoiceKey
    {
      get; set;
    }

    public Decimal TotalPaymentDue
    {
      get; set;
    }

    public string LocalCurrency
    {
      get; set;
    }

    public DateTime PostingDate
    {
      get; set;
    }

    public DateTime DocumentDate
    {
      get; set;
    }

    public DateTime? DueDate
    {
      get; set;
    }

    public string FullName
    {
      get; set;
    }

    public string UserIdentityNo
    {
      get; set;
    }

    public string SubDomainName
    {
      get; set;
    }

    public string CustomerName
    {
      get; set;
    }

    public string ERPCustomerKey
    {
      get; set;
    }

    public string PublisherName
    {
      get; set;
    }

    public string BusinessName
    {
      get; set;
    }

    public string Copyright
    {
      get; set;
    }

    public string DateTimeFormat
    {
      get; set;
    }

    public Guid AppId
    {
      get; set;
    }

    public string AppKey
    {
      get; set;
    }

    public string AppName
    {
      get; set;
    }


    public Guid BusinessTenantId
    {
      get; set;
    }

    public DateTime UpdatedOn
    {
      get; set;
    }
    public Guid BusinessPartnerTenantId
    {
      get; set;
    }

    public Guid PublisherTenantId
    {
      get; set;
    }
    public string TimeZone
    {
      get; set;
    }

       



    }
}
