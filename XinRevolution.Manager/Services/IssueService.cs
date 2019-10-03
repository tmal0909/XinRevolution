using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueService : BaseService<IssueEntity, IssueMD>
    {
        public IssueService(IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork) { }

        public override ServiceResultModel<IssueMD> Delete(IssueMD metaData)
        {
            var result = new ServiceResultModel<IssueMD>();

            try
            {
                var dumpResources = new List<DumpResourceEntity>();
                var issueItems = _unitOfWork.GetRepository<IssueItemEntity>().GetAll(x => x.IssueId == metaData.Id);
                var issueRelativeLinks = _unitOfWork.GetRepository<IssueRelativeLinkEntity>().GetAll(x => x.IssueId == metaData.Id);

                dumpResources.AddRange(issueItems.Select(x => new DumpResourceEntity { ResourceUrl = x.ResourceUrl, DumpStatus = false }));
                dumpResources.AddRange(issueRelativeLinks.Select(x => new DumpResourceEntity { ResourceUrl = x.ResourceUrl, DumpStatus = false }));

                _unitOfWork.GetRepository<IssueItemEntity>().Delete(x => x.IssueId == metaData.Id);
                _unitOfWork.GetRepository<IssueRelativeLinkEntity>().Delete(x => x.IssueId == metaData.Id);
                _unitOfWork.GetRepository<IssueEntity>().Delete(ToEntity(metaData));

                if (dumpResources.Count > 0)
                    _unitOfWork.GetRepository<DumpResourceEntity>().Insert(dumpResources);

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法刪除資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        protected override IssueEntity ToEntity(IssueMD metaData)
        {
            return new IssueEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Intro = metaData.Intro
            };
        }

        protected override IssueMD ToMetaData(IssueEntity entity)
        {
            return new IssueMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Intro = entity.Intro
            };
        }
    }
}
