using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EigenVectorsAndValuesUsingDotNet
{
    public class HtmlOutputMethods
    {
        public static string ToHtmlWithMathJax(string Latex)
        {
            string sGraph = @"
      <html>
      <head>
      <title></title>
      <meta charset=""utf-8"" />
<script type='text/javascript' async
  src = 'https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.5/MathJax.js?config=TeX-MML-AM_CHTML'>
</script>
<script type='text/x-mathjax-config'>
  MathJax.Hub.Register.StartupHook('End', function()
  {
    var x = document.getElementById('pLatex');
    x.style.display = 'block';
  });
</script>

      </ head>
      <body>
      <p id='pLatex' style='height: 300px; width: 300px;display:none'>
      \begin{equation}
       REPLACE_ME_WITH_TEXT
       \end{equation}     
      </p>
      </body>
      </html>
      ";

            return sGraph.Replace("REPLACE_ME_WITH_TEXT", Latex);
        }

        public static string ToHtmlWithMathJaxInline(string Latex)
        {
            string sGraph = @"
      <html>
      <head>
      <title></title>
      <meta charset=""utf-8"" />
<script type='text/javascript' async
  src = 'https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.5/MathJax.js?config=TeX-MML-AM_CHTML'>
</script>
<script type='text/x-mathjax-config'>
  MathJax.Hub.Register.StartupHook('End', function()
  {
    var x = document.getElementById('pLatex');
    x.style.display = 'block';
  });
</script>

      </ head>
      <body>
      <p id='pLatex' style='height: 300px; width: 300px;display:none'>
      $$
       REPLACE_ME_WITH_TEXT
      $$     
      </p>
      </body>
      </html>
      ";

            return sGraph.Replace("REPLACE_ME_WITH_TEXT", Latex);
        }

        private static string Browser = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"; //Change to your browser's path

        public static void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                // throw 
            }
        }


        public static void WriteLatexToHtmlAndLaunch(string Latex, string FileName)
        {
            string path = Directory.GetCurrentDirectory();

            string fil = path + "\\" + FileName;
            string text = HtmlOutputMethods.ToHtmlWithMathJaxInline(Latex);

            System.IO.File.WriteAllText(fil, text);
            OpenBrowser(fil);
        }

    }

}