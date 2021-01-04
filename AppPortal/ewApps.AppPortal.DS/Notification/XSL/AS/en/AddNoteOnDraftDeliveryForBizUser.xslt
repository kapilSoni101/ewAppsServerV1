<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>
  <xsl:param name="userType"/>  
  <xsl:param name="userName"/>
  <xsl:param name="ID"/>
  <xsl:param name="customerCompanyName"/>
  <xsl:param name="dateTime"/>
  
  <xsl:template match="/">    
   
    <xsl:text>New Note added by </xsl:text> <xsl:value-of select="$userType"/><xsl:text> User </xsl:text> <xsl:value-of select="$userName"/><xsl:text> in Draft Delivery </xsl:text> <xsl:value-of select="$ID"/><xsl:text> of </xsl:text><xsl:value-of select="$customerCompanyName"/><xsl:text> on </xsl:text><xsl:value-of select="$dateTime"/><xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>