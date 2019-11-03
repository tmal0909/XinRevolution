using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class TagService : BaseService<TagEntity, TagMD>
    {
        public TagService(IUnitOfWork<DbContext> unitOfWork, IAzureBlobService cloudService) : base(unitOfWork, cloudService) { }

        public override ServiceResultModel<TagMD> Delete(TagMD metaData)
        {
            throw new NotImplementedException();
        }

        protected override TagEntity ToEntity(TagMD metaData)
        {
            return new TagEntity
            {
                Id = metaData.Id,
                Name = metaData.Name,
                Status = metaData.Status
            };
        }

        protected override TagMD ToMetaData(TagEntity entity)
        {
            return new TagMD
            {
                Id = entity.Id,
                Name = entity.Name,
                Status = entity.Status,
                StatusOptions = GetOptions()
            };
        }

        public List<SelectListItem> GetOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text = $"開啟", Value = $"true" },
                new SelectListItem{ Text = $"關閉", Value = $"false" }
            };
        }
    }
}
