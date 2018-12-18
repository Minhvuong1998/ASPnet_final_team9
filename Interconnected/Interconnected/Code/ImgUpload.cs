using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Interconnected.Code
{
    public class ImgUpload
    {
        public string Upload(HttpPostedFileBase img, string path_)
        {
            Directory.CreateDirectory(path_);
            int dt = DateTime.Now.GetHashCode();
            //getting the name of the file
            var fn = dt + "_" + img.FileName;
            var fileName = Path.GetFileName(fn);

            //store file in the Books folder
            var path = Path.Combine(path_, fileName);
            try
            {
                img.SaveAs(path);
                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        internal string UploadFilename(HttpPostedFileBase img, string path_, string filename)
        {
            Directory.CreateDirectory(path_);

            //store file in the Books folder
            var path = Path.Combine(path_, filename);
            try
            {
                img.SaveAs(path);
                return filename;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ChangeFileName(HttpPostedFileBase img)
        {
            int dt = DateTime.Now.GetHashCode();
            //getting the name of the file
            var fn = dt + "_" + img.FileName;
            return Path.GetFileName(fn);
        }

        public void Delete(string path_)
        {
            if (File.Exists(path_))
            {
                File.Delete(path_);
            }
        }
    }
}