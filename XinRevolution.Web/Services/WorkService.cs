using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XinRevolution.Database.Entity;
using XinRevolution.Repository.Interface;
using XinRevolution.Web.Model;
using XinRevolution.Web.ViewModels;

namespace XinRevolution.Web.Services
{
    public class WorkService
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public WorkService(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResultModel<WorkViewModel> Find()
        {
            var result = new ServiceResultModel<WorkViewModel>();

            try
            {
                var works = _unitOfWork.GetRepository<WorkEntity>().GetAll();

                result.Data.Works = works;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }
    }
}
