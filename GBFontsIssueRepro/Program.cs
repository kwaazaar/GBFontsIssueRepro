using GemBox.Document;
using System;
using System.Linq;
using System.Reflection;

namespace GBFontsIssueRepro
{
    class Program
    {
        static void Main(string[] args)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            // Fonts are embedded resources
            FontSettings.FontsBaseResourceLocation = "/GBFontsIssueRepro;component/fonts/";

            // Load template from embedded resource also
            var templateStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GBFontsIssueRepro.Template_UniversOnly.docx");
            var doc = DocumentModel.Load(templateStream);

            // Save it
            var filename = $"Output_{Guid.NewGuid().ToString()}.pdf";
            Console.Write($"Saving {filename}...");

            try
            {
                doc.Save(filename, new PdfSaveOptions());
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex}");
            }
        }
    }
}
