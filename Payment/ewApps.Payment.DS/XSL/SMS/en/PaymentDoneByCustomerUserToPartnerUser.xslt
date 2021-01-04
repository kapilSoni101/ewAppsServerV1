<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="no" encoding="utf-8"/>

  <xsl:param name = "publisherCompanyName"/>
  <xsl:param name = "totalAmount"/>
  <xsl:param name = "transactionID"/>
  <xsl:param name = "businessPartnerCompanyName" />
  <xsl:param name = "transactionDate" />
  <xsl:param name = "serviceName" />

<!--Payment of amount <amount with currency> with Transaction ID <Transaction ID> 
  initiated to <Business Co. Name>, on <payment date and time> from account number <XX  8787> via ACH Service.-->

  <xsl:template match="/">
    <xsl:text>Payment of amount </xsl:text>
    <xsl:value-of select="$totalAmount"/>
    <xsl:text> with Transaction ID </xsl:text>
    <xsl:value-of select="$transactionID"/>
    <xsl:text> initiated to </xsl:text>
    <xsl:value-of select="$businessPartnerCompanyName"/>
    <xsl:text>, on </xsl:text>
    <xsl:value-of select="$transactionDate"/>
    <xsl:text> from account number xx8xxx </xsl:text>
    <xsl:text> via </xsl:text>
    <xsl:value-of select="$serviceName"/>
    <xsl:text>. </xsl:text>
    <!--<xsl:value-of select="$publisherCompanyName"/>-->
  </xsl:template>
</xsl:stylesheet>