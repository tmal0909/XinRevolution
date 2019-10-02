﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class IssueItemService : BaseService<IssueItemEntity, IssueItemMD>
    {
        public IssueItemService(IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork) { }

        public ServiceResultModel<IEnumerable<IssueItemEntity>> Find(int issueId)
        {
            var result = new ServiceResultModel<IEnumerable<IssueItemEntity>>();

            try
            {
                var entities = _unitOfWork.GetRepository<IssueItemEntity>().GetAll(x => x.IssueId == issueId);

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<IssueItemEntity>);
            }

            return result;
        }



        #region TODO : override

        public override ServiceResultModel<IssueItemMD> Create(IssueItemMD metaData)
        {
            throw new NotImplementedException();
        }

        public override ServiceResultModel<IssueItemMD> Update(IssueItemMD metaData)
        {
            throw new NotImplementedException();
        }

        public override ServiceResultModel<IssueItemMD> Delete(IssueItemMD metaData)
        {
            throw new NotImplementedException();
        }

        #endregion



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
