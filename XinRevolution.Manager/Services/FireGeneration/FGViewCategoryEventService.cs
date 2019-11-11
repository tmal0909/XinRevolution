using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGViewCategoryEventService : BaseService<FGViewCategoryEvnentEntity, FGViewCategoryEventMD>
    {
        private readonly string _containerName;

        public FGViewCategoryEventService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGRoleResourceContainer);
        }

        public ServiceResultModel<IEnumerable<FGViewCategoryEvnentEntity>> Find(int categoryId)
        {
            var result = new ServiceResultModel<IEnumerable<FGViewCategoryEvnentEntity>>();

            try
            {
                var entities = DB.GetRepository<FGViewCategoryEvnentEntity>().GetAll(x => x.CategoryId == categoryId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<FGViewCategoryEvnentEntity>);
            }

            return result;
        }

        public override ServiceResultModel<FGViewCategoryEventMD> Create(FGViewCategoryEventMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGViewCategoryEventMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Create(metaData);

            if (!result.Status)
            {
                if (metaData.ResourceFile != null && !string.IsNullOrEmpty(metaData.ResourceUrl))
                {
                    DumpResource(metaData.ResourceUrl);
                    DB.Commit();
                }

                metaData.ResourceUrl = string.Empty;
            }

            return result;
        }

        public override ServiceResultModel<FGViewCategoryEventMD> Update(FGViewCategoryEventMD metaData)
        {
            var sourceData = DB.GetRepository<FGViewCategoryEvnentEntity>().Single(x => x.Id == metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGViewCategoryEventMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
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

        public override ServiceResultModel<FGViewCategoryEventMD> Delete(FGViewCategoryEventMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    DumpResource(metaData.ResourceUrl);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGViewCategoryEventMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGViewCategoryEvnentEntity ToEntity(FGViewCategoryEventMD metaData)
        {
            return new FGViewCategoryEvnentEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Title = metaData.Title,
                ResourceUrl = metaData.ResourceUrl,
                Intro = metaData.Intro,
                Sort = metaData.Sort,
                CategoryId = metaData.CategoryId
            };
        }

        protected override FGViewCategoryEventMD ToMetaData(FGViewCategoryEvnentEntity entity)
        {
            return new FGViewCategoryEventMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Title = entity.Title,
                ResourceUrl = entity.ResourceUrl,
                Intro = entity.Intro,
                Sort = entity.Sort,
                CategoryId = entity.CategoryId
            };
        }
    }
}
