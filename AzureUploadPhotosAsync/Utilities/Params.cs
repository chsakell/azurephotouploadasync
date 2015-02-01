using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureUploadPhotosAsync.Utilities
{
    public class Params
    {
        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["AzureDB"].ToString();
        }

        public static string HqPhotosBaseURI = "http://127.0.0.1:10000/devstoreaccount1/azurephotos/hq/";
        public static string ThumbnailPhotosBaseURI = "http://127.0.0.1:10000/devstoreaccount1/azurephotos/thumbnail/";
    }
}