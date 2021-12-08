using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DemoTest.DemoS
{
    public class BlobStorageOperations
    {
        public static void GetBlobStorageContainer()
        {
            string ContainerName = "storepaypaymentbrandlogo";
            string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storepaylogsnapshot;AccountKey=efZxQGu3S88Rdc+nfhAMIbkrhg5zweMjGMdkCNXC8bvP6PclYjY9yubvrcBim9jf9n8sNAMMir4k1gwdHTqMZw==;EndpointSuffix=core.windows.net";
            string FileName = "homeshop.jpg";
            string downloadpath = @"C:\Users\Aashish\Downloads\DownloadImage\BlobFile\" + FileName;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(ContainerName);
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(FileName);
            string imgURI = blockBlob.Uri.AbsoluteUri;
            Console.WriteLine(imgURI);
            using (FileStream downloadFileStream = File.OpenWrite(downloadpath))
            {
                blockBlob.DownloadToStreamAsync(downloadFileStream);
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
    }
}
