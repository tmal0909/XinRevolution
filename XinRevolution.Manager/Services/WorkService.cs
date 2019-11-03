using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
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

        public override ServiceResultModel<WorkMD> Create(WorkMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = string.Empty;

                return new ServiceResultModel<WorkMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Create(metaData);

            if (!result.Status)
            {
                if (metaData.ResourceFile != null && !string.IsNullOrEmpty(result.Data.ResourceUrl))
                {
                    DumpResource(result.Data.ResourceUrl);
                    DB.Commit();
                }

                result.Data.ResourceUrl = string.Empty;
            }

            return result;
        }

        public override ServiceResultModel<WorkMD> Update(WorkMD metaData)
        {
            var sourceData = DB.GetRepository<WorkEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = sourceData.ResourceUrl;

                return new ServiceResultModel<WorkMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Update(metaData);

            if (result.Status)
            {
                if (metaData.ResourceFile != null && !string.IsNullOrEmpty(sourceData.ResourceUrl))
                {
                    DumpResource(sourceData.ResourceUrl);
                    DB.Commit();
                }
            }
            else
            {
                if (metaData.ResourceFile != null && !string.IsNullOrEmpty(result.Data.ResourceUrl))
                {
                    DumpResource(result.Data.ResourceUrl);
                    DB.Commit();
                }

                result.Data.ResourceUrl = sourceData.ResourceUrl;
            }

            return result;
        }

        public override ServiceResultModel<WorkMD> Delete(WorkMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    DumpResource(metaData.ResourceUrl);
            }
            catch(Exception ex)
            {
                return new ServiceResultModel<WorkMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Delete(metaData);

            return result;
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
