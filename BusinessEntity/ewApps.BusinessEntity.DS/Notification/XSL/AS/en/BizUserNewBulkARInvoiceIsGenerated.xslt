<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="newInvoiceCount"/>
  <xsl:param name="updatedInvoiceCount"/>
  <xsl:param name="updatedByName"/>
  <xsl:param name="updatedOn"/>

  <xsl:template match="/">

    <!--<Count> New A/R Invoice(s) imported and <Count> Existing A/R Invoice updated by <Updated By> on <date & time>.-->
    <xsl:value-of select="$newInvoiceCount"/>
    <xsl:text> New A/R Invoice(s) imported and </xsl:text>
    <xsl:value-of select="$updatedInvoiceCount"/>
    <xsl:text> Existing A/R Invoice updated by </xsl:text>
    <xsl:value-of select="$updatedByName"/>
    <xsl:text> on </xsl:text>
    <xsl:value-of select="$updatedOn"/>
    <xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>