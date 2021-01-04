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
    <!--<User name> is a new Business User onboard <Payment> application on <date & time>.-->
    Void is initiated for transaction <xsl:value-of select="$transactionID"/><xsl:text> for amount </xsl:text> <xsl:value-of select="$transactionAmount"/><xsl:text> of </xsl:text><xsl:value-of select="$businessPartnerCompanyName"/><xsl:text> by </xsl:text><xsl:value-of select="$businessUserName"/><xsl:text> on </xsl:text><xsl:value-of select="$actionDate"/>
  </xsl:template>
</xsl:stylesheet>
<!--Void is initiated for transaction <Tnx. No.> for amount <amount with currency> of <Customer Co. Name> by <Business User Name> on <date and time>.-->