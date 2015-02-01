using AjaxControlToolkit;
using AzureUploadPhotosAsync.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AzureUploadPhotosAsync
{
    public partial class UploadPhotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void File_Upload(object sender, AjaxFileUploadEventArgs e)
        {
            string filename = e.FileName;
            int userID = 1; // Foreign key to Table Users..
            int albumID = 10; // Foreign key to Table Albums..
            string username = "chsakell"; // USE Context.User.Identity.Name for authenticated users..

            try
            {
                // Resize image before upload | Create thumbnail for previews..
                Stream fileStream = e.GetStreamContents();
                ImageResizer imRes = new ImageResizer(307200, fileStream, Path.GetExtension(filename));
                byte[] imageThumbnail;
                byte[] scaledUploadFile = imRes.ScaleImageFromStream(out imageThumbnail, 200, 200);

                // Generate a Guid for public file name..
                Guid guid = Guid.NewGuid();
                AzurePhotoClient blobClient = new AzurePhotoClient("azurephotos");
                blobClient.UploadFromBytes(scaledUploadFile, "hq/" + username + "/album/" + albumID + "/" + guid + Path.GetExtension(filename));
                blobClient.UploadFromBytes(imageThumbnail, "thumbnail/" + username + "/album/" + albumID + "/" + guid + Path.GetExtension(filename));

                // Log photo to DB..
                DbAccess dbAccess = new DbAccess();
                dbAccess.InsertPhotoToDb(userID, filename, guid, scaledUploadFile.Length, albumID);

                e.DeleteTemporaryData();
            }
            catch (Exception ex)
            {
                // Log error..
            }
        }
    }
}