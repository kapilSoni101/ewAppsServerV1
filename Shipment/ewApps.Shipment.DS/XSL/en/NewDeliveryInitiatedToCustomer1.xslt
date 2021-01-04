<?xml version="1.0" encoding="utf-8"?>

<!--Subject - <Publisher Co. Name> Business Portal - <application name>: New Delivery is initiated.

Dear <Business Co. Name>,

A new delivery with below details is initiated:

Delivery ID: <Delivery ID>.

Sales Order ID: <Order ID against which delivery was generated>. (if Ad hoc delivery don’t show this item)

Initiated By <Business User who created the Delivery Order>.

Initiated On: <Generated Date and Time of Delivery Order>.

Carrier Service: <carrier service name>.

Service Plan: <carrier service selected plan>.

Freight Charges: <the amount of freight charges>.

Transit Duration: <the number of days in transit from source to destination>.

Expected Pick Up: <the date on which carrier service will pick the package>.

Please login to the portal to view details. Your portal details are as below:

Sub Domain: <Sub domain>

Portal URL: <Customer Portal URL with shipment application>

Regards

<Publisher Co. Name>

<Copyright text set at the Publisher>-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>

  <!--<xsl:param name="publisherCompanyName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:param name="applicationName"/>
  <xsl:param name="deliveryID"/>
  <xsl:param name="salesOrderID"/>
  <xsl:param name="initiatedBy"/>
  <xsl:param name="initiatedOn"/>
  <xsl:param name="carrierService"/>
  <xsl:param name="servicePlan"/>
  <xsl:param name="freightCharges"/>
  <xsl:param name="transitDuration"/>
  <xsl:param name="expectedPickUp"/>
  <xsl:param name="subDomain"/>
  <xsl:param name="portalURL"/>
  <xsl:param name="copyrightText"/>-->

  <xsl:template match="/">
    <html>
      <head>
        <title>
          <!--HTML document title-->
          <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/PublisherName"/>
          <xsl:text> </xsl:text>
          <xsl:text> Business Portal - </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/AppName"/>
          <xsl:text> </xsl:text>
          <xsl:text>: New Delivery is initiated.</xsl:text>
        </title>
        <style type="text/css">
          .a5q {display: none !important;} .a6S {display: none !important;}
        </style>
        <meta name="x-apple-disable-message-reformatting"/>
      </head>

      <body bgcolor="#fff" leftmargin="0" marginwidth="0" topmargin="0" marginheight="0" offset="0" style="background:#fff; -webkit-text-size-adjust: 100% !important;-ms-text-size-adjust: 100% !important; min-height:620px; ">
        <!--PreHeader Text Start-->
        <div style="display: none !important; max-height: 0px; font-size: 0px;   mso-hide: all !important; position: absolute;">
          <div border="0" cellpadding="0" cellspacing="0" height="0" width="500px" style="display:none !important;
           
           mso-hide:all !important;
           font-size:1px;
           color:#E1E1E1;
           line-height:1px;
           max-height:0px;
           max-width:0px;
           opacity:0;height:0px;
            white-space: nowrap;  position: absolute;" >
            <div>
              <span align="center"   valign="top" width="400px;" colspan="0" style="line-height:0px;display:none !important; mso-hide:all;color:#E1E1E1;font-size:0px;height:0px; position: absolute;">
                <div style="height:0px;margin: 3px 45px 0 0;width: 400px;white-space: nowrap; text-overflow: ellipsis;display:none !important;
             position: absolute;">


                  <xsl:text>Dear </xsl:text>
                  <xsl:value-of select="/EventDataList/CustomerName"/>
                  <xsl:text>, </xsl:text>

                  <xsl:text> A new delivery with below details is initiated by </xsl:text>
                  <xsl:value-of select="/EventDataList/BusinessName"/>
                  <xsl:text> on </xsl:text>
                  <xsl:value-of select="/EventDataList/DeliveryInitiatedDateTime"/>
                  <xsl:text> by Shipping Carrier </xsl:text>
                  <xsl:value-of select="/EventDataList/CarrierServiceName"/>
                  <xsl:text> and expected to reach you by </xsl:text>
                  <xsl:value-of select="/EventDataList/DeliveryExpectedDateTime"/>
                  <xsl:text>.</xsl:text>
                  <div>
                    <table>
                      <tr>
                        <td>
                          <img alt="" align="center" width="130" style="max-width:130px;width:130px; text-align:center;">
                            <xsl:attribute name="alt">
                              <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessName"/>
                            </xsl:attribute>
                            <xsl:attribute name="src">
                              <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessLogoUrl"/>
                            </xsl:attribute>
                          </img>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <!--Shipping From-->
                          <table>
                            <tr>
                              <td>
                                Shipped From:
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessName"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address1"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address2"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address3"/>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td>
                          <!--Shipping To-->
                          <table>
                            <tr>
                              <td>
                                Shipped To:
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CustomerName"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedToAddress/Address1"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedToAddress/Address2"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedToAddress/Address3"/>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <!--Delivery Information-->
                          <table>
                            <tr>
                              <td>
                                <xsl:text>Delivery ID: </xsl:text>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/DeliveryID"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:text>Tracking ID: </xsl:text>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/TrackingID"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:text>Carrier Service: </xsl:text>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CarrierServiceName"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:text>Carrier Service: </xsl:text>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CarrierServiceType"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:text>Delivery Date: </xsl:text>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/DeliveryInitiatedDateTime"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:text>Sales Order ID: </xsl:text>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/SalesOrderID"/>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <!--Billing Address-->
                          <table>
                            <tr>
                              <td>
                                <xsl:text>Billing Address: </xsl:text>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CustomerName"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address1"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address2"/>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address3"/>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <span>Delivery Package(s) Details:</span>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table>
                            <xsl:for-each select="/ShipmentConformationCustomerEmailDTO/Packages">
                              <tr>
                                <td>
                                  <xsl:text>#</xsl:text>
                                  <xsl:value-of select="position()"/>
                                  <xsl:text>; Type: </xsl:text>
                                  <xsl:value-of select="./PackageType"/>
                                  <xsl:text>; Class: </xsl:text>
                                  <xsl:value-of select="./PackageClass"/>
                                  <xsl:text>; Size: </xsl:text>
                                  <xsl:value-of select="./PackageSize"/>
                                  <xsl:text>; Weight: </xsl:text>
                                  <xsl:value-of select="./Weight"/>
                                  <xsl:text>; Tracking ID: </xsl:text>
                                  <xsl:value-of select="./TrackingID"/>
                                </td>
                              </tr>
                              <!--Item Table-->
                              <tr>
                                <td>
                                  <table>
                                    <tr>
                                      <td>
                                        <xsl:text>Item Code</xsl:text>
                                      </td>
                                      <td>
                                        <xsl:text>Item Name</xsl:text>
                                      </td>
                                      <td>
                                        <xsl:text>Quantity</xsl:text>
                                      </td>
                                      <td>
                                        <xsl:text>Unit Price</xsl:text>
                                      </td>
                                      <td>
                                        <xsl:text>Tax %</xsl:text>
                                      </td>
                                      <td>
                                        <xsl:text>Discount %</xsl:text>
                                      </td>
                                      <td>
                                        <xsl:text>Total Price</xsl:text>
                                      </td>
                                    </tr>
                                    <xsl:for-each select="./Items">
                                      <tr>
                                        <td>
                                          <xsl:value-of select="./ItemCode"/>
                                        </td>
                                        <td>
                                          <xsl:value-of select="./ItemName"/>
                                        </td>
                                        <td>
                                          <xsl:value-of select="./Quantity"/>
                                        </td>
                                        <td>
                                          <xsl:value-of select="./UnitPriceWithCurrency"/>
                                        </td>
                                        <td>
                                          <xsl:value-of select="./TaxPercent"/>
                                        </td>
                                        <td>
                                          <xsl:value-of select="./DiscountPercent"/>
                                        </td>
                                        <td>
                                          <xsl:value-of select="./TotalPrice"/>
                                        </td>
                                      </tr>
                                    </xsl:for-each>
                                  </table>
                                </td>
                              </tr>
                            </xsl:for-each>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>

                        </td>
                        <td>
                          <xsl:text>SubTotal: </xsl:text>
                          <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/SubTotal"/>
                        </td>
                      </tr>
                      <tr>
                        <td>

                        </td>
                        <td>
                          <xsl:text>Freight Charges: </xsl:text>
                          <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/FreightCharges"/>
                        </td>
                      </tr>
                      <tr>
                        <td>

                        </td>
                        <td>
                          <xsl:text>Total: </xsl:text>
                          <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/Total"/>
                        </td>
                      </tr>
                    </table>
                  </div>
                  <div>
                    <xsl:text>Please click on the below link to track your delivery:</xsl:text>
                  </div>
                  <div>
                    <xsl:text>Regards,</xsl:text>
                  </div>
                  <div>
                    <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/PublisherName"/>
                  </div>
                  <div>
                    <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CopyrightText"/>
                  </div>

                </div>
              </span>
            </div>
          </div>
        </div>
        <!--PreHeader Text End-->
        <!--Main Body Start-->
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td valign="top" align="center" class="textContent">
              <div style="text-align:left;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;margin-bottom:0;color:#000;line-height:19px;">
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px;color:#000;word-break: break-all;word-break: break-word; margin-top:5px;">
                  <xsl:text>Dear </xsl:text>
                  <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CustomerName"/>
                  <xsl:text>, </xsl:text>
                </p>
                <p style="font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000;">
                  <xsl:text> A new delivery with below details is initiated by </xsl:text>
                  <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessName"/>
                  <xsl:text> on </xsl:text>
                  <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/DeliveryInitiatedDateTime"/>
                  <xsl:text> by Shipping Carrier </xsl:text>
                  <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CarrierServiceName"/>
                  <xsl:text> and expected to reach you by </xsl:text>
                  <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/DeliveryExpectedDateTime"/>
                  <xsl:text>.</xsl:text>
                </p>
                <br/>
              </div>
            </td>
          </tr>
          <tr>
            <td>
              <table style="width:100%;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000; border: 1px solid black;">
                <tr>
                  <td colspan="2" valign="middle">
                    <img src="assets/">
                      <!--<xsl:attribute name="alt">
                        <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessName"/>
                      </xsl:attribute>-->
                      <xsl:attribute name="src">
                        <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessLogoUrl"/>
                      </xsl:attribute>
                    </img>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessName"/>
                    </b>
                  </td>
                </tr>
                <tr>
                  <td>
                    <b>Shipped From:</b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/BusinessName"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address1"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address2"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address3"/>
                    </b>
                    <br/>
                  </td>
                  <td>
                    <b>Shipped To:</b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CustomerName"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedToAddress/Address1"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedToAddress/Address2"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedToAddress/Address3"/>
                    </b>
                    <br/>
                  </td>
                </tr>
                <tr>
                  <td>
                    <table>
                      <tr>
                        <td>
                          Delivery ID: <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/DeliveryID"/>
                          </b>.
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Tracking ID: <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/TrackingID"/>
                          </b>.
                        </td>
                      </tr>
                      <tr>
                        <td>
                          Carrier Service: <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/TrackingID"/>
                          </b>.
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:text>Carrier Service: </xsl:text>
                          <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CarrierServiceName"/>
                          </b>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:text>Carrier Service: </xsl:text>
                          <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CarrierServiceType"/>
                          </b>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:text>Delivery Date: </xsl:text>
                          <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/DeliveryInitiatedDateTime"/>
                          </b>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <xsl:text>Sales Order ID: </xsl:text>
                          <b>
                            <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/SalesOrderID"/>
                          </b>
                        </td>
                      </tr>
                    </table>
                  </td>
                  <td>
                    <b>Billing Address:</b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/CustomerName"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address1"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address2"/>
                    </b>
                    <br/>
                    <b>
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/ShippedFromAddress/Address3"/>
                    </b>
                    <br/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2" valign="middle">
                    <br/>
                  </td>
                </tr>
                <tr>
                  <td colspan="2" valign="middle">
                    <b>Delivery Package(s) Details</b>
                  </td>
                </tr>
                <tr>
                  <td colspan="2" valign="middle">
                    <br/>
                  </td>
                </tr>

                <!--#1. Type: <Box>; Class: <Class 50>; Size: <10x10x10> (in); Weight: <5> (lb); Tracking ID: <show ID>.-->
                <xsl:for-each select="/ShipmentConformationCustomerEmailDTO/Packages/ShipmentPkgNotificationDTO">
                  <tr>
                    <td colspan="2">
                      <b>
                        <xsl:text>#</xsl:text>
                        <xsl:value-of select="position()"/>
                        <xsl:text>.</xsl:text>
                      </b>
                      <b>
                        <xsl:text> Type: </xsl:text>
                      </b>
                      <xsl:value-of select="./PackageType"/>
                      <xsl:text>;</xsl:text>
                      <b>
                        <xsl:text> Class: </xsl:text>
                      </b>
                      <xsl:value-of select="./PackageClass"/>
                      <xsl:text>;</xsl:text>
                      <b>
                        <xsl:text>Size: </xsl:text>
                      </b>
                      <xsl:value-of select="./PackageSize"/>
                      <xsl:text>;</xsl:text>
                      <b>
                        <xsl:text> Weight: </xsl:text>
                      </b>
                      <xsl:value-of select="./Weight"/>
                      <xsl:text>;</xsl:text>
                      <b>
                        <xsl:text> Tracking ID: </xsl:text>
                      </b>
                      <xsl:value-of select="./TrackingID"/>
                      <xsl:text>.</xsl:text>
                    </td>
                  </tr>
                  <!--Item Table-->
                  <tr>
                    <td colspan="2">
                      <table border="1" style="border-collapse: collapse; width:100%;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;font-size:14px; color:#000; border: 1px solid black;">
                        <tr>
                          <td>
                            <xsl:text>Item Code</xsl:text>
                          </td>
                          <td>
                            <xsl:text>Item Name</xsl:text>
                          </td>
                          <td>
                            <xsl:text>Quantity</xsl:text>
                          </td>
                          <td>
                            <xsl:text>Unit Price</xsl:text>
                          </td>
                          <td>
                            <xsl:text>Tax %</xsl:text>
                          </td>
                          <td>
                            <xsl:text>Discount %</xsl:text>
                          </td>
                          <td>
                            <xsl:text>Total Price</xsl:text>
                          </td>
                        </tr>
                        <xsl:for-each select="./Items/ShipmentPkgItemNotificationDTO">
                          <tr>
                            <td>
                              <xsl:value-of select="./ItemCode"/>
                            </td>
                            <td>
                              <xsl:value-of select="./ItemName"/>
                            </td>
                            <td>
                              <xsl:value-of select="./Quantity"/>
                            </td>
                            <td>
                              <xsl:value-of select="./UnitPriceWithCurrency"/>
                            </td>
                            <td>
                              <xsl:value-of select="./TaxPercent"/>
                            </td>
                            <td>
                              <xsl:value-of select="./DiscountPercent"/>
                            </td>
                            <td>
                              <xsl:value-of select="./TotalPrice"/>
                            </td>
                          </tr>
                        </xsl:for-each>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2">
                      <br/>                    
                    </td>
                  </tr>
                </xsl:for-each>
                <tr>
                  <td colspan="2" valign="middle" align="right">
                    <table>
                      <tr>
                        <td align="left">
                          Sub Total
                        </td>
                        <td align="left">
                          <b>$13</b>
                        </td>
                      </tr>
                      <tr>
                        <td align="left">
                          Freight Charges
                        </td>
                        <td align="left">
                          <b>$13</b>
                        </td>
                      </tr>
                      <tr>
                        <td align="left">
                          <b>Total</b>
                        </td>
                        <td align="left">
                          <b>$13</b>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <br/>
              <br/>
            </td>
          </tr>
          <tr>
            <td>
              Please click on the below link to track your delivery:<br/>
              <a style="color:#00A091;font-family:'Lucida Sans Unicode','Lucida Grande', sans-serif;text-decoration:underline;">
                    <xsl:attribute name="href">
                      <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/TrackingURL"/>
                    </xsl:attribute>
                    <xsl:attribute name="target">_blank</xsl:attribute>
                    <xsl:value-of select="/ShipmentConformationCustomerEmailDTO/TrackingURL"/>
                  </a>
              <!--<a href="#">Tracking link to take user to carrier tracking site, with tracking ID enabled.</a>-->
            </td>
          </tr>
          <tr>
            <td align="center">
              Copyright text set at the Publisher

            </td>
          </tr>
        </table>
        <!--Main Body End-->
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>