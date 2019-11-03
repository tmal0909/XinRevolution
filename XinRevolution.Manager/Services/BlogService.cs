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
            var result = new ServiceResultModel<BlogMD>();

            try
            {
                var blogPostResources = _unitOfWork.GetRepository<BlogPostEntity>()
                    .GetAll(x => x.BlogId == metaData.Id)
                    .Where(x => x.ReferenceType != ReferenceTypeEnum.Text)
                    .Select(x => x.ReferenceContent);

                if (blogPostResources.Count() > 0)
                    DumpResource(blogPostResources);

                _unitOfWork.GetRepository<BlogTagEntity>().Delete(x => x.BlogId == metaData.Id);
                _unitOfWork.GetRepository<BlogPostEntity>().Delete(x => x.BlogId == metaData.Id);
                _unitOfWork.GetRepository<BlogEntity>().Delete(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法刪除資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

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
