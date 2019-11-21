using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class BlogTagService : BaseService<BlogTagEntity, BlogTagMD>
    {
        public BlogTagService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        public ServiceResultModel<IEnumerable<BlogTagEntity>> Find(int blogId)
        {
            var result = new ServiceResultModel<IEnumerable<BlogTagEntity>>();

            try
            {
                var entities = DB.GetRepository<BlogTagEntity>().GetAll(x => x.BlogId == blogId, y => y.Include(z => z.Tag));

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<BlogTagEntity>);
            }

            return result;
        }

        protected override BlogTagEntity ToEntity(BlogTagMD metaData)
        {
            return new BlogTagEntity
            {
                Id = metaData.Id,
                BlogId = metaData.BlogId,
                TagId = metaData.TagId
            };
        }

        protected override BlogTagMD ToMetaData(BlogTagEntity entity)
        {
            return new BlogTagMD
            {
                Id = entity.Id,
                BlogId = entity.BlogId,
                TagId = entity.TagId,
                TagOptions = GetOptions()
            };
        }

        public List<SelectListItem> GetOptions()
        {
            return DB
                .GetRepository<TagEntity>()
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }
    }
}
