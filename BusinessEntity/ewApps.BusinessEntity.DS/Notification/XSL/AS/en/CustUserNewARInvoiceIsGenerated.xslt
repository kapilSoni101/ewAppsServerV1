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

    <xsl:text>New A/P Invoice </xsl:text>
    <xsl:value-of select="$invoiceNo"/>
    <xsl:text> generated </xsl:text>
    <xsl:value-of select="$customerName"/>
    <xsl:text> of amount </xsl:text>
    <xsl:value-of select="$totalAmountWithCurrency"/>
    <xsl:text> with Due Date </xsl:text>
    <xsl:value-of select="$dueDate"/>
 <xsl:text> by </xsl:text>
      <xsl:value-of select="$createdByName"/>
    <xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>