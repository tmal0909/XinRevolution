using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueItemService : BaseService<IssueItemEntity, IssueItemMD>
    {
        private readonly string _containerName;

        public IssueItemService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.IssueItemContainer);
        }

        public ServiceResultModel<IEnumerable<IssueItemEntity>> Find(int issueId)
        {
            var result = new ServiceResultModel<IEnumerable<IssueItemEntity>>();

            try
            {
                var entities = DB.GetRepository<IssueItemEntity>().GetAll(x => x.IssueId == issueId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<IssueItemEntity>);
            }

            return result;
        }

        public override ServiceResultModel<IssueItemMD> Create(IssueItemMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = string.Empty;

                return new ServiceResultModel<IssueItemMD>
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

        public override ServiceResultModel<IssueItemMD> Update(IssueItemMD metaData)
        {
            var sourceData = DB.GetRepository<IssueItemEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = sourceData.ResourceUrl;

                return new ServiceResultModel<IssueItemMD>
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

        public override ServiceResultModel<IssueItemMD> Delete(IssueItemMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    DumpResource(metaData.ResourceUrl);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<IssueItemMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override IssueItemEntity ToEntity(IssueItemMD metaData)
        {
            return new IssueItemEntity
            {
                Id = metaData.Id,
                Title = metaData.Title,
                Content = metaData.Content,
                ResourceUrl = metaData.ResourceUrl,
                ReleaseDate = metaData.ReleaseDate,
                IssueId = metaData.IssueId
            };
        }

        protected override IssueItemMD ToMetaData(IssueItemEntity entity)
        {
            return new IssueItemMD
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                ResourceUrl = entity.ResourceUrl,
                ReleaseDate = entity.ReleaseDate,
                IssueId = entity.IssueId
            };
        }
    }
}
