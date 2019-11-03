using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
