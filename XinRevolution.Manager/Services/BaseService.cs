using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using XinRevolution.Manager.Models;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager.Services
{
    public abstract class BaseService<TEntity, TMetaData> where TEntity : class, new() where TMetaData : class
    {
        protected readonly IUnitOfWork<DbContext> _unitOfWork;

        public BaseService(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResultModel<IEnumerable<TEntity>> Find()
        {
            var result = new ServiceResultModel<IEnumerable<TEntity>>();

            try
            {
                var entities = _unitOfWork.GetRepository<TEntity>().GetAll();

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = entities;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(IEnumerable<TEntity>);
            }

            return result;
        }

        public ServiceResultModel<TMetaData> FindMetaData()
        {
            var result = new ServiceResultModel<TMetaData>();

            try
            {
                var metaData = ToMetaData(new TEntity());

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(TMetaData);
            }

            return result;
        }

        public ServiceResultModel<TMetaData> FindMetaData(object key)
        {
            var result = new ServiceResultModel<TMetaData>();

            try
            {
                var entity = _unitOfWork.GetRepository<TEntity>().Single(key);

                if (entity == default(TEntity))
                    throw new Exception($"無法取得資料列 ({key})");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = ToMetaData(entity);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = default(TMetaData);
            }

            return result;
        }

        public virtual ServiceResultModel<TMetaData> Create(TMetaData metaData)
        {
            var result = new ServiceResultModel<TMetaData>();

            try
            {
                _unitOfWork.GetRepository<TEntity>().Insert(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法新增資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public virtual ServiceResultModel<TMetaData> Update(TMetaData metaData)
        {
            var result = new ServiceResultModel<TMetaData>();

            try
            {
                _unitOfWork.GetRepository<TEntity>().Update(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法更新資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        public virtual ServiceResultModel<TMetaData> Delete(TMetaData metaData)
        {
            var result = new ServiceResultModel<TMetaData>();

            try
            {
                _unitOfWork.GetRepository<TEntity>().Delete(ToEntity(metaData));

                if (_unitOfWork.Commit() <= 0)
                    throw new Exception($"無法刪除資料列");

                result.Status = true;
                result.Message = $"操作成功";
                result.Data = metaData;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();

                result.Status = false;
                result.Message = $"操作失敗 : {ex.Message}";
                result.Data = metaData;
            }

            return result;
        }

        protected abstract TEntity ToEntity(TMetaData metaData);

        protected abstract TMetaData ToMetaData(TEntity entity);
    }
}
