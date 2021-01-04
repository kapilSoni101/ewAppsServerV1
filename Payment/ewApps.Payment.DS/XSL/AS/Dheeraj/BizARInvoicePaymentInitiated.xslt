<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="transactionID"/>
  <xsl:param name="transactionAmount"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="businessUserName"/>
  <xsl:param name="actionDate"/>
  
  <xsl:template match="/">
    Void is initiated for transaction <xsl:value-of select="$transactionID"/><xsl:text> for amount </xsl:text> <xsl:value-of select="$transactionAmount"/><xsl:text> of </xsl:text><xsl:value-of select="$businessPartnerCompanyName"/><xsl:text> by </xsl:text><xsl:value-of select="$businessUserName"/><xsl:text> on </xsl:text><xsl:value-of select="$actionDate"/>
  </xsl:template>
</xsl:stylesheet>
<!--Payment of <amount with currency> with Transaction No. <Tnx. No.> is initiated by <Business User name> on <Date and Time>.-->