﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using XinRevolution.Database.Entity;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public class TagService : BaseService<TagEntity, TagMD>
    {
        public TagService(IUnitOfWork<DbContext> unitOfWork) : base(unitOfWork) { }

        public override ServiceResultModel<TagMD> Delete(TagMD metaData)
        {
            var result = new ServiceResultModel<TagMD>();

            try
            {
                _unitOfWork.GetRepository<TagEntity>().Delete(ToEntity(metaData));
                _unitOfWork.GetRepository<BlogTagEntity>().Delete(x => x.TagId == metaData.Id);

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法刪除資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                result.Status = true;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
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
                StatusOptions = new List<SelectListItem>
                {
                    new SelectListItem{ Text = $"開啟", Value = $"true" },
                    new SelectListItem{ Text = $"關閉", Value = $"false" }
                }
            };
        }
    }
}