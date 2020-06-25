using GemBox.Document;
using System;
using System.IO;
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
            FontSettings.FontsBaseResourceLocation = "/fonts/";

            GenerateDocument("Template_Calibri.docx");
            GenerateDocument("Template_Univers.docx");
        }

        private static void GenerateDocument(string templateName)
        {
            var templateStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GBFontsIssueRepro." + templateName);
            var doc = DocumentModel.Load(templateStream);

            // Save it
            var filename = $"Output_{templateName}_{DateTime.Now.Ticks}.pdf";
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
