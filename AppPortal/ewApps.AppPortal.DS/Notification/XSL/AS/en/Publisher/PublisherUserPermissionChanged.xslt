<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>
  <xsl:param name="publisherUserNameChange"/>  
  <xsl:param name="dateTime"/>
  <xsl:template match="/">    
   
    <xsl:text>My permissions on Publisher Portal has been updated by  </xsl:text> <xsl:value-of select="$publisherUserNameChange"/><xsl:text> on </xsl:text><xsl:value-of select="$dateTime"/><xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>