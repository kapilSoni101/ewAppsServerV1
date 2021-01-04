
using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace ewApps.Core.ExportService {

  /// <summary>
  /// This class provides supporting methods and business logics for PDF export.  
  /// </summary>
  public class PDFExport {

      
    /// <summary>
    /// Converts HTML string to pdf document and returns file stream of pdf document.
    /// </summary>
    /// <param name="htmlContent">HTML string to be converted in PDF.</param>
    /// <param name="fileName">Download pdf File name.</param>
    /// <param name="baseURLPath">CSS path/ image path</param>
    /// <returns>File stream result of PDF document</returns>
    public static FileResult ConvertByHTMLString(string htmlContent, string fileName, string baseURLPath) {
      // instantiate a html to pdf converter object 
      HtmlToPdf converter = new HtmlToPdf();

      // set css @media print
      converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;


      // set converter options
      converter.Options.PdfPageSize = PdfPageSize.A4;
      converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

      converter.Options.WebPageWidth = 1024;
      converter.Options.WebPageHeight = 0;
      converter.Options.WebPageFixedSize = false;

      converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
      converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

      // create a new pdf document converting an url 
      PdfDocument doc = converter.ConvertHtmlString(htmlContent, baseURLPath);
      //.ConvertUrl(model.HtmlContent);

      // save pdf document 
      byte[] pdf = doc.Save();
      //doc.Save(fileName);

      // close pdf document 
      doc.Close();

      // return resulted pdf document 
      FileResult fileResult = new FileContentResult(pdf, "application/pdf");
      fileResult.FileDownloadName = fileName + ".pdf";

      // System.Diagnostics.Process.Start(@"cmd.exe ", @"/c C:\documents\selectpdf\" + fileResult.FileDownloadName);
      return fileResult;
    }

  }
}
