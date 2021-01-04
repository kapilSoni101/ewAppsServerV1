<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="ticketID"/>
  <xsl:param name="customerCompanyName"/>  
  
  <xsl:template match="/">
    <!--New Customer Ticket <Ticket ID> of <Customer Co. Name> user received.-->
    <xsl:text> New Customer Ticket </xsl:text> <xsl:value-of select="$ticketID"/> <xsl:text> of </xsl:text><xsl:value-of select="$customerCompanyName"/><xsl:text> user received.</xsl:text>
  </xsl:template>
</xsl:stylesheet>