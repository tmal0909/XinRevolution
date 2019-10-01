using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XinRevolution.CloudService.Model;

namespace XinRevolution.CloudService.AzureBlobService.Interface
{
    public interface IAzureBlobService
    {
        CloudServiceReslutModel<string> Save(string containerName, IFormFile file);

        CloudServiceReslutModel Remove(string resourceUrl);

        CloudServiceReslutModel Remove(List<string> resourceUrlList);
    }
}
