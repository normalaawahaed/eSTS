using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

using Apps.Common;

namespace eSTS.Common
{
    public class QRCodeGen
    {

        public void RenderQrCode(string QRText,string level,string path)
        {
            try
            {
              
                QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(QRText, eccLevel))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {
                            Bitmap oBitmap = new Bitmap(50, 50);
                            using (MemoryStream memory = new MemoryStream())
                            {
                                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    oBitmap = qrCode.GetGraphic(3, Color.Black, Color.White, false);
                                    //oBitmap.Save(path);
                                    oBitmap.Save(memory, ImageFormat.Jpeg);
                                    byte[] bytes = memory.ToArray();
                                    fs.Write(bytes, 0, bytes.Length);
                                    Log.WriteMessageLog("path:" + path, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteMessageLog("error", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                Log.WriteLog(ex, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        //public void SaveBitmap()
        //{
        //    Bitmap myBitmap;
        //    ImageCodecInfo myImageCodecInfo;
        //    Encoder myEncoder;
        //    EncoderParameter myEncoderParameter;
        //    EncoderParameters myEncoderParameters;

        //    // Create a Bitmap object based on a BMP file.
        //    myBitmap = new Bitmap("Shapes.bmp");

        //    // Get an ImageCodecInfo object that represents the JPEG codec.
        //    myImageCodecInfo = GetEncoderInfo("image/jpeg");

        //    // Create an Encoder object based on the GUID

        //    // for the Quality parameter category.
        //    myEncoder = Encoder.Quality;

        //    // Create an EncoderParameters object.

        //    // An EncoderParameters object has an array of EncoderParameter

        //    // objects. In this case, there is only one

        //    // EncoderParameter object in the array.
        //    myEncoderParameters = new EncoderParameters(1);

        //    // Save the bitmap as a JPEG file with quality level 25.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 25L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    myBitmap.Save("Shapes025.jpg", myImageCodecInfo, myEncoderParameters);

        //    // Save the bitmap as a JPEG file with quality level 50.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 50L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    myBitmap.Save("Shapes050.jpg", myImageCodecInfo, myEncoderParameters);

        //    // Save the bitmap as a JPEG file with quality level 75.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 75L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    myBitmap.Save("Shapes075.jpg", myImageCodecInfo, myEncoderParameters);
        //}
        //private static ImageCodecInfo GetEncoderInfo(String mimeType)
        //{
        //    int j;
        //    ImageCodecInfo[] encoders;
        //    encoders = ImageCodecInfo.GetImageEncoders();
        //    for (j = 0; j < encoders.Length; ++j)
        //    {
        //        if (encoders[j].MimeType == mimeType)
        //            return encoders[j];
        //    }
        //    return null;
        //}


    }

}