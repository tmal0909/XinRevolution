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
            throw new NotImplementedException();
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
