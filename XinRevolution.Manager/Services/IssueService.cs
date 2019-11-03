using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueService : BaseService<IssueEntity, IssueMD>
    {
        public IssueService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        public override ServiceResultModel<IssueMD> Delete(IssueMD metaData)
        {
            try
            {
                var dumpResources = new List<string>();
                dumpResources.AddRange(DB.GetRepository<IssueRelativeLinkEntity>()
                    .GetAll(x => x.IssueId == metaData.Id && !string.IsNullOrEmpty(x.ResourceUrl))
                    .Select(x => x.ResourceUrl));
                dumpResources.AddRange(DB.GetRepository<IssueItemEntity>()
                    .GetAll(x => x.IssueId == metaData.Id && !string.IsNullOrEmpty(x.ResourceUrl))
                    .Select(x => x.ResourceUrl));

                if (dumpResources.Count() > 0)
                    DumpResource(dumpResources);

                DB.GetRepository<IssueRelativeLinkEntity>().Delete(x => x.IssueId == metaData.Id);
                DB.GetRepository<IssueItemEntity>().Delete(x => x.IssueId == metaData.Id);
            }
            catch (Exception ex)
            {
                return new ServiceResultModel<IssueMD>
                {
                    Status = false,
                    Message = $"操作失敗 : {ex.Message}",
                    Data = metaData,
                };
            }

            var result = base.Delete(metaData);

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
