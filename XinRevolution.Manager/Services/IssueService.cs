using Microsoft.EntityFrameworkCore;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueService : BaseService<IssueEntity, IssueMD>
    {
        public IssueService(IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork) { }

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
