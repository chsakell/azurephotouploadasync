using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace AzureUploadPhotosAsync.Utilities
{
    public class DbAccess
    {
        public void InsertPhotoToDb(int userID, string filename, Guid guid, int length, int albumID)
        {
            using (SqlConnection connection = new SqlConnection(Utilities.Params.GetConnectionString()))
            {
                // Insert the new record
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO AzurePhotos ([PhotoUser],[PhotoTitle],[PhotoGuid],[PhotoUploadFormat],[PhotoByteLength],[PhotoAlbum])
                            VALUES (@UserId, @PhotoTitle, @PhotoGuid,@PhotoUploadFormat ,@PhotoByteLength,@PhotoAlbum)";
                    command.Parameters.AddWithValue("@UserId", userID);
                    command.Parameters.AddWithValue("@PhotoTitle", filename);
                    command.Parameters.AddWithValue("@PhotoGuid", guid);
                    command.Parameters.AddWithValue("@PhotoUploadFormat", Path.GetExtension(filename));
                    command.Parameters.AddWithValue("@PhotoByteLength", length);
                    command.Parameters.AddWithValue("@PhotoAlbum", albumID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}