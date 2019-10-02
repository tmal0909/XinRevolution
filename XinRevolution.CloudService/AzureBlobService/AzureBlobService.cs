using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using XinRevolution.CloudService.AzureBlobService.Interface;
using XinRevolution.CloudService.Model;

namespace XinRevolution.CloudService.AzureBlobService
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly string _connectionString;

        public AzureBlobService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CloudServiceReslutModel<string> Save(string containerName, IFormFile file)
        {
            var result = new CloudServiceReslutModel<string>();

            using (var stream = new MemoryStream())
            {
                try
                {
                    if (file == null || file.Length <= 0)
                        throw new Exception($"檔案異常");

                    file.CopyTo(stream);

                    var fileName = $"{new Guid().ToString().ToLower()}.{Path.GetExtension(file.FileName)}";
                    var storageAccount = CloudStorageAccount.Parse(_connectionString);
                    var blobClient = storageAccount.CreateCloudBlobClient();

                    var blobContainer = blobClient.GetContainerReference(containerName.ToLower());
                    blobContainer.CreateIfNotExists();

                    var permision = new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob };
                    blobContainer.SetPermissions(permision);

                    var blockBlob = blobContainer.GetBlockBlobReference(fileName);
                    blockBlob.UploadFromStream(stream);

                    result.Status = true;
                    result.Message = $"操作成功";
                    result.Data = blockBlob.Uri.AbsoluteUri;
                }
                catch (Exception ex)
                {
                    result.Status = false;
                    result.Message = ex.Message;
                    result.Data = default(string);
                }
            }

            return result;
        }

        public CloudServiceReslutModel Remove(string resourceUrl)
        {
            var result = new CloudServiceReslutModel();

            try
            {
                var blockBlob = new CloudBlockBlob(new Uri(resourceUrl));
                blockBlob.Delete();

                result.Status = true;
                result.Message = $"操作成功";
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public CloudServiceReslutModel Remove(List<string> resourceUrlList)
        {
            var result = new CloudServiceReslutModel();

            try
            {
                foreach(var resourceUrl in resourceUrlList)
                {
                    var blockBlob = new CloudBlockBlob(new Uri(resourceUrl));
                    blockBlob.Delete();
                }

                result.Status = true;
                result.Message = $"操作成功";
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
