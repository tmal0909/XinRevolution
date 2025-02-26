﻿using Microsoft.EntityFrameworkCore;
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
    public class FGGroupService : BaseService<FGGroupEntity, FGGroupMD>
    {
        private readonly string _containerName;

        public FGGroupService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGGroupContainer);
        }

        public override ServiceResultModel<FGGroupMD> Create(FGGroupMD metaData)
        {
            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGGroupMD>
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

        public override ServiceResultModel<FGGroupMD> Update(FGGroupMD metaData)
        {
            var sourceData = DB.GetRepository<FGGroupEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ResourceFile != null)
                    metaData.ResourceUrl = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGGroupMD>
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

        public override ServiceResultModel<FGGroupMD> Delete(FGGroupMD metaData)
        {
            try
            {
                var dumpResources = new List<string>();
                var roleIds = DB.GetRepository<FGGroupRoleEntity>().GetAll(x => x.GroupId == metaData.Id).Select(x => x.Id);
                var roleResources = DB.GetRepository<FGRoleResourceEntity>().GetAll(x => roleIds.Contains(x.RoleId));
                var equipments = DB.GetRepository<FGRoleEquipmentEntity>().GetAll(x => roleIds.Contains(x.RoleId));

                dumpResources.AddRange(roleResources.Select(x => x.ResourceUrl));
                dumpResources.AddRange(equipments.Select(x => x.SlideResourceUrl));
                dumpResources.AddRange(equipments.Select(x => x.MainResourceUrl));

                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    dumpResources.Add(metaData.ResourceUrl);

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<FGRoleEquipmentEntity>().Delete(x => roleIds.Contains(x.RoleId));
                DB.GetRepository<FGRoleResourceEntity>().Delete(x => roleIds.Contains(x.RoleId));
                DB.GetRepository<FGGroupRoleEntity>().Delete(x => x.GroupId == metaData.Id);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGGroupMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGGroupEntity ToEntity(FGGroupMD metaData)
        {
            return new FGGroupEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                ResourceUrl = metaData.ResourceUrl,
                Note = metaData.Note,
                Sort = metaData.Sort
            };
        }

        protected override FGGroupMD ToMetaData(FGGroupEntity entity)
        {
            return new FGGroupMD
            {
                Id = entity.Id,
                Name = entity.Name,
                ResourceUrl = entity.ResourceUrl,
                Note = entity.Note,
                Sort = entity.Sort
            };
        }
    }
}
