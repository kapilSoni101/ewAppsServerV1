<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <!--Transaction <Transaction Number> of amount <amount with currency> by <Customer Co. Name> dated <date and time of transaction> 
    is updated from <old transaction status> to <new transaction status> status.-->

  <xsl:param name = "transactionID"/>
  <xsl:param name = "transactionAmount"/>
  <xsl:param name = "transactionDate"/>
  <xsl:param name = "transactionStatus"/>
  <xsl:param name = "newTransactionStatus"/>
  <xsl:param name = "businessPartnerCompanyName"/>

  <xsl:template match="/">
    <xsl:text>Transaction </xsl:text>
    <xsl:value-of select="$transactionID"/>
    <xsl:text> of amount </xsl:text>
    <xsl:value-of select="$transactionAmount"/>
    <xsl:text> by </xsl:text>
    <xsl:value-of select="$businessPartnerCompanyName"/>
    <xsl:text> dated </xsl:text>
    <xsl:value-of select="$transactionDate"/>
    <xsl:text>is updated from </xsl:text>
    <xsl:value-of select="$transactionStatus"/>
    <xsl:text>to </xsl:text>
    <xsl:value-of select="$newTransactionStatus"/>
    <xsl:text>status. </xsl:text>

  </xsl:template>
</xsl:stylesheet>