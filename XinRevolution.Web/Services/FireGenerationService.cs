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

        public ServiceResultModel<FireGenerationIntroViewModel> FindIntro(string controllerName)
        {
            var result = new ServiceResultModel<FireGenerationIntroViewModel>();

            try
            {
                var work = _unitOfWork.GetRepository<WorkEntity>().Single(x => x.Controller.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase));

                result.Data.Intro = work.Intro;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<FireGenerationIndexViewModel> FindRoleGroup()
        {
            var result = new ServiceResultModel<FireGenerationIndexViewModel>();

            try
            {
                var groups = _unitOfWork.GetRepository<FGGroupEntity>()
                    .GetAll(x => x.Include(y => y.Roles).ThenInclude(y => y.Resources));

                result.Data.FireGenerationGroups = groups;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<FireGenerationRoleViewModel> FindRole(int roleId)
        {
            var result = new ServiceResultModel<FireGenerationRoleViewModel>();

            try
            {
                var role = _unitOfWork.GetRepository<FGGroupRoleEntity>()
                    .Single(x => x.Id == roleId, x => x.Include(y => y.Resources).Include(y => y.Equipments));

                result.Data.Role = role;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<FireGenerationStoryLineViewModel> FindStoryLine()
        {
            var result = new ServiceResultModel<FireGenerationStoryLineViewModel>();

            try
            {
                var stories = _unitOfWork.GetRepository<FGSeasonEntity>().GetAll(x => x.Include(y => y.Chapters));

                result.Data.Stories = stories;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }
        
        public ServiceResultModel<FireGenerationSeasonViewModel> FindSeason(int seasonId)
        {
            var result = new ServiceResultModel<FireGenerationSeasonViewModel>();

            try
            {
                var season = _unitOfWork.GetRepository<FGSeasonEntity>().Single(x => x.Id == seasonId, x => x.Include(y => y.Chapters));
                season.Chapters = season.Chapters.OrderBy(x => x.SerialNumber).ToList();

                result.Data.Season = season;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<FireGenerationViewCategoryViewModel> FindCategory()
        {
            var result = new ServiceResultModel<FireGenerationViewCategoryViewModel>();

            try
            {
                var categories = _unitOfWork.GetRepository<FGViewCategoryEntity>()
                    .GetAll(x => x.Include(y => y.Events));

                result.Data.Categories = categories;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }

        public ServiceResultModel<FireGenerationViewCategoryEventViewModel> FindCategoryEvent(int eventId)
        {
            var result = new ServiceResultModel<FireGenerationViewCategoryEventViewModel>();

            try
            {
                var eventItem = _unitOfWork.GetRepository<FGViewCategoryEvnentEntity>().Single(x => x.Id == eventId);

                result.Data.Event = eventItem;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = $"查詢失敗，{ex.Message}";
            }

            return result;
        }
    }
}
