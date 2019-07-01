using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace MemeGenerator.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Check if file is really an image
        /// Source: https://stackoverflow.com/questions/11063900/determine-if-uploaded-file-is-image-any-format-on-mvc
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsValidImageFile(IFormFile file)
        {

            try
            {
                var isValidImage = Image.FromStream(file.OpenReadStream());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
