<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="ID"/>
  <xsl:param name="customerCompanyName"/>
  <xsl:param name="trackingNo"/>
  <xsl:param name="shipDate"/>
  <xsl:param name="expectedDate"/>
  <xsl:param name="userName"/>

  <xsl:template match="/">
    <!--New Customer <Customer Co. Name> created by <Created By> on <date & time>.-->
    <xsl:text>New Draft Delivery </xsl:text>
    <xsl:value-of select="$ID"/>
    <xsl:text>generated against </xsl:text>
    <xsl:value-of select="$customerCompanyName"/>   
      <xsl:text> with Tracking No.</xsl:text>
      <xsl:value-of select="$trackingNo"/>   
    <xsl:text> Ship Date </xsl:text>
    <xsl:value-of select="$shipDate"/>
    <xsl:text> and Expected Delivery Date </xsl:text>
    <xsl:value-of select="$expectedDate"/>
   <xsl:text> By </xsl:text>
    <xsl:value-of select="$userName"/>
  </xsl:template>
</xsl:stylesheet>