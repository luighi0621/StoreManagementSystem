using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace StoreManagement.Common
{
    public static class ImageHelper
    {
        public static byte[] fileTobytes(IFormFile file)
        {
            byte[] filebyes = null;
            using (var reader = file.OpenReadStream())
            {
                using (var mem = new MemoryStream())
                {
                    reader.CopyTo(mem);
                    filebyes = mem.ToArray();
                }
            }
            return filebyes;
        }

        public static string bytesToString(byte[] bytes)
        {
            var base64 = Convert.ToBase64String(bytes);
            return string.Format("data:image/png;base64,{0}", base64);
        }
    }
}
