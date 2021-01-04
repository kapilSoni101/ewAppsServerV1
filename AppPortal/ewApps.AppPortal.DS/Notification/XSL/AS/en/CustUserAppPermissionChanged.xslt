<?xml version="1.0" encoding="utf-8"?>

<!--My permissions for <App Name> application has been
 updated by <Modified By> on <date & time>.-->

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>
  
 <xsl:param name="applicationName"/>
    <xsl:param name="userNameChange"/>
  <xsl:param name="actionTime"/>
  
  <xsl:template match="/">   
    <xsl:text> My permissions for </xsl:text> <xsl:value-of select="$applicationName"/><xsl:text> application has been updated by </xsl:text> <xsl:value-of select="$userNameChange"/><xsl:text>  on </xsl:text><xsl:value-of select="$actionTime"/><xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>