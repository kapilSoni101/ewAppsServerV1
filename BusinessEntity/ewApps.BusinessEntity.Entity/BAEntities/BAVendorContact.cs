using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService; 

 namespace ewApps.BusinessEntity.Entity {
  [Table("BAVendorContact", Schema = "be")]
  public class BAVendorContact: BaseEntity   {     

  public string ERPContactKey  { get;set; }  

  public string ERPConnectorKey  { get;set; }  

  public string ERPVendorKey  { get;set; }  

  public Guid VendorId  { get;set; }  

  public bool Default  { get;set; }  

  public string FirstName  { get;set; }  

  public string LastName  { get;set; }  

  public string Title  { get;set; }  

  public string Position  { get;set; }  

  public string Address  { get;set; }  

  public string Telephone  { get;set; }  

  public string Email  { get;set; }  
 
 } }