using Microsoft.EntityFrameworkCore;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGGroupRoleService : BaseService<FGGroupRoleEntity, FGGroupRoleMD>
    {
        public FGGroupRoleService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

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
