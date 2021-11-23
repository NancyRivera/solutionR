using System;
using System.Configuration;
using ZXing;
using System.IO;
namespace SGOUtil.Barcode
{
    public class GenerateImgPdf417:IGenerateBarCode
    {
        public void GenerateImg(string contents)
        {
            try
            {
                var barcodeWriter = new BarcodeWriter();
                var path = ConfigurationManager.AppSettings["pathBarCode"];
                var arr = contents.Split('|');
                path += $"{arr[0].Trim()}-{arr[1].Trim()}-{arr[2].Trim()}-{arr[3].Trim()}.png";
                barcodeWriter.Format = BarcodeFormat.PDF_417;

                if (File.Exists(path))
                    File.Delete(path);
                barcodeWriter.Write(contents).Save(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Error: {e.Message} n {e.Data}]");
            }
            
        }
    }
}
