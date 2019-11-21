using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
                    .GetAll(x => x.Roles).ToList();

                groups.ForEach(x => x.Roles.ForEach(y => y.Resources = _unitOfWork.GetRepository<FGRoleResourceEntity>().GetAll(z => z.RoleId == y.Id).ToList()));
                
                result.Data.FireGenerationGroups = groups;
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
