<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="text" indent="no" encoding="utf-8"/>
  
<!--Payment of amount <amount with currency> with Transaction ID <Transaction ID> 
  initiated by <Customer Co. Name>,  on <payment date and time> via ACH Service.-->

  <xsl:param name = "publisherCompanyName" />
  <xsl:param name = "totalAmount"/>
  <xsl:param name = "transactionID"/>
  <xsl:param name = "businessPartnerCompanyName" />
  <xsl:param name = "transactionDate" />
  <xsl:param name = "serviceName" />

  <xsl:template match="/">
    <xsl:text>Payment of amount </xsl:text>
    <xsl:value-of select="$totalAmount"/>
    <xsl:text> with Transaction ID </xsl:text>
    <xsl:value-of select="$transactionID"/>
    <xsl:text>, initiated by </xsl:text>
    <xsl:value-of select="$businessPartnerCompanyName"/>
    <xsl:text>, on </xsl:text>
    <xsl:value-of select="$transactionDate"/>
    <xsl:text> via </xsl:text>
    <xsl:value-of select="$serviceName"/>
    <xsl:text>.</xsl:text>
    <!--<xsl:text>Regards </xsl:text>
    <xsl:value-of select="$publisherCompanyName"/>-->
  </xsl:template>
</xsl:stylesheet>