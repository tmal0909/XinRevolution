using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Entity.FireGeneration;
using XinRevolution.Repository.Interface;
using XinRevolution.Web.Model;
using XinRevolution.Web.ViewModels.FireGeneration;

namespace XinRevolution.Web.Services
{
    public class FireGenerationService
    {
        private readonly IUnitOfWork<DbContext> _unitOfWork;

        public FireGenerationService(IUnitOfWork<DbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResultModel<FireGenerationIndexViewModel> FindCharacterGroup()
        {
            var result = new ServiceResultModel<FireGenerationIndexViewModel>();

            try
            {
                var groups = _unitOfWork.GetRepository<FGGroupEntity>()
                    .GetAll(x => x.Include(y => y.Roles).ThenInclude(y => y.Resources));

                result.Data.FireGenerationGroups = groups;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<FireGenerationIntroViewModel> FindIntro(string controllerName)
        {
            var result = new ServiceResultModel<FireGenerationIntroViewModel>();

            try
            {
                var work = _unitOfWork.GetRepository<WorkEntity>().Single(x => x.Controller.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase));

                result.Data.Intro = work.Intro;
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
