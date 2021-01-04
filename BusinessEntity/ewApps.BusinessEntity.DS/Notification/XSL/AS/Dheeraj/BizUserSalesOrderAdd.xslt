﻿<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="standaloneMode"/>
  <xsl:param name="salesOrderNo"/>
  <xsl:param name="customerName"/>
  <xsl:param name="totalAmountWithCurrency"/>
  <xsl:param name="postingDate"/>
  <xsl:param name="deliveryDate"/>
  <xsl:param name="createdByName"/>

  <xsl:template match="/">

    <!--New Sales Order <ID> generated against <Customer Co. Name> of amount <amount with currency> on posting date <date> and delivery date <Date> by <Created By>.-->
    <xsl:text>New Sales Order </xsl:text>
    <xsl:value-of select="$salesOrderNo"/>
    <xsl:text> generated against </xsl:text>
    <xsl:value-of select="$customerName"/>
    <xsl:text> of amount </xsl:text>
    <xsl:value-of select="$totalAmountWithCurrency"/>
    <xsl:text> with posting date </xsl:text>
    <xsl:value-of select="$postingDate"/>
    <xsl:text> and delivery date </xsl:text>
    <xsl:value-of select="$deliveryDate"/>
    <xsl:if test="$standaloneMode='1'">
      <xsl:text> by </xsl:text>
      <xsl:value-of select="$createdByName"/>
    </xsl:if>
    <xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>