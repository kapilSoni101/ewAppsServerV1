/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul badgujar <abadgujar@batchmaster.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace ewApps.Core.CommonService
{/// <summary>
 /// This class provides utility methods to send emails. 
 /// </summary>
  public static class EmailHelper
  {

    ///// <summary>
    ///// Send seperate email to each given set of recepient address.
    ///// </summary>
    ///// <param name="subject">Email subject.</param>
    ///// <param name="body">Email body.</param>
    ///// <param name="recepientAddress">List of recepient email address.</param>
    ///// <param name="attachments">Email attachments.</param>
    ///// <param name="logoPNGImage">The logo PNG image.</param>
    ///// <param name="htmlBody">True if provided email body is in html format otherwise body is treated as plain text.</param>
    //public static void SendEmail(string subject, string body, string[] recepientAddress, IDictionary<string, MemoryStream> attachments, System.Drawing.Bitmap logoPNGImage, bool htmlBody = true)
    //{
    //  if (AppSettingHelper.NotificationEnable() && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(body))
    //  {
    //    MailMessage mail = null;

    //    // Get from email address from configuration file.
    //    string fromEmail = AppSettingHelper.NotificationSenderEmail();

    //    // Instantiate smtp client based on configured SMTP information in configuration file.
    //    SmtpClient emailClient = ReadSMTPConfigurationInfo();

    //    LinkedResource lr = null;
    //    MemoryStream imageStream = null;
    //    System.Drawing.Bitmap logoImage = null;

    //    // If email is send as html body then only image logo is add into body.
    //    if (htmlBody && logoPNGImage != null)
    //    {
    //      // Build memory stream of logo image.
    //      imageStream = new MemoryStream();
    //      logoImage = logoPNGImage;
    //      logoImage.Save(imageStream, ImageFormat.Png);
    //      imageStream.Position = 0;

    //      // Build linked resource of image.
    //      lr = new LinkedResource(imageStream, "image/png");
    //      lr.ContentId = "logoImage";
    //      lr.TransferEncoding = TransferEncoding.Base64;
    //    }

    //    foreach (string email in recepientAddress)
    //    {
    //      // Build mail message for each target.
    //      mail = new MailMessage();
    //      // Add attachments (if provided).
    //      if (attachments != null)
    //      {
    //        foreach (KeyValuePair<string, MemoryStream> attachmentStream in attachments)
    //        {
    //          attachmentStream.Value.Position = 0;
    //          Attachment attachment = new Attachment(attachmentStream.Value, attachmentStream.Key);
    //          mail.Attachments.Add(attachment);
    //        }
    //      }
    //      MailAddress senderAddress = new MailAddress(fromEmail, AppSettingHelper.NotificationSenderName());
    //      mail.From = senderAddress;
    //      mail.ReplyToList.Add(senderAddress);
    //      mail.BodyEncoding = System.Text.Encoding.UTF8;
    //      mail.To.Add(email.Trim());
    //      mail.Subject = subject.Trim();
    //      mail.Body = body;

    //      // Set email body format.
    //      mail.IsBodyHtml = htmlBody;

    //      AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
    //      // Skip linked resource in case of plain text body.
    //      if (htmlBody)
    //      {
    //        if (lr != null)
    //        {
    //          htmlView.LinkedResources.Add(lr);
    //        }
    //      }
    //      mail.AlternateViews.Add(htmlView);

    //      // Send email.
    //      emailClient.Send(mail);

    //    }
    //    if (imageStream != null)
    //      imageStream.Close();
    //    if (logoImage != null)
    //      logoImage.Dispose();
    //  }
    //}

    ///// <summary>
    ///// Send email to given 'To' email list, 'CC' email list with provided subject, body and attachments.
    ///// </summary>
    ///// <param name="subject">Email subject.</param>
    ///// <param name="body">Email body.</param>
    ///// <param name="fromEmail">Email sender email shown in From field.</param>
    ///// <param name="fromEmailDisplayName">Sender display name in From field.</param>
    ///// <param name="toRecipientAddress">Recipients list for 'To' field.</param>
    ///// <param name="ccRecipientAddress">Recipients list for 'CC' field.</param>
    ///// <param name="attachments">Attachement list to be send as email attachment.</param>
    ///// <param name="htmlBody">True if email is HTML otherwise false.</param>
    ///// <param name="replyToRecipientAddress">Recipients list for 'ReplyTo' field.</param>
    //public static void SendSingleEmailToAllReceipients(string subject, string body, string fromEmail, string fromEmailDisplayName, string[] toRecipientAddress, string[] ccRecipientAddress, IDictionary<string, MemoryStream> attachments, bool htmlBody = true, KeyValuePair<string, string>? replyToRecipientAddress = null)
    //{
    //  if (AppSettingHelper.NotificationEnable())
    //  {
    //    MailMessage mail = null;

    //    if (string.IsNullOrEmpty(fromEmailDisplayName))
    //    {
    //      fromEmailDisplayName = fromEmail;
    //    }

    //    // Instantiate smtp client based on configured SMTP information in configuration file.
    //    SmtpClient emailClient = ReadSMTPConfigurationInfo();

    //    LinkedResource lr = null;
    //    MemoryStream imageStream = null;
    //    System.Drawing.Bitmap logoImage = null;

    //    // If email is send as html body then only image logo is add into body.
    //    if (htmlBody)
    //    {
    //      // Build memory stream of logo image.
    //      imageStream = new MemoryStream();
    //      logoImage = PFResource.AppLogo;
    //      logoImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
    //      imageStream.Position = 0;

    //      // Build linked resource of image.
    //      lr = new LinkedResource(imageStream, "image/png");
    //      lr.ContentId = "logoImage";
    //      lr.TransferEncoding = TransferEncoding.Base64;
    //    }

    //    // Build mail message for each target.
    //    mail = new MailMessage();
    //    MailAddress senderAddress = new MailAddress(fromEmail, fromEmailDisplayName);
    //    mail.From = senderAddress;
    //    if (replyToRecipientAddress != null)
    //    {
    //      MailAddress replyToAddress = new MailAddress(replyToRecipientAddress.Value.Key, replyToRecipientAddress.Value.Value);

    //      mail.ReplyToList.Add(replyToAddress);
    //    }
    //    else
    //      mail.ReplyToList.Add(senderAddress);
    //    mail.BodyEncoding = System.Text.Encoding.UTF8;
    //    mail.Subject = subject.Trim();
    //    mail.Body = body;

    //    // Set email body format.
    //    mail.IsBodyHtml = htmlBody;

    //    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
    //    // Skip linked resource in case of plain text body.
    //    if (htmlBody)
    //    {
    //      htmlView.LinkedResources.Add(lr);
    //    }

    //    mail.AlternateViews.Add(htmlView);

    //    foreach (string email in toRecipientAddress)
    //    {
    //      mail.To.Add(email.Trim());
    //    }

    //    // Add CC recipients email address (if provided).
    //    if (ccRecipientAddress != null)
    //    {
    //      foreach (string ccEmail in ccRecipientAddress)
    //      {
    //        mail.CC.Add(ccEmail);
    //      }
    //    }

    //    // Add attachments (if provided).
    //    if (attachments != null)
    //    {
    //      foreach (KeyValuePair<string, MemoryStream> attachmentStream in attachments)
    //      {
    //        attachmentStream.Value.Position = 0;
    //        Attachment attachment = new Attachment(attachmentStream.Value, attachmentStream.Key);
    //        mail.Attachments.Add(attachment);
    //      }

    //    }

    //    // Send email.
    //    emailClient.Send(mail);



    //    if (imageStream != null)
    //      imageStream.Close();
    //    if (logoImage != null)
    //      logoImage.Dispose();
    //  }
    //}

    //// Read smtp server configuration from config file.
    //private static SmtpClient ReadSMTPConfigurationInfo()
    //{
    //  SmtpClient SmtpServer = new SmtpClient(AppSettingHelper.GetSMTPServerAddress());
    //  SmtpServer.Credentials = new NetworkCredential(AppSettingHelper.NotificationSenderUserId(), AppSettingHelper.NotificationSenderPassword());
    //  SmtpServer.EnableSsl = false;
    //  SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
    //  return SmtpServer;
    //}
  }
}
