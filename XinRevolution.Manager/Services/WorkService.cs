using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class WorkService : BaseService<WorkEntity, WorkMD>
    {
        private readonly string _containerName;

        public WorkService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.WorkContainer);
        }

        public override ServiceResultModel<WorkMD> Update(WorkMD metaData)
        {
            throw new NotImplementedException();
        }

        protected override WorkEntity ToEntity(WorkMD metaData)
        {
            return new WorkEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro,
                ResourceUrl = metaData.ResourceUrl,
                Controller = metaData.Controller
            };
        }

        protected override WorkMD ToMetaData(WorkEntity entity)
        {
            return new WorkMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Intro = entity.Intro,
                ResourceUrl = entity.ResourceUrl,
                Controller = entity.Controller
            };
        }
    }
}
