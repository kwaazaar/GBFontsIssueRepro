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

            // MS Core Fonts (custom fonts are added to this same folder)
            // FontSettings.FontsBaseDirectory = "/usr/share/fonts/truetype/msttcorefonts";

            var fontFiles = FontSettings.Fonts.Select(ff => ff.FamilyName + ", Italic: " + ff.Italic + ", Weight: " + ff.Weight + ", Stretch: " + ff.Stretch).ToArray();
            File.WriteAllLines("/app/output/FontFiles.txt", fontFiles);

            GenerateDocument("Template_All.docx");
        }

        private static void GenerateDocument(string templateName)
        {
            var templateStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GBFontsIssueRepro." + templateName);
            var doc = DocumentModel.Load(templateStream);

            // Save it
            var filename = $"/app/output/Output_{templateName}_{DateTime.Now.Ticks}.pdf";
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
