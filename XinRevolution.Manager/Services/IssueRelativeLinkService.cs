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

        public IssueRelativeLinkService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService, IConfiguration configuration) : base(unitOfWork, cloudService)
        {
            _containerName = configuration.GetValue<string>(ConfigurationKeyConstant.IssueRelativeLinkContainer);
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
            throw new NotImplementedException();
        }

        public override ServiceResultModel<IssueRelativeLinkMD> Update(IssueRelativeLinkMD metaData)
        {
            throw new NotImplementedException();
        }

        public override ServiceResultModel<IssueRelativeLinkMD> Delete(IssueRelativeLinkMD metaData)
        {
            throw new NotImplementedException();
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
