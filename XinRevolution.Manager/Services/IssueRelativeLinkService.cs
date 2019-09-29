using Microsoft.EntityFrameworkCore;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueRelativeLinkService : BaseService<IssueRelativeLinkEntity, IssueRelativeLinkMD>
    {
        public IssueRelativeLinkService(IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork) { }

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
