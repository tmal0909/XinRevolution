using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class WorkService : BaseService<WorkEntity, WorkMD>
    {
        private readonly string _containerName;

        public WorkService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.WorkContainer);
        }

        public override ServiceResultModel<WorkMD> Update(WorkMD metaData)
        {
            var result = new ServiceResultModel<WorkMD>();
            var sourceData = _unitOfWork.GetRepository<WorkEntity>().Single(metaData.Id);
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

                    if (!string.IsNullOrEmpty(sourceData.ResourceUrl))
                        _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                        {
                            ResourceUrl = sourceData.ResourceUrl,
                            DumpStatus = false
                        });
                }

                _unitOfWork.GetRepository<WorkEntity>().Update(ToEntity(metaData));

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
                    _unitOfWork.GetRepository<DumpResourceEntity>().Delete(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.ResourceUrl,
                        DumpStatus = false
                    });
                    _unitOfWork.Commit();

                    metaData.ResourceUrl = !string.IsNullOrEmpty(sourceData.ResourceUrl) ? sourceData.ResourceUrl : string.Empty;
                }

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        protected override WorkEntity ToEntity(WorkMD metaData)
        {
            return new WorkEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro,
                ResourceUrl = metaData.ResourceUrl,
                Controller = metaData.Controller
            };
        }

        protected override WorkMD ToMetaData(WorkEntity entity)
        {
            return new WorkMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Intro = entity.Intro,
                ResourceUrl = entity.ResourceUrl,
                Controller = entity.Controller
            };
        }
    }
}
