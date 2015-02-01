using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureUploadPhotosAsync.Models
{
    public class AzurePhoto
    {
        public string PhotoUser { get; set; }
        public string PhotoGuid { get; set; }
        public string PhotoUploadFormat { get; set; }
        public string PhotoTitle { get; set; }
        public int PhotoAlbum { get; set; }
        public string HqPhotoURI
        {
            get
            {
                return Utilities.Params.HqPhotosBaseURI + PhotoUser + "/album/" + PhotoAlbum + "/" + PhotoGuid + PhotoUploadFormat;
            }
        }
        public string ThumbnailPhotoURI
        {
            get
            {
                return Utilities.Params.ThumbnailPhotosBaseURI + PhotoUser + "/album/" + PhotoAlbum + "/" + PhotoGuid + PhotoUploadFormat;
            }
        }

        public AzurePhoto(string guid, string uploadFormat, string title, string username, int album)
        {
            this.PhotoGuid = guid;
            this.PhotoUploadFormat = uploadFormat;
            this.PhotoTitle = title;
            this.PhotoUser = username;
            this.PhotoAlbum = album;
        }
    }
}