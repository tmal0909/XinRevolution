using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Database.Enum.FireGeneration;
using XinRevolution.Extension;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGRoleResourceService : BaseService<FGRoleResourceEntity, FGRoleResourceMD>
    {
        private readonly string _containerName;

        public FGRoleResourceService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGRoleResourceContainer);
        }

        public ServiceResultModel<IEnumerable<FGRoleResourceEntity>> Find(int roleId)
        {
            var result = new ServiceResultModel<IEnumerable<FGRoleResourceEntity>>();

            try
            {
                var entities = DB.GetRepository<FGRoleResourceEntity>().GetAll(x => x.RoleId == roleId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<FGRoleResourceEntity>);
            }

            return result;
        }
        
        public override ServiceResultModel<FGRoleResourceMD> Create(FGRoleResourceMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = string.Empty;

                return new ServiceResultModel<FGRoleResourceMD>
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

        public override ServiceResultModel<FGRoleResourceMD> Update(FGRoleResourceMD metaData)
        {
            var sourceData = DB.GetRepository<FGRoleResourceEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                metaData.ResourceUrl = sourceData.ResourceUrl;

                return new ServiceResultModel<FGRoleResourceMD>
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

        public override ServiceResultModel<FGRoleResourceMD> Delete(FGRoleResourceMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    DumpResource(metaData.ResourceUrl);
            }
            catch(Exception ex)
            {
                return new ServiceResultModel<FGRoleResourceMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGRoleResourceEntity ToEntity(FGRoleResourceMD metaData)
        {
            return new FGRoleResourceEntity
            {
                Id = metaData.Id,
                Type = metaData.Type,
                ResourceUrl = metaData.ResourceUrl,
                Sort = metaData.Sort,
                RoleId = metaData.RoleId
            };
        }

        protected override FGRoleResourceMD ToMetaData(FGRoleResourceEntity entity)
        {
            return new FGRoleResourceMD
            {
                Id = entity.Id,
                Type = entity.Type,
                ResourceUrl = entity.ResourceUrl,
                Sort = entity.Sort,
                RoleId = entity.RoleId,
                TypeOptions = GetOptions()
            };
        }

        public List<SelectListItem> GetOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = EnumHelper<FGRoleResourceTypeEnum>.GetDisplayValue(FGRoleResourceTypeEnum.Main1), Value = ((int)FGRoleResourceTypeEnum.Main1).ToString()},
                new SelectListItem { Text = EnumHelper<FGRoleResourceTypeEnum>.GetDisplayValue(FGRoleResourceTypeEnum.Main2), Value = ((int)FGRoleResourceTypeEnum.Main2).ToString()},
                new SelectListItem { Text = EnumHelper<FGRoleResourceTypeEnum>.GetDisplayValue(FGRoleResourceTypeEnum.Character1), Value = ((int)FGRoleResourceTypeEnum.Character1).ToString()},
                new SelectListItem { Text = EnumHelper<FGRoleResourceTypeEnum>.GetDisplayValue(FGRoleResourceTypeEnum.Character2), Value = ((int)FGRoleResourceTypeEnum.Character2).ToString()},
            };
        }
    }
}
