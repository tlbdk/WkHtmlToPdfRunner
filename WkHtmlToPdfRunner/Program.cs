using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WkHtmlToPdfRunner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wkhtmltopdfexe = @"C:\repos\git\WkHtmlToPdfRunner\WkHtmlToPdfRunner\lib\wkhtmltopdf.exe";
            var header = @"C:\repos\git\WkHtmlToPdfRunner\WkHtmlToPdfRunner\header.html";
            var footer = @"C:\repos\git\WkHtmlToPdfRunner\WkHtmlToPdfRunner\footer.html";
            var pages = @"C:\repos\git\WkHtmlToPdfRunner\WkHtmlToPdfRunner\sample.html";
            var output = @"C:\repos\git\WkHtmlToPdfRunner\WkHtmlToPdfRunner\sample.pdf";


            var options = new List<string>()
            {
                "--page-width 216mm",
                "--page-height 279mm",
                "--dpi 96",
                "--image-quality 100",
                //"--margin-top 16mm",
                "--margin-right 14mm",
                "--margin-bottom 25mm",
                "--margin-left 14mm",
                "--header-spacing 15",
                "--footer-spacing 5",
                "--disable-smart-shrinking",
                "--no-outline",
                //"user-style-sheet extension/myextension/design/standard/stylesheets/pdf.css",
                "--header-html " + header,
                "--footer-html " + footer
            };

            var si = new ProcessStartInfo
            {
                CreateNoWindow = false,
                FileName = wkhtmltopdfexe,
                Arguments = string.Join(" ", options) + " " + pages + " " + output,
                UseShellExecute = false,
                RedirectStandardError = false
            };

            using (var process = new Process())
            {
                process.StartInfo = si;
                process.Start();

                if (!process.WaitForExit(50000))
                {
                    throw new Exception("Timed out waiting for wkhtmltopdf.exe to finish");
                }

                if (process.ExitCode != 0)
                {
                    throw new Exception("Process returned non 0 error code");
                }

                if (!File.Exists(output)) {
                    throw new Exception("Output file is missing");
                }
            }
        }
    }
}
