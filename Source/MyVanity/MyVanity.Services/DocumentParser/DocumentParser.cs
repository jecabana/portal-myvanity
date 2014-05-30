using System.IO;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using MyVanity.Common.Helpers;
using OpenXmlPowerTools;

namespace MyVanity.Services.DocumentParser
{
    public class DocumentParser : IDocumentParser
    {
        public string ParseDocument(Stream stream)
        {
            XNamespace w = "http://www.w3.org/1999/xhtml";
            string result;

            var byteArray = stream.ToByteArray((int) stream.Length);
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (var doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    var settings = new HtmlConverterSettings();
                    XElement html = HtmlConverter.ConvertToHtml(doc, settings);

                    // Note: the XHTML returned by ConvertToHtmlTransform contains objects of type
                    // XEntity. PtOpenXmlUtil.cs defines the XEntity class. See
                    // http://blogs.msdn.com/ericwhite/archive/2010/01/21/writing-entity-references-using-linq-to-xml.aspx
                    // for detailed explanation.
                    //
                    // If you further transform the XML tree returned by ConvertToHtmlTransform, you
                    // must do it correctly, or entities do not serialize properly.
                    var bodyContainer = html.Element(w + "body");
                    result = bodyContainer.ToStringNewLineOnAttributes();
                }
            }

            return result;
        }
    }
}
