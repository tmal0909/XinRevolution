using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGGroupRoleService : BaseService<FGGroupRoleEntity, FGGroupRoleMD>
    {
        private readonly string _containerName;

        public FGGroupRoleService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGGroupRoleContainer);
        }

        public override ServiceResultModel<FGGroupRoleMD> Create(FGGroupRoleMD metaData)
        {
            var result = new ServiceResultModel<FGGroupRoleMD>();

            try
            {
                if (metaData.GroupId <= 0)
                    throw new Exception($"資料異常");

                _unitOfWork.GetRepository<FGGroupRoleEntity>().Insert(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法新增資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                if (!string.IsNullOrEmpty(metaData.CoverMainResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CoverMainResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CoverMainResourceUrl = string.Empty;
                }

                if (!string.IsNullOrEmpty(metaData.CoverViceResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CoverViceResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CoverViceResourceUrl = string.Empty;
                }

                if (!string.IsNullOrEmpty(metaData.CharacterMainResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CharacterMainResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CharacterMainResourceUrl = string.Empty;
                }

                if (!string.IsNullOrEmpty(metaData.CharacterViceResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CharacterViceResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CharacterViceResourceUrl = string.Empty;
                }

                _unitOfWork.Commit();

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<FGGroupRoleMD> Update(FGGroupRoleMD metaData)
        {
            var result = new ServiceResultModel<FGGroupRoleMD>();
            var sourceData = _unitOfWork.GetRepository<FGGroupRoleEntity>().Single(x => x.Id == metaData.Id);

            try
            {

                _unitOfWork.GetRepository<FGGroupRoleMD>().Update(metaData);

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法更新資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                if (metaData.CoverMainResourceUrl.Equals(sourceData.CoverMainResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CoverMainResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CoverMainResourceUrl = sourceData.CoverMainResourceUrl;
                }

                if (metaData.CoverViceResourceUrl.Equals(sourceData.CoverViceResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CoverViceResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CoverViceResourceUrl = sourceData.CoverViceResourceUrl;
                }

                if (metaData.CharacterMainResourceUrl.Equals(sourceData.CharacterMainResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CharacterMainResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CharacterMainResourceUrl = sourceData.CharacterMainResourceUrl;
                }

                if (metaData.CharacterViceResourceUrl.Equals(sourceData.CharacterViceResourceUrl))
                {
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CharacterViceResourceUrl,
                        DumpStatus = false
                    });

                    metaData.CharacterViceResourceUrl = sourceData.CharacterViceResourceUrl;
                }

                _unitOfWork.Commit();

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public override ServiceResultModel<FGGroupRoleMD> Delete(FGGroupRoleMD metaData)
        {
            var result = new ServiceResultModel<FGGroupRoleMD>();

            try
            {
                _unitOfWork.GetRepository<FGGroupRoleEntity>().Delete(ToEntity(metaData));

                if (!string.IsNullOrEmpty(metaData.CoverMainResourceUrl))
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CoverMainResourceUrl,
                        DumpStatus = false
                    });

                if (!string.IsNullOrEmpty(metaData.CoverViceResourceUrl))
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CoverViceResourceUrl,
                        DumpStatus = false
                    });

                if (!string.IsNullOrEmpty(metaData.CharacterMainResourceUrl))
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CharacterMainResourceUrl,
                        DumpStatus = false
                    });

                if (!string.IsNullOrEmpty(metaData.CharacterViceResourceUrl))
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(new DumpResourceEntity
                    {
                        ResourceUrl = metaData.CharacterViceResourceUrl,
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

        protected override FGGroupRoleEntity ToEntity(FGGroupRoleMD metaData)
        {
            return new FGGroupRoleEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro,
                CoverMainResourceUrl = metaData.CoverMainResourceUrl,
                CoverViceResourceUrl = metaData.CoverViceResourceUrl,
                CharacterMainResourceUrl = metaData.CharacterMainResourceUrl,
                CharacterViceResourceUrl = metaData.CharacterViceResourceUrl,
                RelativeLinkUrl = metaData.RelativeLinkUrl,
                GroupId = metaData.GroupId
            };
        }

        protected override FGGroupRoleMD ToMetaData(FGGroupRoleEntity entity)
        {
            return new FGGroupRoleMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Intro = entity.Intro,
                CoverMainResourceUrl = entity.CoverMainResourceUrl,
                CoverViceResourceUrl = entity.CoverViceResourceUrl,
                CharacterMainResourceUrl = entity.CharacterMainResourceUrl,
                CharacterViceResourceUrl = entity.CharacterViceResourceUrl,
                RelativeLinkUrl = entity.RelativeLinkUrl,
                GroupId = entity.GroupId
            };
        }
    }
}
