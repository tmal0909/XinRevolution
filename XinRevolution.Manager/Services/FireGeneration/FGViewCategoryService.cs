using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGViewCategoryService : BaseService<FGViewCategoryEntity, FGViewCategoryMD>
    {
        public FGViewCategoryService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        public override ServiceResultModel<FGViewCategoryMD> Delete(FGViewCategoryMD metaData)
        {
            try
            {
                var dumpResources = DB.GetRepository<FGViewCategoryEvnentEntity>()
                    .GetAll(x => x.CategoryId == metaData.Id)
                    .Select(x => x.ResourceUrl);

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<FGViewCategoryEvnentEntity>().Delete(x => x.CategoryId == metaData.Id);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGViewCategoryMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

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
