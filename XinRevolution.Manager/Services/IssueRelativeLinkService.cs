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
    public class IssueRelativeLinkService : BaseService<IssueRelativeLinkEntity, IssueRelativeLinkMD>
    {
        private readonly string _containerName;
        private readonly IAzureBlobService _cloudService;

        public IssueRelativeLinkService(IConfiguration configuration, IAzureBlobService cloudService, IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.IssueRelativeLinkContainer);
            _cloudService = cloudService;
        }

        public ServiceResultModel<IEnumerable<IssueRelativeLinkEntity>> Find(int issueId)
        {
            var result = new ServiceResultModel<IEnumerable<IssueRelativeLinkEntity>>();

            try
            {
                var entities = _unitOfWork.GetRepository<IssueRelativeLinkEntity>().GetAll(x => x.IssueId == issueId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<IssueRelativeLinkEntity>);
            }

            return result;
        }

        public override ServiceResultModel<IssueRelativeLinkMD> Create(IssueRelativeLinkMD metaData)
        {
            var result = new ServiceResultModel<IssueRelativeLinkMD>();
            var resourceChange = false;

            try
            {
                if (metaData.IssueId <= 0)
                    throw new Exception($"資料異常");

                if (metaData.ResourceFile == null || metaData.ResourceFile.Length <= 0)
                    throw new Exception($"資源檔案異常");

                var extension = Path.GetExtension(metaData.ResourceFile.FileName).ToLower();
                if (!ValidResourceTypeConstant.Image.Contains(extension))
                    throw new Exception($"不支援該類型資源檔案");

                var uploadResult = _cloudService.Upload(_containerName, metaData.ResourceFile);
                if (!uploadResult.Status)
                    throw new Exception(uploadResult.Message);

                resourceChange = true;
                metaData.ResourceUrl = uploadResult.Data;

                _unitOfWork.GetRepository<IssueRelativeLinkEntity>().Insert(ToEntity(metaData));

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

        public override ServiceResultModel<IssueRelativeLinkMD> Update(IssueRelativeLinkMD metaData)
        {
            var result = new ServiceResultModel<IssueRelativeLinkMD>();
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

                _unitOfWork.GetRepository<IssueRelativeLinkEntity>().Update(ToEntity(metaData));

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

        public override ServiceResultModel<IssueRelativeLinkMD> Delete(IssueRelativeLinkMD metaData)
        {
            var result = new ServiceResultModel<IssueRelativeLinkMD>();

            try
            {
                _unitOfWork.GetRepository<IssueRelativeLinkEntity>().Delete(ToEntity(metaData));

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

        protected override IssueRelativeLinkEntity ToEntity(IssueRelativeLinkMD metaData)
        {
            return new IssueRelativeLinkEntity
            {
                Id = metaData.Id,
                LinkUrl = metaData.LinkUrl,
                ResourceUrl = metaData.ResourceUrl,
                Note = metaData.Note,
                IssueId = metaData.IssueId
            };
        }

        protected override IssueRelativeLinkMD ToMetaData(IssueRelativeLinkEntity entity)
        {
            return new IssueRelativeLinkMD
            {
                Id = entity.Id,
                LinkUrl = entity.LinkUrl,
                ResourceUrl = entity.ResourceUrl,
                Note = entity.Note,
                IssueId = entity.IssueId
            };
        }
    }
}
