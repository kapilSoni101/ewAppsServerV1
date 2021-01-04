<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:fo="http://www.w3.org/1999/XSL/Format">
  <xsl:output method="text" indent="no" encoding="utf-8"/>

  <xsl:param name="transactionID"/>
  <xsl:param name="transactionAmount"/>
  <xsl:param name="businessPartnerCompanyName"/>
  <xsl:param name="actionDate"/>
  <xsl:param name="newTransactionStatus"/>
  <xsl:param name="oldTransactionStatus"/>
  
  <xsl:template match="/">
    <!--<User name> is a new Business User onboard <Payment> application on <date & time>.-->
    Status of Transaction <xsl:value-of select="$transactionID"/><xsl:text> for amount </xsl:text><xsl:value-of select="$transactionAmount"/><xsl:text> 
is updated from </xsl:text><xsl:value-of select="$oldTransactionStatus"/><xsl:text> to </xsl:text><xsl:value-of select="$newTransactionStatus"/>
<xsl:text> on </xsl:text><xsl:value-of select="$actionDate"/>

  </xsl:template>
</xsl:stylesheet>

<!--Status of Transaction <Tnx. No.> of <Customer Co. Name> for amount <amount with currency> is updated from <old status> to <new status> on <date & time>.-->

<!--Status of Transaction <Tnx. No.> for amount <amount with currency> 
is updated from <old status> to <new status> on <date & time>.-->