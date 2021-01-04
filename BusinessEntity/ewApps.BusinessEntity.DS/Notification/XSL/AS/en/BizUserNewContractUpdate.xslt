<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="standaloneMode"/>
  <xsl:param name="customerName"/>
  <xsl:param name="createdByName"/>
  <xsl:param name="createdOn"/>

  <xsl:template match="/">
    <!--New Customer <Customer Co. Name> created by <Created By> on <date & time>.-->
    <xsl:text>New Customer </xsl:text>
    <xsl:value-of select="$customerName"/>
    <xsl:if test="$standaloneMode='1'">
      <xsl:text> created by </xsl:text>
      <xsl:value-of select="$createdByName"/>
    </xsl:if>
    <xsl:text> on </xsl:text>
    <xsl:value-of select="$createdOn"/>
  </xsl:template>
</xsl:stylesheet>