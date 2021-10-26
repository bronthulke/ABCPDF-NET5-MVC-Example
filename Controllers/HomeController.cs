using ABCPDFNET5MVCExample.Models;
using ABCPDFNET5MVCExample.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABCPDFNET5MVCExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(bool toPDF)
        {
            // Return view if there is an error
            if (!ModelState.IsValid)
            {
                return View();
            }

            // If user has clicked the export button
            if (toPDF)
            {
                // Render view output to string
                var report = await this.ViewToStringAsync("_TestPartial", "with my model");

                // Convert string to PDF using ABCpdf
                var pdfbytes = PDFHelper.PDFForHtml(report, Path.Combine(_webHostEnvironment.WebRootPath, "resources", "pdftemplate.html"));

                // Return file result
                return File(pdfbytes, System.Net.Mime.MediaTypeNames.Application.Pdf, "Report.pdf");
            }

            // Not exporting to PDF, show the view
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
