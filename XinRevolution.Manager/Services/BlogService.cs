using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Enum;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class BlogService : BaseService<BlogEntity, BlogMD>
    {
        public BlogService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService _cloudService) : base(unitOfWork, _cloudService) { }

        public override ServiceResultModel<BlogMD> Delete(BlogMD metaData)
        {
            try
            {
                var dumpResources = DB.GetRepository<BlogPostEntity>()
                    .GetAll(x => x.BlogId == metaData.Id)
                    .Where(x => x.ReferenceType != ReferenceTypeEnum.Text && !string.IsNullOrEmpty(x.ReferenceContent))
                    .Select(x => x.ReferenceContent);

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<BlogTagEntity>().Delete(x => x.BlogId == metaData.Id);
                DB.GetRepository<BlogPostEntity>().Delete(x => x.BlogId == metaData.Id);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<BlogMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override BlogEntity ToEntity(BlogMD metaData)
        {
            return new BlogEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                ReleaseDate = metaData.ReleaseDate
            };
        }

        protected override BlogMD ToMetaData(BlogEntity entity)
        {
            return new BlogMD
            {
                Id = entity.Id,
                Name = entity.Name,
                ReleaseDate = entity.ReleaseDate
            };
        }
    }
}
