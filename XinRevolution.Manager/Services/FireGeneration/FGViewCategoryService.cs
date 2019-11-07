using Microsoft.EntityFrameworkCore;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGViewCategoryService : BaseService<FGViewCategoryEntity, FGViewCategoryMD>
    {
        public FGViewCategoryService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        protected override FGViewCategoryEntity ToEntity(FGViewCategoryMD metaData)
        {
            return new FGViewCategoryEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Sort = metaData.Sort
            };
        }

        protected override FGViewCategoryMD ToMetaData(FGViewCategoryEntity entity)
        {
            return new FGViewCategoryMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Sort = entity.Sort
            };
        }
    }
}
