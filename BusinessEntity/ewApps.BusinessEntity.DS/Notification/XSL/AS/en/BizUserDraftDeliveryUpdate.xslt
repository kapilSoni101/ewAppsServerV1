<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="ID"/>
  <xsl:param name="customerCompanyName"/>  
  <xsl:param name="dateTime"/>
  <xsl:param name="userName"/>

  <xsl:template match="/">
    <!--New Customer <Customer Co. Name> created by <Created By> on <date & time>.-->
    <xsl:text>Existing Draft Delivery </xsl:text>
    <xsl:value-of select="$ID"/>
    <xsl:text>for </xsl:text>
    <xsl:value-of select="$customerCompanyName"/>   
      <xsl:text> > updated by</xsl:text>
  <xsl:value-of select="$userName"/>
     <xsl:text> on </xsl:text>
    <xsl:value-of select="$dateTime"/>  
  </xsl:template>
</xsl:stylesheet>