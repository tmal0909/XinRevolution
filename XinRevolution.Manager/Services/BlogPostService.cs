﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Enum;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class BlogPostService : BaseService<BlogPostEntity, BlogPostMD>
    {
        private readonly string _containerName;

        public BlogPostService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.BlogPostContainer);
        }

        public ServiceResultModel<IEnumerable<BlogPostEntity>> Find(int blogId)
        {
            var result = new ServiceResultModel<IEnumerable<BlogPostEntity>>();

            try
            {
                var entities = _unitOfWork.GetRepository<BlogPostEntity>().GetAll(x => x.BlogId == blogId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<BlogPostEntity>);
            }

            return result;
        }

        public override ServiceResultModel<BlogPostMD> Create(BlogPostMD metaData)
        {
            throw new NotImplementedException();
        }

        public override ServiceResultModel<BlogPostMD> Update(BlogPostMD metaData)
        {
            throw new NotImplementedException();
        }

        public override ServiceResultModel<BlogPostMD> Delete(BlogPostMD metaData)
        {
            throw new NotImplementedException();
        }

        protected override BlogPostEntity ToEntity(BlogPostMD metaData)
        {
            return new BlogPostEntity
            {
                Id = metaData.Id,
                ReferenceType = metaData.ReferenceType,
                ReferenceContent = metaData.ReferenceType == ReferenceTypeEnum.Text ? metaData.TextReferenceContent : metaData.MediaReferenceContent,
                BlogId = metaData.BlogId,
                Sort = metaData.Sort
            };
        }

        protected override BlogPostMD ToMetaData(BlogPostEntity entity)
        {
            return new BlogPostMD
            {
                Id = entity.Id,
                ReferenceType = entity.ReferenceType,
                TextReferenceContent = entity.ReferenceType == ReferenceTypeEnum.Text ? entity.ReferenceContent : string.Empty,
                MediaReferenceContent = entity.ReferenceType != ReferenceTypeEnum.Text ? entity.ReferenceContent : string.Empty,
                BlogId = entity.BlogId,
                Sort = entity.Sort,
                ReferenceTypeOptions = GetOptions()
            };
        }

        public List<SelectListItem> GetOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = $"文字", Value = ((int)ReferenceTypeEnum.Text).ToString() },
                new SelectListItem { Text = $"圖片", Value = ((int)ReferenceTypeEnum.Image).ToString() },
                new SelectListItem { Text = $"影片", Value = ((int)ReferenceTypeEnum.Video).ToString() }
            };
        }
    }
}
