<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="transactionID"/>
  <xsl:param name="transactionAmount"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="actionDate"/>
  <xsl:param name="TransactionStatus"/>
  <xsl:param name="payeeName"/>
  <xsl:param name="businessCompanyName"/>
  
  <xsl:template match="/">   
    <!--<User name> is a new Business User onboard <Payment> application on <date & time>.-->
    Advance Payment of <xsl:value-of select="$transactionAmount"/> for <xsl:value-of select="$businessPartnerCompanyName"/>  with Transaction No. <xsl:value-of select="$transactionID"/><xsl:text> is initiated by </xsl:text> <xsl:value-of select="$payeeName"/> <xsl:text> on </xsl:text>  <xsl:value-of select="$actionDate"/><xsl:text>.</xsl:text>
  </xsl:template>
</xsl:stylesheet>
 <!-- Advance Payment of <amount with currency> for <Customer Co. Name>. with Transaction No. <Tnx. No.> is initiated by <Business User Name> on <Date and Time>. . -->