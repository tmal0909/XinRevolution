using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGGroupService : BaseService<FGGroupEntity, FGGroupMD>
    {
        private readonly string _containerName;
        private readonly IAzureBlobService _cloudService;

        public FGGroupService(IConfiguration configuration, IAzureBlobService cloudService, IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGGroupContainer);
            _cloudService = cloudService;
        }

        public override ServiceResultModel<FGGroupMD> Create(FGGroupMD metaData)
        {
            var result = new ServiceResultModel<FGGroupMD>();
            var resourceChange = false;

            try
            {
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

                _unitOfWork.GetRepository<FGGroupEntity>().Insert(ToEntity(metaData));

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

        public override ServiceResultModel<FGGroupMD> Update(FGGroupMD metaData)
        {
            var result = new ServiceResultModel<FGGroupMD>();
            var sourceData = _unitOfWork.GetRepository<FGGroupEntity>().Single(metaData.Id);
            var resourceChange = false;

            try
            {
                if (metaData.ResourceFile != null && metaData.ResourceFile.Length > 0)
                {
                    var extension = Path.GetExtension(metaData.ResourceFile.FileName).ToLower();
                    if (!ValidResourceTypeConstant.Image.Contains(extension))
                        throw new Exception($"不支援該類型資源檔案");

                    var uploadResult = _cloudService.Upload(_containerName, metaData.ResourceFile);
                    if (!uploadResult.Status)
                        throw new Exception(uploadResult.Message);

                    resourceChange = true;
                    metaData.ResourceUrl = uploadResult.Data;

                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = sourceData.ResourceUrl,
                        DumpStatus = false
                    });
                }

                _unitOfWork.GetRepository<FGGroupEntity>().Update(ToEntity(metaData));

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

                    metaData.ResourceUrl = sourceData.ResourceUrl;
                }

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<FGGroupMD> Delete(FGGroupMD metaData)
        {
            var result = new ServiceResultModel<FGGroupMD>();

            try
            {
                // TODO : Remove Relative Row Data & Resource of Character and Equipments
                _unitOfWork.GetRepository<FGGroupEntity>().Delete(ToEntity(metaData));

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

        protected override FGGroupEntity ToEntity(FGGroupMD metaData)
        {
            return new FGGroupEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                ResourceUrl = metaData.ResourceUrl,
                Note = metaData.Note,
                Sort = metaData.Sort
            };
        }

        protected override FGGroupMD ToMetaData(FGGroupEntity entity)
        {
            return new FGGroupMD
            {
                Id = entity.Id,
                Name = entity.Name,
                ResourceUrl = entity.ResourceUrl,
                Note = entity.Note,
                Sort = entity.Sort
            };
        }
    }
}
