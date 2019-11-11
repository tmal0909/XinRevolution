using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGGroupRoleService : BaseService<FGGroupRoleEntity, FGGroupRoleMD>
    {
        public FGGroupRoleService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        public ServiceResultModel<IEnumerable<FGGroupRoleEntity>> Find(int groupId)
        {
            var result = new ServiceResultModel<IEnumerable<FGGroupRoleEntity>>();

            try
            {
                var entities = DB.GetRepository<FGGroupRoleEntity>().GetAll(x => x.GroupId == groupId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<FGGroupRoleEntity>);
            }

            return result;
        }

        public override ServiceResultModel<FGGroupRoleMD> Delete(FGGroupRoleMD metaData)
        {
            try
            {
                var dumpResources = new List<string>();
                var roleResources = DB.GetRepository<FGRoleResourceEntity>().GetAll(x => x.RoleId == metaData.Id);
                var roleEquipments = DB.GetRepository<FGRoleEquipmentEntity>().GetAll(x => x.RoleId == metaData.Id);

                dumpResources.AddRange(roleResources.Select(x => x.ResourceUrl));
                dumpResources.AddRange(roleEquipments.Select(x => x.SlideResourceUrl));
                dumpResources.AddRange(roleEquipments.Select(x => x.MainResourceUrl));

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<FGRoleResourceEntity>().Delete(x => x.RoleId == metaData.Id);
                DB.GetRepository<FGRoleEquipmentEntity>().Delete(x => x.RoleId == metaData.Id);
            }
            catch(Exception ex)
            {
                return new ServiceResultModel<FGGroupRoleMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGGroupRoleEntity ToEntity(FGGroupRoleMD metaData)
        {
            return new FGGroupRoleEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro,
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
                RelativeLinkUrl = entity.RelativeLinkUrl,
                GroupId = entity.GroupId
            };
        }
    }
}
