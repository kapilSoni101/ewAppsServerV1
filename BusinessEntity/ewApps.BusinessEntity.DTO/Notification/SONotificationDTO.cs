using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class SONotificationDTO
  {

    public Guid SalesOrderId
    {
      get; set;
    }

    public string ERPSalesOrderKey
    {
      get; set;
    }

    public decimal TotalPaymentDue
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

    public DateTime DeliveryDate
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

    public Guid BusinessTenantId
    {
      get; set;
    }

    public DateTime CreatedOn
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
