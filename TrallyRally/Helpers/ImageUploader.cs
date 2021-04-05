using System;
using System.IO;
using System.Drawing;

namespace TrallyRally.Helpers
{
    public class ImageUploader
    {
        static public string UploadJpeg(string base64Photo, string webRootPath, string publicPath)
        {
            byte[] photoData = Convert.FromBase64String(base64Photo);

            Image image;
            using (MemoryStream ms = new MemoryStream(photoData))
            {
                image = Image.FromStream(ms);
            }

            var fullPath = Path.Combine(webRootPath, publicPath);
            image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return publicPath;
        }

        static public string RandomJpegName()
        {
            return Guid.NewGuid().ToString() + ".jpg";
        }
    }
}
