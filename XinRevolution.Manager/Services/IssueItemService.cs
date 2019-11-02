using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueItemService : BaseService<IssueItemEntity, IssueItemMD>
    {
        private readonly string _containerName;

        public IssueItemService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.IssueItemContainer);
        }

        public ServiceResultModel<IEnumerable<IssueItemEntity>> Find(int issueId)
        {
            var result = new ServiceResultModel<IEnumerable<IssueItemEntity>>();

            try
            {
                var entities = _unitOfWork.GetRepository<IssueItemEntity>().GetAll(x => x.IssueId == issueId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<IssueItemEntity>);
            }

            return result;
        }

        public override ServiceResultModel<IssueItemMD> Create(IssueItemMD metaData)
        {
            var result = new ServiceResultModel<IssueItemMD>();
            var resourceChange = false;

            try
            {
                if (metaData.IssueId <= 0)
                    throw new Exception($"資料異常");

                if (metaData.ResourceFile == null || metaData.ResourceFile.Length <= 0)
                    throw new Exception($"檔案異常");

                var extension = Path.GetExtension(metaData.ResourceFile.FileName).ToLower();
                if (!ValidResourceTypeConstant.Image.Contains(extension))
                    throw new Exception($"不支援該類型資源檔案");

                var uploadResult = _cloudService.Upload(_containerName, metaData.ResourceFile);
                if (!uploadResult.Status)
                    throw new Exception(uploadResult.Message);

                resourceChange = true;
                metaData.ResourceUrl = uploadResult.Data;

                _unitOfWork.GetRepository<IssueItemEntity>().Insert(ToEntity(metaData));

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
                        ResourceUrl = metaData.ResourceUrl,
                        DumpStatus = false
                    });
                    _unitOfWork.Commit();

                    metaData.ResourceUrl = string.Empty;
                }

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<IssueItemMD> Update(IssueItemMD metaData)
        {
            var result = new ServiceResultModel<IssueItemMD>();
            var resourceChange = false;
            var originResourceUrl = string.Empty;

            try
            {
                if (metaData.ResourceFile != null && metaData.ResourceFile.Length > 0)
                {
                    var extension = Path.GetExtension(metaData.ResourceFile.FileName).ToLower();
                    if (!ValidResourceTypeConstant.Image.Contains(extension))
                        throw new Exception($"不支援該類型資源檔案");

                    originResourceUrl = metaData.ResourceUrl;

                    var uploadResult = _cloudService.Upload(_containerName, metaData.ResourceFile);
                    if (!uploadResult.Status)
                        throw new Exception(uploadResult.Message);

                    resourceChange = true;
                    metaData.ResourceUrl = uploadResult.Data;

                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = originResourceUrl,
                        DumpStatus = false
                    });
                }

                _unitOfWork.GetRepository<IssueItemEntity>().Update(ToEntity(metaData));

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
                        ResourceUrl = metaData.ResourceUrl,
                        DumpStatus = false
                    });
                    _unitOfWork.Commit();

                    metaData.ResourceUrl = originResourceUrl;
                }

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<IssueItemMD> Delete(IssueItemMD metaData)
        {
            var result = new ServiceResultModel<IssueItemMD>();

            try
            {
                _unitOfWork.GetRepository<IssueItemEntity>().Delete(ToEntity(metaData));

                if (!string.IsNullOrEmpty(metaData.ResourceUrl))
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.ResourceUrl,
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

        protected override IssueItemEntity ToEntity(IssueItemMD metaData)
        {
            return new IssueItemEntity
            {
                Id = metaData.Id,
                Title = metaData.Title,
                Content = metaData.Content,
                ResourceUrl = metaData.ResourceUrl,
                ReleaseDate = metaData.ReleaseDate,
                IssueId = metaData.IssueId
            };
        }

        protected override IssueItemMD ToMetaData(IssueItemEntity entity)
        {
            return new IssueItemMD
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                ResourceUrl = entity.ResourceUrl,
                ReleaseDate = entity.ReleaseDate,
                IssueId = entity.IssueId
            };
        }
    }
}
