using System;
using System.Text;
using System.Web;

namespace HBS.Data.Entities.TimeTracking.Models
{
    public class ExportPage : IExportPage
    {
        public void ExportExcel(string data, string fileName)
        {
            Export("application/vnd.ms-excel", ConvertToAscii(data), fileName);
        }

        private string ConvertToAscii(string unicodeString)
        {
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            return asciiString;
        }

        private void Export(string contentType, string data, string fileName)
        {
            //EnableViewState = false;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = contentType;
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", fileName));
            HttpContext.Current.Response.Write(data);
            HttpContext.Current.Response.End();

        }
    }
}
