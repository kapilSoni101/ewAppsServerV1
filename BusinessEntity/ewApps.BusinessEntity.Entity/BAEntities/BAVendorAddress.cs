using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService; 

 namespace ewApps.BusinessEntity.Entity {
  [Table("BAVendorAddress", Schema = "be")]
  public class BAVendorAddress: BaseEntity   {   

  public string ERPConnectorKey  { get;set; }  

  public string ERPVendorKey  { get;set; }  

  public Guid VendorId  { get;set; }  

  public string Label  { get;set; }  

  public int ObjectType  { get;set; }  

  public string ObjectTypeText  { get;set; }  

  public string AddressName  { get;set; }  

  public string Line1  { get;set; }  

  public string Line2  { get;set; }  

  public string Line3  { get;set; }  

  public string Street  { get;set; }  

  public string StreetNo  { get;set; }  

  public string City  { get;set; }  

  public string ZipCode  { get;set; }  

  public string State  { get;set; }  

  public string Country  { get;set; }  
 
 } }