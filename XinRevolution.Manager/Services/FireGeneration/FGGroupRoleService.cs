using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGGroupRoleService : BaseService<FGGroupRoleEntity, FGGroupRoleMD>
    {
        private readonly string _containerName;

        public FGGroupRoleService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGGroupRoleContainer);
        }

        public override ServiceResultModel<FGGroupRoleMD> Create(FGGroupRoleMD metaData)
        {
            try
            {
                if (metaData.CoverMainResourceFile != null)
                    metaData.CoverMainResourceUrl = UploadResource(_containerName, metaData.CoverMainResourceFile, ResourceTypeEnum.Image);

                if (metaData.CoverViceResourceFile != null)
                    metaData.CoverViceResourceUrl = UploadResource(_containerName, metaData.CoverViceResourceFile, ResourceTypeEnum.Image);

                if (metaData.CharacterMainResourceFile != null)
                    metaData.CharacterMainResourceUrl = UploadResource(_containerName, metaData.CharacterMainResourceFile, ResourceTypeEnum.Image);

                if (metaData.CharacterViceResourceFile != null)
                    metaData.CharacterViceResourceUrl = UploadResource(_containerName, metaData.CharacterViceResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.CoverMainResourceUrl = string.Empty;
                metaData.CoverViceResourceUrl = string.Empty;
                metaData.CharacterMainResourceUrl = string.Empty;
                metaData.CharacterViceResourceUrl = string.Empty;

                return new ServiceResultModel<FGGroupRoleMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Create(metaData);

            if (!result.Status)
            {
                DumpResource(new List<string>
                {
                    metaData.CoverMainResourceUrl,
                    metaData.CoverViceResourceUrl,
                    metaData.CharacterMainResourceUrl,
                    metaData.CharacterViceResourceUrl
                });
                DB.Commit();

                metaData.CoverMainResourceUrl = string.Empty;
                metaData.CoverViceResourceUrl = string.Empty;
                metaData.CharacterMainResourceUrl = string.Empty;
                metaData.CharacterViceResourceUrl = string.Empty;
            }

            return result;
        }

        public override ServiceResultModel<FGGroupRoleMD> Update(FGGroupRoleMD metaData)
        {
            var sourceData = DB.GetRepository<FGGroupRoleEntity>().Single(metaData.Id);

            try
            {
                if (metaData.CoverMainResourceFile != null)
                    metaData.CoverMainResourceUrl = UploadResource(_containerName, metaData.CoverMainResourceFile, ResourceTypeEnum.Image);

                if (metaData.CoverViceResourceFile != null)
                    metaData.CoverViceResourceUrl = UploadResource(_containerName, metaData.CoverViceResourceFile, ResourceTypeEnum.Image);

                if (metaData.CharacterMainResourceFile != null)
                    metaData.CharacterMainResourceUrl = UploadResource(_containerName, metaData.CharacterMainResourceFile, ResourceTypeEnum.Image);

                if (metaData.CharacterViceResourceFile != null)
                    metaData.CharacterViceResourceUrl = UploadResource(_containerName, metaData.CharacterViceResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                if (!sourceData.CoverMainResourceUrl.Equals(metaData.CoverMainResourceUrl, StringComparison.CurrentCultureIgnoreCase))
                {
                    DumpResource(metaData.CoverMainResourceUrl);
                    metaData.CoverMainResourceUrl = sourceData.CoverMainResourceUrl;
                }

                if (!sourceData.CoverViceResourceUrl.Equals(metaData.CoverViceResourceUrl, StringComparison.CurrentCultureIgnoreCase))
                {
                    DumpResource(metaData.CoverViceResourceUrl);
                    metaData.CoverViceResourceUrl = sourceData.CoverMainResourceUrl;
                }

                if (!sourceData.CharacterMainResourceUrl.Equals(metaData.CharacterMainResourceUrl, StringComparison.CurrentCultureIgnoreCase))
                {
                    DumpResource(metaData.CharacterMainResourceUrl);
                    metaData.CharacterMainResourceUrl = sourceData.CoverMainResourceUrl;
                }

                if (!sourceData.CharacterViceResourceUrl.Equals(metaData.CharacterViceResourceUrl, StringComparison.CurrentCultureIgnoreCase))
                {
                    DumpResource(metaData.CharacterViceResourceUrl);
                    metaData.CharacterViceResourceUrl = sourceData.CoverMainResourceUrl;
                }

                return new ServiceResultModel<FGGroupRoleMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Update(metaData);

            if (result.Status)
            {
                if (metaData.CoverMainResourceFile != null && !string.IsNullOrEmpty(sourceData.CoverMainResourceUrl))
                    DumpResource(sourceData.CoverMainResourceUrl);

                if(metaData.CoverViceResourceFile != null && !string.IsNullOrEmpty(sourceData.CoverViceResourceUrl))
                    DumpResource(sourceData.CoverMainResourceUrl);

                if (metaData.CharacterMainResourceFile != null && !string.IsNullOrEmpty(sourceData.CharacterMainResourceUrl))
                    DumpResource(sourceData.CharacterMainResourceUrl);

                if (metaData.CharacterViceResourceFile != null && !string.IsNullOrEmpty(sourceData.CharacterViceResourceUrl))
                    DumpResource(sourceData.CharacterViceResourceUrl);

                DB.Commit();
            }
            else
            {
                if (metaData.CoverMainResourceFile != null && !string.IsNullOrEmpty(result.Data.CoverMainResourceUrl))
                    DumpResource(result.Data.CoverMainResourceUrl);

                if (metaData.CoverViceResourceFile != null && !string.IsNullOrEmpty(result.Data.CoverViceResourceUrl))
                    DumpResource(result.Data.CoverMainResourceUrl);

                if (metaData.CharacterMainResourceFile != null && !string.IsNullOrEmpty(result.Data.CharacterMainResourceUrl))
                    DumpResource(result.Data.CharacterMainResourceUrl);

                if (metaData.CharacterViceResourceFile != null && !string.IsNullOrEmpty(result.Data.CharacterViceResourceUrl))
                    DumpResource(result.Data.CharacterViceResourceUrl);

                DB.Commit();

                result.Data.CoverMainResourceUrl = sourceData.CoverMainResourceUrl;
                result.Data.coverviceur
            }

            return result;
        }

        public override ServiceResultModel<FGGroupRoleMD> Delete(FGGroupRoleMD metaData)
        {
            throw new NotImplementedException();
        }

        protected override FGGroupRoleEntity ToEntity(FGGroupRoleMD metaData)
        {
            return new FGGroupRoleEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro,
                CoverMainResourceUrl = metaData.CoverMainResourceUrl,
                CoverViceResourceUrl = metaData.CoverViceResourceUrl,
                CharacterMainResourceUrl = metaData.CharacterMainResourceUrl,
                CharacterViceResourceUrl = metaData.CharacterViceResourceUrl,
                RelativeLinkUrl = metaData.RelativeLinkUrl,
                GroupId = metaData.GroupId
            };
        }

        protected override FGGroupRoleMD ToMetaData(FGGroupRoleEntity entity)
        {
            return new FGGroupRoleMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Intro = entity.Intro,
                CoverMainResourceUrl = entity.CoverMainResourceUrl,
                CoverViceResourceUrl = entity.CoverViceResourceUrl,
                CharacterMainResourceUrl = entity.CharacterMainResourceUrl,
                CharacterViceResourceUrl = entity.CharacterViceResourceUrl,
                RelativeLinkUrl = entity.RelativeLinkUrl,
                GroupId = entity.GroupId
            };
        }
    }
}
