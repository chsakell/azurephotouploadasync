using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AzureUploadPhotosAsync.Utilities
{
    public class AzurePhotoClient
    {
        private CloudBlobContainer BlobContainer;
        CloudStorageAccount storageAccount;

        public AzurePhotoClient(string container)
        {
            // Retrieve storage account from connection string.
            // Emulator initialization
            storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a specific created container.
            BlobContainer = blobClient.GetContainerReference(container);

            if (!BlobContainer.Exists())
            {
                // create the container and set public access to the blobs
                BlobContainer.Create();
                BlobContainerPermissions permissions = new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                BlobContainer.SetPermissions(permissions);
            }
        }

        public string UploadFromStream(FileStream fileStream, string fileName)
        {
            // Create the container and blob.
            CloudBlockBlob blob = BlobContainer.GetBlockBlobReference(fileName);

            // Set the content type to image
            blob.Properties.ContentType = "image/" + Path.GetExtension(fileName).Replace(".", "");
            blob.UploadFromStream(fileStream);

            // Return a URI fro viewing the photo
            return blob.Uri.AbsoluteUri;
        }

        public string UploadFromBytes(byte[] fileBytes, string fileName)
        {
            // Create the container and blob.
            CloudBlockBlob blob = BlobContainer.GetBlockBlobReference(fileName);

            // Set the content type to image
            blob.Properties.ContentType = "image/" + Path.GetExtension(fileName).Replace(".", "");
            blob.UploadFromByteArray(fileBytes, 0, fileBytes.Length - 1);

            // Return a URI fro viewing the photo
            return blob.Uri.AbsoluteUri;
        }
    }
}