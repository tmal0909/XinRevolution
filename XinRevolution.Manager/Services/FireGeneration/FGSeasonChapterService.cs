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
    public class FGSeasonChapterService : BaseService<FGSeasonChapterEntity, FGSeasonChapterMD>
    {
        private readonly string _containerName;

        public FGSeasonChapterService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGSeasonChapterContainer);
        }

        public ServiceResultModel<FGSeasonChapterEntity> FindDetail(int chapterId)
        {
            var result = new ServiceResultModel<FGSeasonChapterEntity>();

            try
            {
                var entity = DB.GetRepository<FGSeasonChapterEntity>().Single(x => x.Id == chapterId, x => x.Include(y => y.Season));

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entity;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(FGSeasonChapterEntity);
            }

            return result;
        }

        public ServiceResultModel<IEnumerable<FGSeasonChapterEntity>> Find(int seasonId)
        {
            var result = new ServiceResultModel<IEnumerable<FGSeasonChapterEntity>>();

            try
            {
                var entities = DB.GetRepository<FGSeasonChapterEntity>().GetAll(x => x.SeasonId == seasonId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<FGSeasonChapterEntity>);
            }

            return result;
        }

        public override ServiceResultModel<FGSeasonChapterMD> Create(FGSeasonChapterMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = string.Empty;

                return new ServiceResultModel<FGSeasonChapterMD>
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

        public override ServiceResultModel<FGSeasonChapterMD> Update(FGSeasonChapterMD metaData)
        {
            var sourceData = DB.GetRepository<FGSeasonChapterEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = sourceData.ResourceUrl;

                return new ServiceResultModel<FGSeasonChapterMD>
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

        public override ServiceResultModel<FGSeasonChapterMD> Delete(FGSeasonChapterMD metaData)
        {
            try
            {
                var dumpResources = new List<string>();
                var comics = DB.GetRepository<FGChapterComicEntity>().GetAll(x => x.ChapterId == metaData.Id);

                dumpResources.AddRange(comics.Where(x => !string.IsNullOrEmpty(x.ResourceUrl)).Select(x => x.ResourceUrl));

                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    dumpResources.Add(metaData.ResourceUrl);

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<FGChapterComicEntity>().Delete(x => x.ChapterId == metaData.Id);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGSeasonChapterMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGSeasonChapterEntity ToEntity(FGSeasonChapterMD metaData)
        {
            return new FGSeasonChapterEntity
            {
                Id = metaData.Id,
                SerialNumber = metaData.SerialNumber,
                Name = metaData.Name,
                Intro = metaData.Intro,
                Offset = metaData.Offset,
                ResourceUrl = metaData.ResourceUrl,
                SeasonId = metaData.SeasonId
            };
        }

        protected override FGSeasonChapterMD ToMetaData(FGSeasonChapterEntity entity)
        {
            return new FGSeasonChapterMD
            {
                Id = entity.Id,
                SerialNumber = entity.SerialNumber,
                Name = entity.Name,
                Intro = entity.Intro,
                Offset = entity.Offset,
                ResourceUrl = entity.ResourceUrl,
                SeasonId = entity.SeasonId
            };
        }
    }
}
