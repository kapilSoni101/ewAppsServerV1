<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="publisherCompanyName"/>
  <xsl:param name="invoiceID"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="totalAmount"/>
  <xsl:param name="generatedDate"/>
  <xsl:param name="dueDate"/>

  <xsl:template match="/">
    <!--New Invoice <Invoice Number> of amount <amount with currency> is generated against 
    <Customer Co. Name> on <generated date & time> and is due on <due date>.-->
    
    <xsl:text>New Invoide </xsl:text><xsl:value-of select="$invoiceID"/>
    <xsl:text> of amount </xsl:text>
    <xsl:value-of select="$totalAmount"/><xsl:text> is generated against </xsl:text>
    <xsl:value-of select="$businessPartnerCompanyName"/><xsl:text> on </xsl:text>
    <xsl:value-of select="$generatedDate"/> <xsl:text> and is due on </xsl:text><xsl:value-of select="$dueDate"/>.
    <!--<xsl:text>Regards </xsl:text>
    <xsl:value-of select="$publisherCompanyName"/>-->
  </xsl:template>
</xsl:stylesheet>