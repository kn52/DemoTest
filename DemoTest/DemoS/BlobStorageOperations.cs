using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest.DemoS
{
    public class BlobStorageOperations
    {
        private static string ContainerName = "storepaypaymentbrandlogo";
        private static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storepaylogsnapshot;AccountKey=efZxQGu3S88Rdc+nfhAMIbkrhg5zweMjGMdkCNXC8bvP6PclYjY9yubvrcBim9jf9n8sNAMMir4k1gwdHTqMZw==;EndpointSuffix=core.windows.net";
        private static string FileName = "homeshop.jpg";
        private static string FileSource = @"C:\Users\Aashish\Desktop\homeshop.jpg";
        private static string downloadpath = @"C:\Users\Aashish\Downloads\DownloadImage\BlobFile\" + FileName;
        private static string FileMimeType = "image/jpeg";
        private static byte[] FileData;
        public static void GetFromBlobStorageContainer()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(ContainerName);
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(FileName);
                string imgURI = blockBlob.Uri.AbsoluteUri;
                Console.WriteLine(imgURI);
                //using (FileStream downloadFileStream = File.OpenWrite(downloadpath))
                {
                    //  blockBlob.DownloadToStreamAsync(downloadFileStream);
                    //BlobDownloadInfo blobdata = blockBlob.DownloadAsync();
                    //blobdata.Content.CopyToAsync(downloadFileStream);
                    //downloadFileStream.Close();
                }
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    blockBlob.DownloadToStreamAsync(memoryStream);
                //}
                //Stream blobStream = blockBlob.OpenReadAsync().Result;
                //return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
            }
            catch(Exception ex) { }
        }

        public static async Task UploadToBlobStorageContainer()
        {
            try
            {
                //var path = File.ReadAllBytes(FileSource);
                //using (FileStream fs = new FileStream(FileSource, FileMode.Open, FileAccess.Read))
                //{
                //    using (BinaryReader binaryReader = new System.IO.BinaryReader(fs))
                //    {
                //        long byteLength = new FileInfo(FileName).Length;
                //        FileData = binaryReader.ReadBytes((Int32)byteLength);
                //    }
                //}

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(StorageConnectionString);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(ContainerName);

                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                if (FileName != null && FileData != null)
                {
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(FileName);
                    cloudBlockBlob.Properties.ContentType = FileMimeType;
                    cloudBlockBlob.UploadFromByteArrayAsync(FileData, 0, FileData.Length);
                    Console.WriteLine(cloudBlockBlob.Uri.AbsoluteUri);
                }
            }
            catch(Exception ex) { }
        }
    }
}