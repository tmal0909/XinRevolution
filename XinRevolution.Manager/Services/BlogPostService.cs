using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Enum;
using XinRevolution.Extension;
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
                var entities = DB.GetRepository<BlogPostEntity>().GetAll(x => x.BlogId == blogId);

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
            try
            {
                if (metaData.ReferenceType != ReferenceTypeEnum.Text)
                    metaData.MediaReferenceContent = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Media);
            }
            catch (Exception ex)
            {
                metaData.MediaReferenceContent = string.Empty;

                return new ServiceResultModel<BlogPostMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Create(metaData);

            if (!result.Status)
            {
                if (metaData.ResourceFile != null && !string.IsNullOrEmpty(result.Data.MediaReferenceContent))
                {
                    DumpResource(result.Data.MediaReferenceContent);
                    _unitOfWork.Commit();
                }

                result.Data.MediaReferenceContent = string.Empty;
            }

            return result;
        }

        public override ServiceResultModel<BlogPostMD> Update(BlogPostMD metaData)
        {
            var sourceData = DB.GetRepository<BlogPostEntity>().Single(metaData.Id);

            try
            {
                if (metaData.ReferenceType != ReferenceTypeEnum.Text && metaData.ResourceFile != null)
                    metaData.MediaReferenceContent = UploadResource(_containerName, metaData.ResourceFile, ResourceTypeEnum.Media);
            }
            catch (Exception ex)
            {
                metaData.MediaReferenceContent = sourceData.ReferenceType != ReferenceTypeEnum.Text ? sourceData.ReferenceContent : string.Empty;

                return new ServiceResultModel<BlogPostMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Update(metaData);

            if (result.Status)
            {
                if (sourceData.ReferenceType != ReferenceTypeEnum.Text && !string.IsNullOrEmpty(sourceData.ReferenceContent))
                {
                    DumpResource(sourceData.ReferenceContent);
                    _unitOfWork.Commit();
                }
            }
            else
            {
                if (metaData.ResourceFile != null && !string.IsNullOrEmpty(result.Data.MediaReferenceContent))
                {
                    DumpResource(result.Data.MediaReferenceContent);
                    _unitOfWork.Commit();
                }

                metaData.MediaReferenceContent = sourceData.ReferenceType != ReferenceTypeEnum.Text ? sourceData.ReferenceContent : string.Empty;
            }

            return result;
        }

        public override ServiceResultModel<BlogPostMD> Delete(BlogPostMD metaData)
        {
            try
            {
                if (metaData.ReferenceType != ReferenceTypeEnum.Text && !string.IsNullOrEmpty(metaData.MediaReferenceContent))
                    DumpResource(metaData.MediaReferenceContent);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<BlogPostMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Delete(metaData);

            return result;
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
                new SelectListItem { Text = EnumHelper<ReferenceTypeEnum>.GetDisplayValue(ReferenceTypeEnum.Text), Value = ((int)ReferenceTypeEnum.Text).ToString() },
                new SelectListItem { Text = EnumHelper<ReferenceTypeEnum>.GetDisplayValue(ReferenceTypeEnum.Image), Value = ((int)ReferenceTypeEnum.Image).ToString() },
                new SelectListItem { Text = EnumHelper<ReferenceTypeEnum>.GetDisplayValue(ReferenceTypeEnum.Video), Value = ((int)ReferenceTypeEnum.Video).ToString() }
            };
        }
    }
}
