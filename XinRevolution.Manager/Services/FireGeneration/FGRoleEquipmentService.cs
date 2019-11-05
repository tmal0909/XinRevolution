using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Database.Enum.FireGeneration;
using XinRevolution.Extension;
using XinRevolution.Manager.Constants;
using XinRevolution.Manager.Enum;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services.FireGeneration
{
    public class FGRoleEquipmentService : BaseService<FGRoleEquipmentEntity, FGRoleEquipmentMD>
    {
        private readonly string _containerName;

        public FGRoleEquipmentService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.FGRoleEquipmentContainer);
        }

        public ServiceResultModel<IEnumerable<FGRoleEquipmentEntity>> Find(int roleId)
        {
            var result = new ServiceResultModel<IEnumerable<FGRoleEquipmentEntity>>();

            try
            {
                var entities = DB.GetRepository<FGRoleEquipmentEntity>().GetAll(x => x.RoleId == roleId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<FGRoleEquipmentEntity>);
            }

            return result;
        }

        public override ServiceResultModel<FGRoleEquipmentMD> Create(FGRoleEquipmentMD metaData)
        {
            try
            {
                if (metaData.SlideResourceFile != null)
                    metaData.SlideResourceUrl = UploadResource(_containerName, metaData.SlideResourceFile, ResourceTypeEnum.Image);

                if (metaData.MainResourceFile != null)
                    metaData.MainResourceUrl = UploadResource(_containerName, metaData.MainResourceFile, ResourceTypeEnum.Image);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(metaData.SlideResourceUrl))
                {
                    DumpResource(metaData.SlideResourceUrl);
                    DB.Commit();

                    metaData.SlideResourceUrl = string.Empty;
                }

                if (!string.IsNullOrEmpty(metaData.MainResourceUrl))
                {
                    DumpResource(metaData.MainResourceUrl);
                    DB.Commit();

                    metaData.MainResourceUrl = string.Empty;
                }

                return new ServiceResultModel<FGRoleEquipmentMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Create(metaData);

            if (!result.Status)
            {
                if (!string.IsNullOrEmpty(metaData.SlideResourceUrl))
                {
                    DumpResource(metaData.SlideResourceUrl);
                    metaData.SlideResourceUrl = string.Empty;
                }

                if (!string.IsNullOrEmpty(metaData.MainResourceUrl))
                {
                    DumpResource(metaData.MainResourceUrl);
                    metaData.MainResourceUrl = string.Empty;
                }

                DB.Commit();
            }

            return result;
        }

        public override ServiceResultModel<FGRoleEquipmentMD> Update(FGRoleEquipmentMD metaData)
        {
            var sourceData = DB.GetRepository<FGRoleEquipmentEntity>().Single(metaData.Id);
            var slideResourceChange = false;
            var mainResourceChange = false;

            try
            {
                if (metaData.SlideResourceFile != null)
                {
                    metaData.SlideResourceUrl = UploadResource(_containerName, metaData.SlideResourceFile, ResourceTypeEnum.Image);
                    slideResourceChange = true;
                }

                if (metaData.MainResourceFile != null)
                {
                    metaData.MainResourceUrl = UploadResource(_containerName, metaData.MainResourceFile, ResourceTypeEnum.Image);
                    mainResourceChange = true;
                }
            }
            catch (Exception ex)
            {
                if (slideResourceChange)
                {
                    DumpResource(metaData.SlideResourceUrl);
                    DB.Commit();

                    metaData.SlideResourceUrl = sourceData.SlideResourceUrl;
                }

                if (mainResourceChange)
                {
                    DumpResource(metaData.MainResourceUrl);
                    DB.Commit();

                    metaData.MainResourceUrl = sourceData.MainResourceUrl;
                }

                return new ServiceResultModel<FGRoleEquipmentMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Update(metaData);

            if (result.Status)
            {
                if (slideResourceChange)
                {
                    DumpResource(sourceData.SlideResourceUrl);
                    DB.Commit();
                }

                if (mainResourceChange)
                {
                    DumpResource(sourceData.MainResourceUrl);
                    DB.Commit();
                }
            }
            else
            {
                if (slideResourceChange)
                {
                    DumpResource(metaData.SlideResourceUrl);
                    DB.Commit();

                    metaData.SlideResourceUrl = sourceData.SlideResourceUrl;
                }

                if (mainResourceChange)
                {
                    DumpResource(metaData.MainResourceUrl);
                    DB.Commit();

                    metaData.MainResourceUrl = sourceData.MainResourceUrl;
                }
            }

            return result;
        }

        public override ServiceResultModel<FGRoleEquipmentMD> Delete(FGRoleEquipmentMD metaData)
        {
            try
            {
                if (!string.IsNullOrEmpty(metaData.SlideResourceUrl))
                    DumpResource(metaData.SlideResourceUrl);

                if (!string.IsNullOrEmpty(metaData.MainResourceUrl))
                    DumpResource(metaData.MainResourceUrl);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<FGRoleEquipmentMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData
                };
            }

            var result = base.Delete(metaData);

            return result;
        }

        protected override FGRoleEquipmentEntity ToEntity(FGRoleEquipmentMD metaData)
        {
            return new FGRoleEquipmentEntity {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro,
                SlideResourceUrl = metaData.SlideResourceUrl,
                MainResourceUrl = metaData.MainResourceUrl,
                Sort = metaData.Sort,
                RoleId = metaData.RoleId
            };
        }

        protected override FGRoleEquipmentMD ToMetaData(FGRoleEquipmentEntity entity)
        {
            return new FGRoleEquipmentMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Intro = entity.Intro,
                SlideResourceUrl = entity.SlideResourceUrl,
                MainResourceUrl = entity.MainResourceUrl,
                Sort = entity.Sort,
                RoleId = entity.RoleId
            };
        }
    }
}
