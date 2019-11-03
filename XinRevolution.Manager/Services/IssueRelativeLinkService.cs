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
    public class IssueRelativeLinkService : BaseService<IssueRelativeLinkEntity, IssueRelativeLinkMD>
    {
        private readonly string _containerName;

        public IssueRelativeLinkService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.IssueRelativeLinkContainer);
        }

        public ServiceResultModel<IEnumerable<IssueRelativeLinkEntity>> Find(int issueId)
        {
            var result = new ServiceResultModel<IEnumerable<IssueRelativeLinkEntity>>();

            try
            {
                var entities = DB.GetRepository<IssueRelativeLinkEntity>().GetAll(x => x.IssueId == issueId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<IssueRelativeLinkEntity>);
            }

            return result;
        }

        public override ServiceResultModel<IssueRelativeLinkMD> Create(IssueRelativeLinkMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = string.Empty;

                return new ServiceResultModel<IssueRelativeLinkMD>
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

        public override ServiceResultModel<IssueRelativeLinkMD> Update(IssueRelativeLinkMD metaData)
        {
            var sourceData = DB.GetRepository<IssueRelativeLinkEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = sourceData.ResourceUrl;

                return new ServiceResultModel<IssueRelativeLinkMD>
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

        public override ServiceResultModel<IssueRelativeLinkMD> Delete(IssueRelativeLinkMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    DumpResource(metaData.ResourceUrl);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<IssueRelativeLinkMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override IssueRelativeLinkEntity ToEntity(IssueRelativeLinkMD metaData)
        {
            return new IssueRelativeLinkEntity
            {
                Id = metaData.Id,
                LinkUrl = metaData.LinkUrl,
                ResourceUrl = metaData.ResourceUrl,
                Note = metaData.Note,
                IssueId = metaData.IssueId
            };
        }

        protected override IssueRelativeLinkMD ToMetaData(IssueRelativeLinkEntity entity)
        {
            return new IssueRelativeLinkMD
            {
                Id = entity.Id,
                LinkUrl = entity.LinkUrl,
                ResourceUrl = entity.ResourceUrl,
                Note = entity.Note,
                IssueId = entity.IssueId
            };
        }
    }
}
