<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>
  <xsl:param name="ID"/>
  <xsl:param name="updatedBy"/> 
  <xsl:param name="dateTime"/>

  <xsl:template match="/">

    <xsl:text>My Ticket </xsl:text>
    <xsl:value-of select="$ID"/>
    <xsl:text> is updated by </xsl:text>
    <xsl:value-of select="$updatedBy"/>  
      <xsl:text> on </xsl:text>
    <xsl:value-of select="$dateTime"/>
    <xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>