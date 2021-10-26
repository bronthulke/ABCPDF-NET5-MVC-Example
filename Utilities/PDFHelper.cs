using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf12;

namespace ABCPDFNET5MVCExample.Utilities
{
    public class PDFHelper
    {
        public static byte[] PDFForHtml(string html, string templatePath)
        {
            // Create ABCpdf Doc object
            var doc = new Doc();

            //load the template for this code to be presented nicely
            string strTemplateCode;
            TextReader tr = new StreamReader(templatePath);
            strTemplateCode = tr.ReadToEnd();
            tr.Close();

            //put the panel code into the template - this adds additional CSS and required HTML container tags
            string htmlInTemplate = strTemplateCode.Replace("[RAWData]", html);
            //strControlAsString = processHTMLForForm(strControlAsString, PDFMode);

            // Add html to Doc
            int theID = doc.AddImageHtml(htmlInTemplate);

            //Loop through document to create multi-page PDF
            while (true)
            {
                if (!doc.Chainable(theID))
                    break;
                doc.Page = doc.AddPage();
                theID = doc.AddImageToChain(theID);
            }

            // Flatten the PDF
            for (int i = 1; i <= doc.PageCount; i++)
            {
                doc.PageNumber = i;
                doc.Flatten();
            }

            // Get PDF as byte array. Couls also use .Save() to save to disk
            var pdfbytes = doc.GetData();

            doc.Clear();

            return pdfbytes;
        }
    }
}
