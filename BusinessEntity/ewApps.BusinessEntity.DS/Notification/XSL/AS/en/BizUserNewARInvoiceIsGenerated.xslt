<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="standaloneMode"/>
  <xsl:param name="invoiceNo"/>
  <xsl:param name="customerName"/>
  <xsl:param name="totalAmountWithCurrency"/>
  <xsl:param name="dueDate"/>
  <xsl:param name="createdByName"/>

  <xsl:template match="/">

    <!--New A/R Invoice <Invoice Number> generated against <Customer Co. Name> of amount <amount with currency> with Due Date <Date> by <Created By>.-->
    <xsl:text>New A/R Invoice </xsl:text>
    <xsl:value-of select="$invoiceNo"/>
    <xsl:text> generated against </xsl:text>
    <xsl:value-of select="$customerName"/>
    <xsl:text> of amount </xsl:text>
    <xsl:value-of select="$totalAmountWithCurrency"/>
    <xsl:text> with Due Date </xsl:text>
    <xsl:value-of select="$dueDate"/>
    <!--<xsl:if test="$standaloneMode='1'">-->
      <xsl:text> by </xsl:text>
      <xsl:value-of select="$createdByName"/>
    <!--</xsl:if>-->
    <xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>