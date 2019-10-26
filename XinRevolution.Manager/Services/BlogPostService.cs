using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Enum;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class BlogPostService : BaseService<BlogPostEntity, BlogPostMD>
    {
        private readonly string _containerName;
        private readonly IAzureBlobService _cloudService;

        public BlogPostService(IConfiguration configuration, IAzureBlobService cloudService, IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.BlogPostContainer);
            _cloudService = cloudService;
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
            var result = new ServiceResultModel<BlogPostMD>();
            var resourceChange = false;

            try
            {
                if (metaData.BlogId <= 0)
                    throw new Exception($"資料異常");

                if (metaData.ReferenceType != ReferenceTypeEnum.Text)
                {
                    if (metaData.ResourceFile == null || metaData.ResourceFile.Length <= 0)
                        throw new Exception($"資源檔案異常");

                    var extension = Path.GetExtension(metaData.ResourceFile.FileName).ToLower();
                    if (!ValidResourceTypeConstant.Image.Contains(extension) && !ValidResourceTypeConstant.Video.Contains(extension))
                        throw new Exception($"不支援該類型資源檔案");

                    var uploadResult = _cloudService.Upload(_containerName, metaData.ResourceFile);
                    if (!uploadResult.Status)
                        throw new Exception(uploadResult.Message);

                    resourceChange = true;
                    metaData.MediaReferenceContent = uploadResult.Data;
                }

                _unitOfWork.GetRepository<BlogPostEntity>().Insert(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法新增資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                if (resourceChange)
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.MediaReferenceContent,
                        DumpStatus = false
                    });

                    metaData.MediaReferenceContent = string.Empty;
                }

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<BlogPostMD> Update(BlogPostMD metaData)
        {
            var result = new ServiceResultModel<BlogPostMD>();
            var sourceData = _unitOfWork.GetRepository<BlogPostEntity>().Single(metaData.Id);
            var resourceChange = false;

            try
            {
                if (metaData.ReferenceType != ReferenceTypeEnum.Text)
                {
                    if (metaData.ResourceFile == null || metaData.ResourceFile.Length <= 0)
                        throw new Exception($"資源檔案異常");

                    var extension = Path.GetExtension(metaData.ResourceFile.FileName).ToLower();
                    if (!ValidResourceTypeConstant.Image.Contains(extension) && !ValidResourceTypeConstant.Video.Contains(extension))
                        throw new Exception($"不支援該類型資源檔案");

                    var uploadResult = _cloudService.Upload(_containerName, metaData.ResourceFile);
                    if (!uploadResult.Status)
                        throw new Exception(uploadResult.Message);

                    resourceChange = true;
                    metaData.MediaReferenceContent = uploadResult.Data;
                }
                
                if (resourceChange && sourceData.ReferenceType != ReferenceTypeEnum.Text)
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = sourceData.ReferenceContent,
                        DumpStatus = false
                    });

                _unitOfWork.GetRepository<BlogPostEntity>().Update(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法更新資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                if (resourceChange)
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.MediaReferenceContent,
                        DumpStatus = false
                    });

                    metaData.MediaReferenceContent = sourceData.ReferenceType != ReferenceTypeEnum.Text ? sourceData.ReferenceContent : string.Empty;
                }

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<BlogPostMD> Delete(BlogPostMD metaData)
        {
            var result = new ServiceResultModel<BlogPostMD>();

            try
            {
                _unitOfWork.GetRepository<BlogPostEntity>().Delete(ToEntity(metaData));

                if (metaData.ReferenceType != ReferenceTypeEnum.Text)
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.MediaReferenceContent,
                        DumpStatus = false
                    });

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
