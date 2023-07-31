using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageExtractor
{
    public class Services
    {
        public void ImageService(byte[] imgBytes, string path)
        {
            try
            {
                Image img = null;
                MemoryStream ms = new MemoryStream(imgBytes);

                img = Image.FromStream(ms);

                //img.Save(path);

                //string outputFileName = "...";
                using (Image tempImage = img)
                {
                    tempImage.Save(path.Replace('/', '-') + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }

            }
            catch(Exception e)
            {
                Console.WriteLine($"ERROR:  {e.Message} Patientid : {path.Substring(path.LastIndexOf('\\')+1).Replace("~", " DocumentID: ")}");
            }
            
        }
    }
}
