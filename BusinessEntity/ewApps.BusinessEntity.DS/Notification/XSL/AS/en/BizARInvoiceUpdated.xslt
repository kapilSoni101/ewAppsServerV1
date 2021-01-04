<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="standaloneMode"/>
  <xsl:param name="invoiceNo"/>
  <xsl:param name="updatedByName"/>
  <xsl:param name="updatedOn"/>
 
  <xsl:template match="/">
    <!--Existing A/R Invoice <Invoice ID> updated by <Updated By> on <date & time>..-->
    <xsl:text>Existing A/R Invoice </xsl:text>
    <xsl:value-of select="$invoiceNo"/>
    <!--<xsl:if test="$standaloneMode='1'">-->
      <xsl:text> updated by </xsl:text>
      <xsl:value-of select="$updatedByName"/>
    <!--</xsl:if>-->
    <xsl:text> updated on </xsl:text>
    <xsl:value-of select="$updatedOn"/>
  </xsl:template>
</xsl:stylesheet>