using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using XinRevolution.CloudService.Model;

namespace XinRevolution.CloudService.AzureService.Interface
{
    public interface IAzureBlobService
    {
        CloudServiceReslutModel<string> Upload(string containerName, IFormFile file);

        CloudServiceReslutModel Remove(string resourceUrl);

        CloudServiceReslutModel Remove(List<string> resourceUrlList);
    }
}
