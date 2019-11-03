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
            throw new NotImplementedException();
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
