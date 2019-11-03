using Microsoft.EntityFrameworkCore;
using System;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class UserService : BaseService<UserEntity, UserMD>
    {
        public UserService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        public ServiceResultModel<UserEntity> Login(UserMD metaData)
        {
            var result = new ServiceResultModel<UserEntity>();

            try
            {
                var entity = _unitOfWork.GetRepository<UserEntity>()
                    .Single(x => x.Account.Equals(metaData.Account, StringComparison.CurrentCultureIgnoreCase));

                if (entity == default(UserEntity))
                    throw new Exception($"帳號錯誤");

                if (!entity.Password.Equals(metaData.Password, StringComparison.CurrentCultureIgnoreCase))
                    throw new Exception($"密碼錯誤");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(UserEntity);
            }

            return result;
        }

        protected override UserEntity ToEntity(UserMD metaData)
        {
            return new UserEntity
            {
                Id = metaData.Id,
                Account = metaData.Account,
                Password = metaData.Password,
                Name = metaData.Name,
                Phone = metaData.Phone,
                Mail = metaData.Mail,
                Address = metaData.Address
            };
        }

        protected override UserMD ToMetaData(UserEntity entity)
        {
            return new UserMD
            {
                Id = entity.Id,
                Account = entity.Account,
                Password = entity.Password,
                Name = entity.Name,
                Phone = entity.Phone,
                Mail = entity.Mail,
                Address = entity.Address
            };
        }
    }
}
