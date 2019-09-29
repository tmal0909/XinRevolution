using Microsoft.EntityFrameworkCore;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueItemService : BaseService<IssueItemEntity, IssueItemMD>
    {
        public IssueItemService(IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork) { }

        protected override IssueItemEntity ToEntity(IssueItemMD metaData)
        {
            return new IssueItemEntity
            {
                Id = metaData.Id,
                Title = metaData.Title,
                Content = metaData.Content,
                ResourceUrl = metaData.ResourceUrl,
                ReleaseDate = metaData.ReleaseDate,
                IssueId = metaData.IssueId
            };
        }

        protected override IssueItemMD ToMetaData(IssueItemEntity entity)
        {
            return new IssueItemMD
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                ResourceUrl = entity.ResourceUrl,
                ReleaseDate = entity.ReleaseDate,
                IssueId = entity.IssueId
            };
        }
    }
}
