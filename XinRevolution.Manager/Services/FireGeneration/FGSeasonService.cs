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
    public class FGSeasonService : BaseService<FGSeasonEntity, FGSeasonMD>
    {
        private readonly string _containerName;

        public FGSeasonService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGSeasonContainer);
        }

        public override ServiceResultModel<FGSeasonMD> Create(FGSeasonMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGSeasonMD>
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

        public override ServiceResultModel<FGSeasonMD> Update(FGSeasonMD metaData)
        {
            var sourceData = DB.GetRepository<FGSeasonEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGSeasonMD>
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

            return result; ;
        }

        public override ServiceResultModel<FGSeasonMD> Delete(FGSeasonMD metaData)
        {
            try
            {
                var dumpResources = new List<string>();

                var chapters = DB.GetRepository<FGSeasonChapterEntity>().GetAll(x => x.SeasonId == metaData.Id);
                var chapterIds = chapters.Select(x => x.Id);
                var chapterComics = DB.GetRepository<FGChapterComicEntity>().GetAll(x => chapterIds.Contains(x.ChapterId));

                dumpResources.AddRange(chapters.Where(x => !string.IsNullOrEmpty(x.ResourceUrl)).Select(x => x.ResourceUrl));
                dumpResources.AddRange(chapterComics.Where(x => !string.IsNullOrEmpty(x.ResourceUrl)).Select(x => x.ResourceUrl));

                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    dumpResources.Add(metaData.ResourceUrl);

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<FGSeasonChapterEntity>().Delete(x => x.SeasonId == metaData.Id);
                DB.GetRepository<FGChapterComicEntity>().Delete(x => chapterIds.Contains(x.ChapterId));
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGSeasonMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGSeasonEntity ToEntity(FGSeasonMD metaData)
        {
            return new FGSeasonEntity
            {
                Id = metaData.Id,
                SerialNumber = metaData.SerialNumber,
                Name = metaData.Name,
                ResourceUrl = metaData.ResourceUrl
            };
        }

        protected override FGSeasonMD ToMetaData(FGSeasonEntity entity)
        {
            return new FGSeasonMD
            {
                Id = entity.Id,
                SerialNumber = entity.SerialNumber,
                Name = entity.Name,
                ResourceUrl = entity.ResourceUrl
            };
        }
    }
}
