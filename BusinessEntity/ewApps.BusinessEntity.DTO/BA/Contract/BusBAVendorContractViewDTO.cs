﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO
{
  public class BusBAVendorContractViewDTO
  {

    public string ERPContractKey
    {
      get; set;
    }

    public string ERPVendorKey
    {
      get; set;
    }


    public Guid VendorId
    {
      get; set;
    }


    public string VendorName
    {
      get; set;
    }

    public string ContactPerson
    {
      get; set;
    }

    public string VendorRefNo
    {
      get; set;
    }

    public string BPCurrency
    {
      get; set;
    }

    public string TelephoneNo
    {
      get; set;
    }

    public string Email
    {
      get; set;
    }

    public int DocumentNo
    {
      get; set;
    }

    public string AgreementMethod
    {
      get; set;
    }


    public DateTime? StartDate
    {
      get; set;
    }

    public DateTime? EndDate
    {
      get; set;
    }

    public string BPProject
    {
      get; set;
    }

    public DateTime? TerminationDate
    {
      get; set;
    }

    public DateTime? SigningDate
    {
      get; set;
    }

    public string Description
    {
      get; set;
    }

    public string AgreementType
    {
      get; set;
    }

    public string PaymentTerms
    {
      get; set;
    }


    public string PaymentMethod
    {
      get; set;
    }

    public int ShippingType
    {
      get; set;
    }

    public string ShippingTypeText
    {
      get; set;
    }

    public int Status
    {
      get; set;
    }


    public string StatusText
    {
      get; set;
    }

    public string Remarks
    {
      get; set;
    }


    public string Owner
    {
      get; set;
    }

    public string ERPDocNum
    {
      get; set;
    }


    [NotMapped]
    public List<BusBAContractItemDTO> ContractItemList
    {
      get; set;
    }

    [NotMapped]
    public IEnumerable<BusBAContractAttachmentDTO> AttachmentList
    {
      get; set;
    }

  }
}
