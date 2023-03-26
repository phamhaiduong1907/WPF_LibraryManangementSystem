using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace LibraryManagementSystem.Utility
{
    class ImageHandler
    {
        private static ImageHandler instance;
        private static readonly object instanceLock = new object();
        private ImageHandler() { }
        public static ImageHandler Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ImageHandler();
                    }
                    return instance;
                }
            }
        }

        private Cloudinary cloudinary;
        private const string CLOUD_NAME = "dw9re61vc";
        private const string API_KEY = "385823156987158";
        private const string API_SECRET = "9f3Kvj_Q7t7kiAoURUf_Xg0OFWQ";

        public string uploadImage(string path)
        {
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path)
            };
            ImageUploadResult result = cloudinary.Upload(uploadParams);
            return result.Url.ToString();
        }
    }
}
