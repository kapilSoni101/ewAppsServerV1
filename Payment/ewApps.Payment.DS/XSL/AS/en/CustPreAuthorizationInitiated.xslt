<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="transactionID"/>
  <xsl:param name="transactionAmount"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="actionDate"/>
  <xsl:param name="TransactionStatus"/>
  <xsl:param name="businessUserName"/>
  <xsl:param name="businessCompanyName"/>
  <xsl:template match="/">   
    <!--<User name> is a new Business User onboard <Payment> application on <date & time>.-->
    Pre - Auth of amount <xsl:value-of select="$transactionAmount"/> is secured by <xsl:value-of select="$businessCompanyName"/><xsl:text> on </xsl:text> <xsl:value-of select="$actionDate"/>
  </xsl:template>
</xsl:stylesheet>
 <!-- Payment of <amount with currency> with Transaction No. <Tnx. No.> is initiated on <Date and Time> by <Business User name>. -->