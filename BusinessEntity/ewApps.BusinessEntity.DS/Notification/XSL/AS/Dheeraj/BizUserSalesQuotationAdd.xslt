<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="standaloneMode"/>
  <xsl:param name="salesQuotationNo"/>
  <xsl:param name="customerName"/>
  <xsl:param name="createdByName"/>

  <xsl:template match="/">

    <!--New Sales Order <ID> generated against <Customer Co. Name>  by <Created By>.-->
    <xsl:text> New Sales Order </xsl:text>
    <xsl:value-of select="$salesQuotationNo"/>
    <xsl:text> generated against </xsl:text>
    <xsl:value-of select="$customerName"/>
    <xsl:if test="$standaloneMode='1'">
      <xsl:text> by </xsl:text>
      <xsl:value-of select="$createdByName"/>
    </xsl:if>
    <xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>