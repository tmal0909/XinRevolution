using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGChapterComicService : BaseService<FGChapterComicEntity, FGChapterComicMD>
    {
        private readonly string _containerName;

        public FGChapterComicService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGSeasonChapterComicContainer);
        }

        public ServiceResultModel<IEnumerable<FGChapterComicEntity>> Find(int chapterId)
        {
            var result = new ServiceResultModel<IEnumerable<FGChapterComicEntity>>();

            try
            {
                var entities = DB.GetRepository<FGChapterComicEntity>().GetAll(x => x.ChapterId == chapterId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<FGChapterComicEntity>);
            }

            return result;
        }
        
        public override ServiceResultModel<FGChapterComicMD> Create(FGChapterComicMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = string.Empty;

                return new ServiceResultModel<FGChapterComicMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
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
            }

            return result;
        }

        public override ServiceResultModel<FGChapterComicMD> Update(FGChapterComicMD metaData)
        {
            var sourceData = DB.GetRepository<FGChapterComicEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = sourceData.ResourceUrl;

                return new ServiceResultModel<FGChapterComicMD>
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

        public override ServiceResultModel<FGChapterComicMD> Delete(FGChapterComicMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    DumpResource(metaData.ResourceUrl);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGChapterComicMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGChapterComicEntity ToEntity(FGChapterComicMD metaData)
        {
            return new FGChapterComicEntity
            {
                Id= metaData.Id,
                Page = metaData.Page,
                ResourceUrl = metaData.ResourceUrl,
                Note = metaData.Note,
                ChapterId = metaData.ChapterId
            };
        }

        protected override FGChapterComicMD ToMetaData(FGChapterComicEntity entity)
        {
            return new FGChapterComicMD
            {
                Id = entity.Id,
                Page = entity.Page,
                ResourceUrl = entity.ResourceUrl,
                Note = entity.Note,
                ChapterId = entity.ChapterId
            };
        }
    }
}
